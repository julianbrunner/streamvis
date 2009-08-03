using System;
using System.Collections.Generic;
using System.Drawing;
using Graphics;

namespace Visualizer.Plotting
{
	public class Plotter
	{
		readonly IEnumerable<Graph> graphs;
		readonly Viewport viewport;
		readonly TimeManager timeManager;
		readonly ValueManager valueManager;
		readonly Layouter layouter;
		readonly int resolution;
		readonly int intervalsX;
		readonly int intervalsY;
		readonly Color color;

		public Viewport Viewport { get { return viewport; } }
		public TimeManager TimeManager { get { return timeManager; } }
		public ValueManager ValueManager { get { return valueManager; } }
		public Layouter Layouter { get { return layouter; } }
		public int Resolution { get { return resolution; } }
		public bool ExtendGraphs { get; set; }
		public bool Frozen { get; set; }
		public bool IsUpdated { get; set; }
		public bool IsDrawn { get; set; }

		public Plotter(IEnumerable<Graph> graphs, Viewport viewport, TimeManager timeManager, ValueManager valueManager, Layouter layouter, int resolution, int intervalsX, int intervalsY, Color color)
		{
			this.graphs = graphs;
			this.viewport = viewport;
			this.timeManager = timeManager;
			this.valueManager = valueManager;
			this.layouter = layouter;
			this.resolution = resolution;
			this.intervalsX = intervalsX;
			this.intervalsY = intervalsY;
			this.color = color;

			IsUpdated = true;
			IsDrawn = true;
		}

		public virtual void Update()
		{
			if (IsUpdated)
			{
				if (!Frozen) timeManager.Update();
				valueManager.Update();
				layouter.Update();

				foreach (Graph graph in graphs) graph.Update();
			}
		}
		public virtual void Draw()
		{
			if (IsDrawn)
			{
				foreach (Graph graph in graphs) graph.Draw();

				Range<long> timeRange = timeManager.Range;
				Range<double> valueRange = valueManager.Range;

				DrawAxisX(timeRange, valueRange);
				DrawAxisY(timeRange, valueRange);
			}
		}

		void DrawAxisX(Range<long> timeRange, Range<double> valueRange)
		{
			PointF start = layouter.TransformGraph(timeRange.Map(0), valueRange.Map(0));
			PointF end = layouter.TransformGraph(timeRange.Map(1), valueRange.Map(0));

			start.Y += 5;
			end.Y += 5;
			viewport.DrawLine(start, end, color, 1);

			long width = timeRange.End.Value - timeRange.Start.Value;
			long interval = width / intervalsX;
			long offset = interval - Modulo(timeRange.Start.Value, interval);

			if (width > 0)
				for (int i = 0; i < intervalsX + 1; i++)
				{
					long time = offset + i * interval;
					PointF position = layouter.TransformGraph(timeRange.Map((float)((double)time / (double)width)), valueRange.Map(0));
					position.Y += 5;
					viewport.DrawLine(new PointF(position.X, position.Y + 5), position, color, 1);
					viewport.DrawNumber(new TimeSpan(timeRange.Start.Value + time).TotalSeconds, new PointF(position.X, position.Y + 7), color, TextAlignment.Center);
				}
		}
		void DrawAxisY(Range<long> timeRange, Range<double> valueRange)
		{
			PointF start = layouter.TransformGraph(timeRange.Map(0), valueRange.Map(0));
			PointF end = layouter.TransformGraph(timeRange.Map(0), valueRange.Map(1));

			viewport.DrawLine(start, end, color, 1);

			double height = valueRange.End.Value - valueRange.Start.Value;
			double interval = height / intervalsY;

			if (height > 0)
				for (int i = 0; i < intervalsY + 1; i++)
				{
					double value = i * interval;
					PointF position = layouter.TransformGraph(timeRange.Map(0), valueRange.Map((float)(value / height)));
					viewport.DrawLine(new PointF(position.X - 5, position.Y), position, color, 1);
					viewport.DrawNumber(valueRange.Start.Value + value, new PointF(position.X - 7, position.Y - 5), color, TextAlignment.Far);
				}
		}

		static long Modulo(long a, long b)
		{
			long rest = a % b;
			if (rest <= 0) rest += b;
			return rest;
		}
	}
}
