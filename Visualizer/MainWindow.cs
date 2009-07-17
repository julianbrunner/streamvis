using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using System.Xml.Linq;
using Visualizer.Data;
using Visualizer.Plotting;

namespace Visualizer
{
	partial class MainWindow : Form
	{
		struct ItemTag
		{
			public readonly Stream stream;
			public readonly Graph graph;

			public ItemTag(Stream stream, Graph graph)
			{
				this.stream = stream;
				this.graph = graph;
			}
		}

		const string title = "Yarp Visualizer";
		const int frameWindow = 20;

		readonly TKDrawer drawer;
		readonly Plotter plotter;
		readonly List<Graph> graphs = new List<Graph>();
		readonly Data.Timer timer = new Data.Timer();
		readonly System.Timers.Timer frameTimer = new System.Timers.Timer(10);
		readonly AutoResetEvent drawFrame = new AutoResetEvent(false);

		Source source;
		string filePath;

		int frames = 0;
		TimeSpan last = TimeSpan.Zero;
		double fps = 0;

		public MainWindow(Parameters parameters)
		{
			Console.WriteLine("Initializing graphics and user interface...");
			InitializeComponent();

			drawer = new TKDrawer(viewport, parameters.BackgroundColor);

			Text = title;

			Console.WriteLine("Initializing plotter...");
			Layouter layouter = new Layouter(drawer);
			TimeManager timeManager;
			switch (parameters.PlotterType)
			{
				case PlotterType.Continuous: timeManager = new ContinuousTimeManager(timer, parameters.PlotterWidth); break;
				case PlotterType.Shiftting: timeManager = new ShiftingTimeManager(timer, parameters.PlotterWidth, parameters.PlotterTypeParameter); break;
				case PlotterType.Wrapping: timeManager = new WrappingTimeManager(timer, parameters.PlotterWidth, parameters.PlotterTypeParameter); break;
				default: throw new ArgumentOutOfRangeException("plotterType");
			}
			ValueManager valueManager;
			if (parameters.RangeLow == parameters.RangeHigh) valueManager = new FittingValueManager(graphs);
			else valueManager = new FixedValueManager(parameters.RangeLow, parameters.RangeHigh);
			plotter = new Plotter(graphs, drawer, timeManager, valueManager, layouter, parameters.Resolution, parameters.IntervalsX, parameters.IntervalsY, parameters.PlotterColor);

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

			Console.WriteLine("Starting render loop...");

			viewport.VSync = false;
			frameTimer.Elapsed += delegate(object sender, System.Timers.ElapsedEventArgs e) { drawFrame.Set(); };
			frameTimer.Start();

			Application.Idle += Application_Idle;
		}

		void Application_Idle(object sender, EventArgs e)
		{
			Application.DoEvents();

			while (viewport.IsIdle)
			{
				// TODO: Remove drawFrame AutoResetEvent
				drawFrame.WaitOne();

				drawer.Begin();

				plotter.Update();
				plotter.Draw();

				drawer.DrawNumber(fps, new Point(viewport.Right, viewport.Top), Color.Yellow, Plotting.TextAlignment.Far);

				drawer.End();

				if (++frames == frameWindow)
				{
					TimeSpan time = new TimeSpan(timer.Time);
					fps = frameWindow / (time - last).TotalSeconds;
					last = time;
					frames = 0;
				}
			}
		}

		private void streamsListView_ItemChecked(object sender, ItemCheckedEventArgs e)
		{
			ItemTag tag = (ItemTag)e.Item.Tag;
			tag.graph.IsDrawn = e.Item.Checked;
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
			if (exportCaptureFileDialog.ShowDialog() == DialogResult.OK)
				foreach (Port port in source.Ports)
					using (System.IO.StreamWriter streamWriter = new System.IO.StreamWriter(System.IO.Path.ChangeExtension(exportCaptureFileDialog.FileName, EscapeFilename(port.Name) + ".stream")))
						if (port.Streams.Any())
						{
							IEnumerable<IEnumerable<Entry>> streams =
							(
								from stream in port.Streams
								select stream.Container.Data
							)
							.ToArray();

							IEnumerable<IEnumerator<Entry>> enumerators =
							(
								from stream in streams
								select stream.GetEnumerator()
							)
							.ToArray();

							foreach (Entry leadEntry in streams.First())
								if (enumerators.All(enumerator => enumerator.MoveNext()))
								{
									StringBuilder stringBuilder = new StringBuilder();
									stringBuilder.Append(new TimeSpan(leadEntry.Time).TotalSeconds);
									stringBuilder.Append(" ");
									foreach (Entry entry in from enumerator in enumerators select enumerator.Current)
									{
										stringBuilder.Append(entry.Value);
										stringBuilder.Append(" ");
									}
									stringBuilder.Remove(stringBuilder.Length - 1, 1);
									streamWriter.WriteLine(stringBuilder.ToString());
								}
						}
		}
		private void clearDataToolStripMenuItem_Click(object sender, EventArgs e)
		{
			source.ClearData();
			timer.Reset();
		}
		private void freezeToolStripMenuItem_Click(object sender, EventArgs e)
		{
			plotter.Frozen = freezeToolStripMenuItem.Checked;
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
			menuStrip1.Visible = !minimalModeToolStripMenuItem.Checked;
			statusStrip1.Visible = !minimalModeToolStripMenuItem.Checked;
		}

		void NewSource(IEnumerable<string> ports)
		{
			DisposeSource();

			SetFilePath(null);
			timer.Reset();
			source = new Source();
			if (ports.Any())
				try { source = Capturing.Capture.Create(ports, timer, new Random()); }
				catch (InvalidOperationException e) { MessageBox.Show(e.Message, "Capture creation error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
				catch (Exception e)
				{
					if (e.InnerException.InnerException is DllNotFoundException)
						MessageBox.Show(e.InnerException.InnerException.Message, "Library not found", MessageBoxButtons.OK, MessageBoxIcon.Error);
					else throw;
				}
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
			graphs.Clear();
			streamsListView.Groups.Clear();
			streamsListView.Items.Clear();

			foreach (Port port in source.Ports)
			{
				ListViewGroup group = new ListViewGroup(port.Name);
				streamsListView.Groups.Add(group);
				foreach (Stream stream in port.Streams)
				{
					Graph graph = new Graph(plotter, stream);
					graphs.Add(graph);

					ListViewItem item = new ListViewItem();
					item.Name = port.Name + "Stream" + stream.Path;
					item.Text = "Stream " + stream.Path;
					item.Group = group;
					item.Checked = true;
					item.Tag = new ItemTag(stream, graph);

					SetColor(item, stream.Color);

					streamsListView.Items.Add(item);
				}
			}
		}
		void SetColor(ListViewItem item, Color color)
		{
			ItemTag tag = (ItemTag)item.Tag;

			tag.stream.Color = color;
			item.BackColor = color;
			item.ForeColor = item.BackColor.R * 0.299 + item.BackColor.G * 0.587 + item.BackColor.B * 0.114 >= 0x80 ? Color.Black : Color.White;
		}

		static string EscapeFilename(string filename)
		{
			foreach (char invalidChar in System.IO.Path.GetInvalidFileNameChars())
				filename = filename.Replace(invalidChar.ToString(), string.Empty);

			return filename;
		}

		// TODO: Remove on release
		private void gCCollectToolStripMenuItem_Click(object sender, EventArgs e)
		{
			GC.Collect();
		}
	}
}
