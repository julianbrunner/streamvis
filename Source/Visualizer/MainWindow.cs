// Copyright © Julian Brunner 2009

// This file is part of Stream Visualizer (streamvis).
// 
// Stream Visualizer is free software: you can redistribute it and/or modify
// it under the terms of the GNU General Public License as published by
// the Free Software Foundation, either version 3 of the License, or
// (at your option) any later version.
// 
// Stream Visualizer is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU General Public License for more details.
// 
// You should have received a copy of the GNU General Public License
// along with Stream Visualizer.  If not, see <http://www.gnu.org/licenses/>.

using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using System.Xml.Linq;
using Graphics;
using OpenTK.Math;
using Visualizer.Data;
using Visualizer.Drawing;
using Visualizer.Drawing.Data;
using Visualizer.Drawing.Timing;
using Visualizer.Drawing.Values;

namespace Visualizer
{
	partial class MainWindow : Form
	{
		const string title = "Yarp Visualizer";

		readonly Drawer drawer;
		readonly Data.Timer timer;
		readonly Parameters parameters;
		readonly List<Graph> graphs;
		readonly Layouter layouter;
		readonly TimeManager timeManager;
		readonly SegmentManager segmentManager;
		readonly ValueManager valueManager;
		readonly Diagram diagram;
		readonly VisibleFrameCounter frameCounter;
		readonly CoordinateLabel coordinateLabel;

		Source source;
		string filePath;

		public MainWindow(Parameters parameters)
		{
			Console.WriteLine("Initializing graphics and user interface...");
			InitializeComponent();

			Text = title;

			viewport.ClearColor = parameters.BackgroundColor;
			viewport.VSync = parameters.VerticalSynchronization;
			viewport.Layout += viewport_Layout;

			this.drawer = new Drawer(parameters.LineSmoothing, parameters.AlphaBlending);

			this.timer = new Data.Timer();

			this.parameters = parameters;

			Console.WriteLine("Initializing diagram...");
			this.graphs = new List<Graph>();
			this.layouter = new Layouter(viewport);
			switch (parameters.DiagramType)
			{
				case DiagramType.Continuous: this.timeManager = new ContinuousTimeManager(timer, parameters.DiagramWidth); break;
				case DiagramType.Shiftting: this.timeManager = new ShiftingTimeManager(timer, parameters.DiagramWidth, parameters.DiagramTypeParameter); break;
				case DiagramType.Wrapping: this.timeManager = new WrappingTimeManager(timer, parameters.DiagramWidth, parameters.DiagramTypeParameter); break;
				default: throw new InvalidOperationException();
			}
			this.segmentManager = new SimpleSegmentManager(timeManager, graphs);
			if (parameters.RangeLow == parameters.RangeHigh) this.valueManager = new FittingValueManager(segmentManager, graphs);
			else this.valueManager = new FixedValueManager(parameters.RangeLow, parameters.RangeHigh);
			this.diagram = new Diagram(drawer, graphs, timeManager, segmentManager, valueManager, layouter, parameters.MarkersX, parameters.MarkersY, parameters.DiagramColor);

			Console.WriteLine("Initializing frame counter");
			this.frameCounter = new VisibleFrameCounter(drawer, Color.Yellow, TextAlignment.Far);

			Console.WriteLine("Initializing coordinate display");
			this.coordinateLabel = new CoordinateLabel(coordinateStatusLabel, viewport, layouter, timeManager, valueManager);

			Console.WriteLine("Initializing data source...");
			NewSource(parameters.Ports);

			Console.WriteLine("Applying parameters...");
			freezeToolStripMenuItem.Checked = false;
			freezeToolStripMenuItem_Click(this, EventArgs.Empty);
			graphExtensionToolStripMenuItem.Checked = parameters.ExtendGraphs;
			graphExtensionToolStripMenuItem_Click(this, EventArgs.Empty);
			showStreamListToolStripMenuItem.Checked = true;
			showStreamListToolStripMenuItem_Click(this, EventArgs.Empty);
			minimalModeToolStripMenuItem.Checked = parameters.MinimalMode;
			minimalModeToolStripMenuItem_Click(this, EventArgs.Empty);
			showDiagramToolStripMenuItem.Checked = true;
			showDiagramToolStripMenuItem_Click(this, EventArgs.Empty);
			showFrameCounterToolStripMenuItem.Checked = true;
			showFrameCounterToolStripMenuItem_Click(this, EventArgs.Empty);
			verticalSynchronizationToolStripMenuItem.Checked = parameters.VerticalSynchronization;
			verticalSynchronizationToolStripMenuItem_Click(this, EventArgs.Empty);

			viewport.AddComponent(diagram);
			viewport.AddComponent(frameCounter);
			viewport.AddComponent(coordinateLabel);
		}

		void viewport_Layout(object sender, LayoutEventArgs e)
		{
			frameCounter.Position = new Vector2(viewport.Right, viewport.Top);
		}

		private void streamsListView_ItemChecked(object sender, ItemCheckedEventArgs e)
		{
			Graph graph = (Graph)e.Item.Tag;
			graph.IsDrawn = e.Item.Checked;
		}
		private void newToolStripMenuItem_Click(object sender, EventArgs e)
		{
			using (TextDialog textDialog = new TextDialog("Port selection", "Specify the ports for this capture:", string.Empty))
				if (textDialog.ShowDialog() == DialogResult.OK)
					NewSource(textDialog.Result == string.Empty ? Enumerable.Empty<string>() : textDialog.Result.Split(' '));
		}
		private void openToolStripMenuItem_Click(object sender, EventArgs e)
		{
			if (openCaptureFileDialog.ShowDialog() == DialogResult.OK)
				LoadSource(openCaptureFileDialog.FileName);
		}
		private void saveToolStripMenuItem_Click(object sender, EventArgs e)
		{
			if (filePath == null) saveAsToolStripMenuItem_Click(sender, e);
			else SaveSource(filePath);
		}
		private void saveAsToolStripMenuItem_Click(object sender, EventArgs e)
		{
			if (saveCaptureFileDialog.ShowDialog() == DialogResult.OK) SaveSource(saveCaptureFileDialog.FileName);
		}
		private void exportToolStripMenuItem_Click(object sender, EventArgs e)
		{
			if (exportCaptureFileDialog.ShowDialog() == DialogResult.OK) source.ExportGNUPlot(exportCaptureFileDialog.FileName);
		}
		private void clearDataToolStripMenuItem_Click(object sender, EventArgs e)
		{
			timer.Reset();
			source.ClearData();
		}
		private void freezeToolStripMenuItem_Click(object sender, EventArgs e)
		{
			timeManager.Frozen = freezeToolStripMenuItem.Checked;
		}
		private void graphExtensionToolStripMenuItem_Click(object sender, EventArgs e)
		{
			segmentManager.ExtendGraphs = graphExtensionToolStripMenuItem.Checked;
		}
		private void changeColorToolStripMenuItem_Click(object sender, EventArgs e)
		{
			if (streamsListView.SelectedItems.Count > 0 && colorDialog.ShowDialog() == DialogResult.OK)
				SetColor(streamsListView.SelectedItems[0], colorDialog.Color);
		}
		private void showStreamListToolStripMenuItem_Click(object sender, EventArgs e)
		{
			splitContainer1.Panel1Collapsed = !showStreamListToolStripMenuItem.Checked;
		}
		private void minimalModeToolStripMenuItem_Click(object sender, EventArgs e)
		{
			showStreamListToolStripMenuItem.Checked = !minimalModeToolStripMenuItem.Checked;
			showStreamListToolStripMenuItem_Click(this, EventArgs.Empty);
			toolStripContainer1.TopToolStripPanelVisible = !minimalModeToolStripMenuItem.Checked;
			toolStripContainer1.BottomToolStripPanelVisible = !minimalModeToolStripMenuItem.Checked;
		}
		private void showDiagramToolStripMenuItem_Click(object sender, EventArgs e)
		{
			diagram.IsUpdated = diagram.IsDrawn = showDiagramToolStripMenuItem.Checked;
		}
		private void showFrameCounterToolStripMenuItem_Click(object sender, EventArgs e)
		{
			frameCounter.IsUpdated = frameCounter.IsDrawn = showFrameCounterToolStripMenuItem.Checked;
		}
		private void verticalSynchronizationToolStripMenuItem_Click(object sender, EventArgs e)
		{
			viewport.VSync = verticalSynchronizationToolStripMenuItem.Checked;
		}
		private void viewport_DoubleClick(object sender, EventArgs e)
		{
			minimalModeToolStripMenuItem.Checked = !minimalModeToolStripMenuItem.Checked;
			minimalModeToolStripMenuItem_Click(this, EventArgs.Empty);
		}

		void NewSource(IEnumerable<string> ports)
		{
			DisposeSource();

			SetFilePath(null);
			timer.Reset();
			source = new Source();
			if (ports.Any())
				try { source = Capturing.Capture.Create(ports, timer); }
				catch (InvalidOperationException e) { MessageBox.Show(e.Message, "Capture creation error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
			// TODO: Check beforehand if Yarp is available
			//catch (Exception e)
			//{
			//    if (e.InnerException != null && e.InnerException.InnerException != null && e.InnerException.InnerException is DllNotFoundException)
			//        MessageBox.Show(e.InnerException.InnerException.Message, "Library not found", MessageBoxButtons.OK, MessageBoxIcon.Error);
			//    else throw;
			//}
			RebuildList();
		}
		void LoadSource(string filePath)
		{
			DisposeSource();

			SetFilePath(filePath);
			timer.Reset();
			source = new Source(XElement.Load(this.filePath));
			RebuildList();
		}
		void SaveSource(string filePath)
		{
			SetFilePath(filePath);
			source.XElement.Save(this.filePath);
		}
		void DisposeSource()
		{
			IDisposable disposable = source as IDisposable;
			if (disposable != null) disposable.Dispose();
		}
		void SetFilePath(string filePath)
		{
			this.filePath = filePath;

			Text = title + " - " + (filePath == null ? "Untitled" : System.IO.Path.GetFileName(filePath));
		}
		void RebuildList()
		{
			Random random = new Random();

			graphs.Clear();
			streamsListView.Groups.Clear();
			streamsListView.Items.Clear();

			foreach (Port port in source.Ports)
			{
				ListViewGroup group = new ListViewGroup(port.Name);
				streamsListView.Groups.Add(group);

				foreach (Stream stream in port.Streams)
				{
					DataManager dataManager;
					switch (parameters.SamplerType)
					{
						case SamplerType.PerSecond: dataManager = new PerSecondDataManager(timeManager, stream.EntryData, parameters.DataLogging, parameters.SamplerFrequency); break;
						case SamplerType.PerPixel: dataManager = new PerPixelDataManager(timeManager, stream.EntryData, parameters.DataLogging, parameters.SamplerFrequency, layouter); break;
						default: throw new InvalidOperationException();
					}

					Graph graph = new Graph(layouter, valueManager, segmentManager, drawer, dataManager, parameters.LineWidth);
					graph.Color = Color.FromArgb(random.Next(0x100), random.Next(0x100), random.Next(0x100));
					graphs.Add(graph);

					ListViewItem item = new ListViewItem();
					item.Name = port.Name + "Stream" + stream.Path;
					item.Text = "Stream " + stream.Path;
					item.Group = group;
					item.Checked = true;
					item.Tag = graph;

					SetColor(item, graph.Color);

					streamsListView.Items.Add(item);
				}
			}
		}
		void SetColor(ListViewItem item, Color color)
		{
			Graph graph = (Graph)item.Tag;

			graph.Color = color;
			item.BackColor = color;
			item.ForeColor = item.BackColor.R * 0.299 + item.BackColor.G * 0.587 + item.BackColor.B * 0.114 >= 0x80 ? Color.Black : Color.White;
		}

		// TODO: Remove on release
		private void gCCollectToolStripMenuItem_Click(object sender, EventArgs e)
		{
			GC.Collect();
		}
	}
}
