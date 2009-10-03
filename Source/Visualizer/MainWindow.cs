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
using Utility;
using Visualizer.Data;
using Visualizer.Drawing;
using Visualizer.Drawing.Axes;
using Visualizer.Drawing.Data;
using Visualizer.Drawing.Timing;
using Visualizer.Drawing.Values;
using Visualizer.Environment;

namespace Visualizer
{
	// TODO: Do some cleanup here, extract stuff into extra components
	partial class MainWindow : Form
	{
		const string title = "Yarp Visualizer";

		readonly Drawer drawer;
		readonly Data.Timer timer;
		readonly List<Graph> graphs;
		readonly GraphSettings graphSettings;
		readonly Layouter layouter;
		readonly TimeManager timeManager;
		readonly ValueManager valueManager;
		readonly DataManager dataManager;
		readonly Axis axisX;
		readonly Axis axisY;
		readonly Diagram diagram;
		readonly VisibleFrameCounter frameCounter;
		readonly CoordinateLabel coordinateLabel;
		readonly Settings settings;

		Point oldMousePosition;
		Source source;
		string filePath;

		public MainWindow(Parameters parameters)
		{
			Console.WriteLine("Initializing graphics and user interface...");
			InitializeComponent();

			Text = title;

			viewport.ClearColor = parameters.BackgroundColor;
			viewport.VSync = parameters.VerticalSynchronization;

			this.drawer = new Drawer(parameters.LineSmoothing, parameters.AlphaBlending);

			this.timer = new Data.Timer();

			Console.WriteLine("Initializing diagram...");
			this.graphs = new List<Graph>();

			this.graphSettings = new GraphSettings();
			this.graphSettings.ExtendGraphs = parameters.ExtendGraphs;
			this.graphSettings.LineWidth = parameters.LineWidth;

			this.layouter = new Layouter(viewport);

			switch (parameters.DiagramType)
			{
				case DiagramType.Continuous: this.timeManager = new ContinuousTimeManager(timer); break;
				case DiagramType.Shiftting: this.timeManager = new ShiftingTimeManager(timer, parameters.DiagramTypeParameter); break;
				case DiagramType.Wrapping: this.timeManager = new WrappingTimeManager(timer, parameters.DiagramTypeParameter); break;
				default: throw new InvalidOperationException();
			}
			this.timeManager.Width = parameters.DiagramWidth;

			if (parameters.RangeLow == parameters.RangeHigh) this.valueManager = new FittingValueManager(graphs);
			else this.valueManager = new FixedValueManager(parameters.RangeLow, parameters.RangeHigh);

			switch (parameters.SamplerType)
			{
				case SamplerType.PerSecond: this.dataManager = new PerSecondDataManager(graphs, timeManager, parameters.DataLogging, parameters.SamplerFrequency); break;
				case SamplerType.PerPixel: this.dataManager = new PerPixelDataManager(graphs, timeManager, parameters.DataLogging, layouter, parameters.SamplerFrequency); break;
				default: throw new InvalidOperationException();
			}

			this.axisX = new AxisX(drawer, layouter, timeManager);
			this.axisX.MarkerCount = parameters.MarkersX;
			this.axisX.Color = parameters.DiagramColor;
			this.axisY = new AxisY(drawer, layouter, valueManager);
			this.axisY.MarkerCount = parameters.MarkersY;
			this.axisY.Color = parameters.DiagramColor;

			this.diagram = new Diagram(graphs, graphSettings, layouter, timeManager, valueManager, dataManager, axisX, axisY);

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

			this.settings = new Settings(diagram);

			properties.SelectedObject = settings;
		}

		private void streamsList_ItemChecked(object sender, ItemCheckedEventArgs e)
		{
			Graph graph = (Graph)e.Item.Tag;
			graph.IsDrawn = e.Item.Checked;
		}
		private void viewport_Layout(object sender, LayoutEventArgs e)
		{
			// TODO: Find a cleaner way to avoid a crash if layout is called before frameCounter is initialized
			if (frameCounter != null) frameCounter.Position = new Vector2(viewport.Right, viewport.Top);
		}
		private void viewport_DoubleClick(object sender, EventArgs e)
		{
			minimalModeToolStripMenuItem.Checked = !minimalModeToolStripMenuItem.Checked;
			minimalModeToolStripMenuItem_Click(this, EventArgs.Empty);
		}
		private void viewport_MouseMove(object sender, MouseEventArgs e)
		{
			// TODO: This should be extracted into a ZoomComponent
			if (e.Button == MouseButtons.Right) timeManager.Width *= Math.Pow(1.1, e.Location.X - oldMousePosition.X);

			oldMousePosition = e.Location;
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
			graphSettings.ExtendGraphs = graphExtensionToolStripMenuItem.Checked;
		}
		private void changeColorToolStripMenuItem_Click(object sender, EventArgs e)
		{
			if (streamsList.SelectedItems.Count > 0 && colorDialog.ShowDialog() == DialogResult.OK)
				SetColor(streamsList.SelectedItems[0], colorDialog.Color);
		}
		private void showStreamListToolStripMenuItem_Click(object sender, EventArgs e)
		{
			streamsListContainer.Panel1Collapsed = !showStreamListToolStripMenuItem.Checked;
		}
		private void minimalModeToolStripMenuItem_Click(object sender, EventArgs e)
		{
			showStreamListToolStripMenuItem.Checked = !minimalModeToolStripMenuItem.Checked;
			showStreamListToolStripMenuItem_Click(this, EventArgs.Empty);
			mainContainer.TopToolStripPanelVisible = !minimalModeToolStripMenuItem.Checked;
			mainContainer.BottomToolStripPanelVisible = !minimalModeToolStripMenuItem.Checked;
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
			ColorGenerator colorGenerator = new ColorGenerator();

			graphs.Clear();
			streamsList.Groups.Clear();
			streamsList.Items.Clear();

			foreach (Port port in source.Ports)
			{
				ListViewGroup group = new ListViewGroup(port.Name);
				streamsList.Groups.Add(group);

				foreach (Stream stream in port.Streams)
				{
					Graph graph = new Graph(drawer, diagram, stream.EntryData);
					graph.Color = colorGenerator.NextColor();
					graphs.Add(graph);

					ListViewItem item = new ListViewItem();
					item.Name = port.Name + "Stream" + stream.Path;
					item.Text = stream.Name + " (" + stream.Path + ")";
					item.Group = group;
					item.Checked = true;
					item.Tag = graph;

					SetColor(item, graph.Color);

					streamsList.Items.Add(item);
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
	}
}
