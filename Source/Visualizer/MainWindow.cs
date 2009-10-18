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
		readonly Diagram diagram;
		readonly VisibleFrameCounter frameCounter;
		readonly CoordinateLabel coordinateLabel;
		readonly Settings settings;

		Source source;
		string filePath;

		public bool StreamListVisible
		{
			get { return !streamsListContainer.Panel1Collapsed; }
			set { streamsListContainer.Panel1Collapsed = !value; }
		}
		public bool PropertiesVisible
		{
			get { return !propertiesContainer.Panel2Collapsed; }
			set { propertiesContainer.Panel2Collapsed = !value; }
		}
		public bool MinimalMode
		{
			get { return !mainContainer.TopToolStripPanelVisible && !mainContainer.BottomToolStripPanelVisible && !StreamListVisible && !PropertiesVisible; }
			set { mainContainer.TopToolStripPanelVisible = mainContainer.BottomToolStripPanelVisible = StreamListVisible = PropertiesVisible = !value; }
		}

		public MainWindow(Parameters parameters)
		{
			Console.WriteLine("Initializing graphics and user interface...");
			InitializeComponent();

			Text = title;

			if (parameters.VerticalSynchronization != null) viewport.VSync = parameters.VerticalSynchronization.Value;
			if (parameters.BackgroundColor != null) viewport.ClearColor = parameters.BackgroundColor.Value;

			Console.WriteLine("Initializing drawer...");
			this.drawer = new Drawer();
			if (parameters.LineSmoothing != null) drawer.LineSmoothing = parameters.LineSmoothing.Value;
			if (parameters.AlphaBlending != null) drawer.AlphaBlending = parameters.AlphaBlending.Value;

			Console.WriteLine("Initializing timer...");
			this.timer = new Data.Timer();

			Console.WriteLine("Initializing diagram...");
			this.diagram = CreateDiagram(viewport, drawer, timer, parameters);

			Console.WriteLine("Initializing frame counter");
			this.frameCounter = new VisibleFrameCounter(drawer);
			this.frameCounter.Color = Color.Yellow;
			this.frameCounter.Alignment = TextAlignment.Far;

			Console.WriteLine("Initializing coordinate label");
			this.coordinateLabel = new CoordinateLabel(coordinateStatusLabel, viewport, diagram);

			Console.WriteLine("Initializing data source...");
			NewSource(parameters.Ports);

			Console.WriteLine("Applying parameters...");
			if (parameters.MinimalMode != null) MinimalMode = parameters.MinimalMode.Value;

			Console.WriteLine("Initializing settings...");
			this.settings = new Settings(properties, this, viewport, drawer, timer, diagram, frameCounter);
			properties.SelectedObject = settings;

			Console.WriteLine("Adding components...");
			viewport.AddComponent(diagram);
			viewport.AddComponent(frameCounter);
			viewport.AddComponent(coordinateLabel);
		}

		private void streamsList_ItemChecked(object sender, ItemCheckedEventArgs e)
		{
			StreamListItem streamListItem = (StreamListItem)e.Item.Tag;
			streamListItem.Graph.IsDrawn = e.Item.Checked;
		}
		private void viewport_Layout(object sender, LayoutEventArgs e)
		{
			// TODO: Find a cleaner way to avoid a crash if layout is called before frameCounter is initialized
			if (frameCounter != null) frameCounter.Position = new Vector2(viewport.Right, viewport.Top);
		}
		private void viewport_DoubleClick(object sender, EventArgs e)
		{
			MinimalMode = !MinimalMode;
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
			if (exportCaptureFileDialog.ShowDialog() == DialogResult.OK) source.Save(exportCaptureFileDialog.FileName);
		}
		private void clearDataToolStripMenuItem_Click(object sender, EventArgs e)
		{
			timer.Reset();
			source.ClearData();
		}
		private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
		{
			using (AboutBox aboutBox = new AboutBox()) aboutBox.ShowDialog();
		}
		private void changeNameToolStripMenuItem_Click(object sender, EventArgs e)
		{
			using (TextDialog textDialog = new TextDialog("Stream Name", "Please enter the new name for the stream", string.Empty))
				if (streamsList.SelectedItems.Count > 0 && textDialog.ShowDialog() == DialogResult.OK)
					SetName(streamsList.SelectedItems[0], textDialog.Result);
		}
		private void changeColorToolStripMenuItem_Click(object sender, EventArgs e)
		{
			if (streamsList.SelectedItems.Count > 0 && colorDialog.ShowDialog() == DialogResult.OK)
				SetColor(streamsList.SelectedItems[0], colorDialog.Color);
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

			diagram.Graphs.Clear();
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
					diagram.Graphs.Add(graph);

					ListViewItem item = new ListViewItem();
					item.Name = port.Name + "Stream" + stream.Path;
					item.Text = stream.Name + " (" + stream.Path + ")";
					item.Group = group;
					item.Checked = true;
					item.Tag = new StreamListItem(stream, graph);

					SetColor(item, graph.Color);

					streamsList.Items.Add(item);
				}
			}
		}
		void SetName(ListViewItem item, string name)
		{
			StreamListItem streamListItem = (StreamListItem)item.Tag;

			streamListItem.Stream.Name = name;
			item.Text = streamListItem.Stream.Name + " (" + streamListItem.Stream.Path + ")";
		}
		void SetColor(ListViewItem item, Color color)
		{
			StreamListItem streamListItem = (StreamListItem)item.Tag;

			streamListItem.Graph.Color = color;
			item.BackColor = color;
			item.ForeColor = item.BackColor.R * 0.299 + item.BackColor.G * 0.587 + item.BackColor.B * 0.114 >= 0x80 ? Color.Black : Color.White;
		}

		static Diagram CreateDiagram(Viewport viewport, Drawer drawer, Data.Timer timer, Parameters parameters)
		{
			Diagram diagram = new Diagram();

			diagram.Graphs = new List<Graph>();

			diagram.GraphSettings = new GraphSettings();
			if (parameters.ExtendGraphs != null) diagram.GraphSettings.ExtendGraphs = parameters.ExtendGraphs.Value;
			if (parameters.LineWidth != null) diagram.GraphSettings.LineWidth = parameters.LineWidth.Value;

			diagram.Layouter = new Layouter(viewport);
			diagram.Layouter.BaseMargin = new Padding(9, 8, 0, 9);

			switch (parameters.TimeManagerType)
			{
				case TimeManagerType.Continuous:
					diagram.TimeManager = new ContinuousTimeManager(timer);
					break;
				case TimeManagerType.Shiftting:
					diagram.TimeManager = new ShiftingTimeManager(timer);
					if (parameters.TimeManagerParameter != null) ((ShiftingTimeManager)diagram.TimeManager).ShiftLength = parameters.TimeManagerParameter.Value;
					break;
				case TimeManagerType.Wrapping:
					diagram.TimeManager = new WrappingTimeManager(timer);
					if (parameters.TimeManagerParameter != null) ((WrappingTimeManager)diagram.TimeManager).GapLength = parameters.TimeManagerParameter.Value;
					break;
				default: throw new InvalidOperationException();
			}
			if (parameters.DiagramWidth != null) diagram.TimeManager.Width = parameters.DiagramWidth.Value;

			switch (parameters.ValueManagerType)
			{
				case ValueManagerType.Fitting:
					diagram.ValueManager = new FittingValueManager(diagram);
					break;
				case ValueManagerType.Fixed:
					diagram.ValueManager = new FixedValueManager();
					if (parameters.ValueRange != null) ((FixedValueManager)diagram.ValueManager).FixedRange = parameters.ValueRange.Value;
					break;
				default: throw new InvalidOperationException();
			}

			switch (parameters.DataManagerType)
			{
				case DataManagerType.PerSecond:
					diagram.DataManager = new PerSecondDataManager(diagram);
					if (parameters.DataManagerParameter != null) ((PerSecondDataManager)diagram.DataManager).SamplesPerSecond = parameters.DataManagerParameter.Value;
					break;
				case DataManagerType.PerPixel:
					diagram.DataManager = new PerPixelDataManager(diagram);
					if (parameters.DataManagerParameter != null) ((PerPixelDataManager)diagram.DataManager).SamplesPerPixel = parameters.DataManagerParameter.Value;
					break;
				default: throw new InvalidOperationException();
			}
			if (parameters.ClearData != null) diagram.DataManager.ClearData = parameters.ClearData.Value;

			diagram.AxisX = new AxisX(drawer, diagram);
			if (parameters.MarkerCountX != null) diagram.AxisX.MarkerCount = parameters.MarkerCountX.Value;
			if (parameters.DiagramColor != null) diagram.AxisX.Color = parameters.DiagramColor.Value;

			diagram.AxisY = new AxisY(drawer, diagram);
			if (parameters.MarkerCountY != null) diagram.AxisY.MarkerCount = parameters.MarkerCountY.Value;
			if (parameters.DiagramColor != null) diagram.AxisY.Color = parameters.DiagramColor.Value;

			return diagram;
		}
	}
}
