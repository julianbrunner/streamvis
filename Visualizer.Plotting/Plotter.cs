using System.Collections.Generic;
using System.Drawing;
using Graphics;
using Visualizer.Data;

namespace Visualizer.Plotting
{
	public class Plotter : IComponent, IUpdateable, IDrawable
	{
		readonly Drawer drawer;
		readonly IEnumerable<Graph> graphs;
		readonly TimeManager timeManager;
		readonly DataManager dataManager;
		readonly ValueManager valueManager;
		readonly Layouter layouter;
		// TODO: Is this even used?
		readonly int resolution;
		readonly int intervalsX;
		readonly int intervalsY;
		readonly Color color;

		public bool IsUpdated { get; set; }
		public bool IsDrawn { get; set; }
		public TimeManager TimeManager { get { return timeManager; } }
		public DataManager DataManager { get { return dataManager; } }
		public ValueManager ValueManager { get { return valueManager; } }
		public Layouter Layouter { get { return layouter; } }
		public int Resolution { get { return resolution; } }
		public bool ExtendGraphs { get; set; }

		public Plotter(Drawer drawer, IEnumerable<Graph> graphs, TimeManager timeManager, DataManager dataManager, ValueManager valueManager, Layouter layouter, int resolution, int intervalsX, int intervalsY, Color color)
		{
			this.drawer = drawer;
			this.graphs = graphs;
			this.timeManager = timeManager;
			this.dataManager = dataManager;
			this.valueManager = valueManager;
			this.layouter = layouter;
			this.resolution = resolution;
			this.intervalsX = intervalsX;
			this.intervalsY = intervalsY;
			this.color = color;

			IsUpdated = true;
			IsDrawn = true;
		}

		public void Update()
		{
			if (IsUpdated)
			{
				timeManager.Update();
				dataManager.Update();
				valueManager.Update();
				layouter.Update();

				foreach (Graph graph in graphs) graph.Update();
			}
		}
		public void Draw()
		{
			if (IsDrawn)
			{
				foreach (Graph graph in graphs) graph.Draw();

				TimeRange timeRange = timeManager.Range;
				ValueRange valueRange = valueManager.Range;

				DrawAxisX(timeRange, valueRange);
				DrawAxisY(timeRange, valueRange);
			}
		}

		void DrawAxisX(TimeRange timeRange, ValueRange valueRange)
		{
			PointF start = layouter.TransformGraph(0, 0);
			PointF end = layouter.TransformGraph(1, 0);

			start.Y += 5;
			end.Y += 5;
			drawer.DrawLine(start, end, color, 1);

			Time width = timeRange.Range.End - timeRange.Range.Start;
			Time interval = width / intervalsX;
			Time offset = interval - timeRange.Range.Start % interval;

			if (width > Time.Zero)
				for (int i = 0; i < intervalsX + 1; i++)
				{
					Time time = offset + i * interval;
					PointF position = layouter.TransformGraph(time / width, 0);
					position.Y += 5;
					drawer.DrawLine(new PointF(position.X, position.Y + 5), position, color, 1);
					drawer.DrawNumber((timeRange.Range.Start + time).Seconds, new PointF(position.X, position.Y + 7), color, TextAlignment.Center);
				}
		}
		void DrawAxisY(TimeRange timeRange, ValueRange valueRange)
		{
			PointF start = layouter.TransformGraph(0, 0);
			PointF end = layouter.TransformGraph(0, 1);

			drawer.DrawLine(start, end, color, 1);

			double height = valueRange.Range.End - valueRange.Range.Start;
			double interval = height / intervalsY;

			if (height > 0)
				for (int i = 0; i < intervalsY + 1; i++)
				{
					double value = i * interval;
					PointF position = layouter.TransformGraph(0, value / height);
					drawer.DrawLine(new PointF(position.X - 5, position.Y), position, color, 1);
					drawer.DrawNumber(valueRange.Range.Start + value, new PointF(position.X - 7, position.Y - 5), color, TextAlignment.Far);
				}
		}
	}
}
