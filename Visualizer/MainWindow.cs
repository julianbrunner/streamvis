using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using System.Xml.Linq;
using Graphics;
using Visualizer.Data;
using Visualizer.Plotting;
using Visualizer.Plotting.Data;
using Visualizer.Plotting.Timing;
using Visualizer.Plotting.Values;

namespace Visualizer
{
	partial class MainWindow : Form
	{
		const string title = "Yarp Visualizer";

		readonly Drawer drawer;
		readonly Data.Timer timer = new Data.Timer();
		readonly List<Graph> graphs = new List<Graph>();
		readonly Plotter plotter;
		readonly VisibleFrameCounter frameCounter;

		Source source;
		string filePath;

		public MainWindow(Parameters parameters)
		{
			Console.WriteLine("Initializing graphics and user interface...");
			InitializeComponent();

			Text = title;

			viewport.ClearColor = parameters.BackgroundColor;
			viewport.Layout += viewport_Layout;

			drawer = new Drawer();

			Console.WriteLine("Initializing plotter...");
			Layouter layouter = new Layouter(viewport);
			TimeManager timeManager;
			switch (parameters.PlotterType)
			{
				case PlotterType.Continuous: timeManager = new ContinuousTimeManager(timer, parameters.PlotterWidth); break;
				case PlotterType.Shiftting: timeManager = new ShiftingTimeManager(timer, parameters.PlotterWidth, parameters.PlotterTypeParameter); break;
				case PlotterType.Wrapping: timeManager = new WrappingTimeManager(timer, parameters.PlotterWidth, parameters.PlotterTypeParameter); break;
				default: throw new InvalidOperationException();
			}
			DataManager dataManager = new SimpleDataManager(timeManager, graphs);
			ValueManager valueManager;
			if (parameters.RangeLow == parameters.RangeHigh) valueManager = new FittingValueManager(dataManager, graphs);
			else valueManager = new FixedValueManager(parameters.RangeLow, parameters.RangeHigh);
			plotter = new Plotter(drawer, graphs, timeManager, dataManager, valueManager, layouter, parameters.IntervalsX, parameters.IntervalsY, parameters.PlotterColor);

			System.Console.WriteLine("Initializing frame counter");
			frameCounter = new VisibleFrameCounter(drawer, Color.Yellow, TextAlignment.Far);

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
			showPlotterToolStripMenuItem.Checked = true;
			showPlotterToolStripMenuItem_Click(this, EventArgs.Empty);
			showFrameCounterToolStripMenuItem.Checked = true;
			showFrameCounterToolStripMenuItem_Click(this, EventArgs.Empty);
			verticalSynchronizationToolStripMenuItem.Checked = true;
			verticalSynchronizationToolStripMenuItem_Click(this, EventArgs.Empty);

			viewport.AddComponent(plotter);
			viewport.AddComponent(frameCounter);
		}

		void viewport_Layout(object sender, LayoutEventArgs e)
		{
			frameCounter.Position = new Point(viewport.Right, viewport.Top);
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
			// TODO: Reenable exporting
			// TODO: This should be moved
			//			if (exportCaptureFileDialog.ShowDialog() == DialogResult.OK)
			//				foreach (Port port in source.Ports)
			//					using (System.IO.StreamWriter streamWriter = new System.IO.StreamWriter(System.IO.Path.ChangeExtension(exportCaptureFileDialog.FileName, EscapeFilename(port.Name) + ".stream")))
			//						if (port.Streams.Any())
			//						{
			//							IEnumerable<IEnumerable<Entry>> streams =
			//							(
			//								from stream in port.Streams
			//								select stream.Container.Data
			//							)
			//							.ToArray();
			//
			//							IEnumerable<IEnumerator<Entry>> enumerators =
			//							(
			//								from stream in streams
			//								select stream.GetEnumerator()
			//							)
			//							.ToArray();
			//
			//							foreach (Entry leadEntry in streams.First())
			//								if (enumerators.All(enumerator => enumerator.MoveNext()))
			//								{
			//									StringBuilder stringBuilder = new StringBuilder();
			//									stringBuilder.Append(leadEntry.Time.Seconds);
			//									stringBuilder.Append(" ");
			//									foreach (Entry entry in from enumerator in enumerators select enumerator.Current)
			//									{
			//										stringBuilder.Append(entry.Value);
			//										stringBuilder.Append(" ");
			//									}
			//									stringBuilder.Remove(stringBuilder.Length - 1, 1);
			//									streamWriter.WriteLine(stringBuilder.ToString());
			//								}
			//						}
		}
		private void clearDataToolStripMenuItem_Click(object sender, EventArgs e)
		{
			timer.Reset();
			source.ClearData();
		}
		private void freezeToolStripMenuItem_Click(object sender, EventArgs e)
		{
			plotter.TimeManager.Frozen = freezeToolStripMenuItem.Checked;
		}
		private void graphExtensionToolStripMenuItem_Click(object sender, EventArgs e)
		{
			plotter.ExtendGraphs = graphExtensionToolStripMenuItem.Checked;
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
		private void showPlotterToolStripMenuItem_Click(object sender, EventArgs e)
		{
			plotter.IsUpdated = plotter.IsDrawn = showPlotterToolStripMenuItem.Checked;
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
					Graph graph = new Graph(plotter, drawer, stream.EntryData);
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

		//		static string EscapeFilename(string filename)
		//		{
		//			foreach (char invalidChar in System.IO.Path.GetInvalidFileNameChars())
		//				filename = filename.Replace(invalidChar.ToString(), string.Empty);
		//
		//			return filename;
		//		}
	}
}
