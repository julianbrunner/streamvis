﻿// Copyright © Julian Brunner 2009

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

using System.Collections.Generic;
using System.Drawing;
using Graphics;
using Utility;
using Visualizer.Data;
using Visualizer.Drawing.Data;
using Visualizer.Drawing.Timing;
using Visualizer.Drawing.Values;

namespace Visualizer.Drawing
{
	public class Diagram : IComponent, IUpdateable, IDrawable
	{
		readonly Drawer drawer;
		readonly IEnumerable<Graph> graphs;
		readonly TimeManager timeManager;
		readonly SegmentManager segmentManager;
		readonly ValueManager valueManager;
		readonly Layouter layouter;
		readonly int intervalsX;
		readonly int intervalsY;
		readonly Color color;

		public bool IsUpdated { get; set; }
		public bool IsDrawn { get; set; }

		public Diagram(Drawer drawer, IEnumerable<Graph> graphs, TimeManager timeManager, SegmentManager segmentManager, ValueManager valueManager, Layouter layouter, int intervalsX, int intervalsY, Color color)
		{
			this.drawer = drawer;
			this.graphs = graphs;
			this.timeManager = timeManager;
			this.segmentManager = segmentManager;
			this.valueManager = valueManager;
			this.layouter = layouter;
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
				// TODO: This looks random
				layouter.Update();
				timeManager.Update();
				foreach (Graph graph in graphs) graph.Update();
				segmentManager.Update();
				valueManager.Update();
			}
		}
		public void Draw()
		{
			if (IsDrawn)
			{
				TimeRange timeRange = timeManager.Range;
				ValueRange valueRange = valueManager.Range;

				if (!timeRange.Range.IsEmpty() && !valueManager.Range.Range.IsEmpty())
					foreach (Graph graph in graphs)
						graph.Draw();

				DrawAxisX(timeRange, valueRange);
				DrawAxisY(timeRange, valueRange);
			}
		}

		void DrawAxisX(TimeRange timeRange, ValueRange valueRange)
		{
			PointF start = layouter[0, 0];
			PointF end = layouter[1, 0];

			start.Y += 5;
			end.Y += 5;
			drawer.DrawLine(start, end, color, 1);

			foreach (Time time in GetTimes(timeRange, intervalsX))
			{
				PointF position = layouter[timeRange[time], 0];
				position.Y += 5;
				drawer.DrawLine(new PointF(position.X, position.Y + 5), position, color, 1);
				drawer.DrawNumber(time.Seconds, new PointF(position.X, position.Y + 7), color, TextAlignment.Center);
			}
		}
		void DrawAxisY(TimeRange timeRange, ValueRange valueRange)
		{
			PointF start = layouter[0, 0];
			PointF end = layouter[0, 1];

			drawer.DrawLine(start, end, color, 1);

			foreach (double value in GetValues(valueRange, intervalsY))
			{
				PointF position = layouter[0, valueRange[value]];
				drawer.DrawLine(new PointF(position.X - 5, position.Y), position, color, 1);
				drawer.DrawNumber(value, new PointF(position.X - 7, position.Y - 5), color, TextAlignment.Far);
			}
		}

		static IEnumerable<Time> GetTimes(TimeRange timeRange, int count)
		{
			Time width = timeRange.Range.End - timeRange.Range.Start;
			Time interval = width / count;
			Time offset = timeRange.Range.Start + interval - timeRange.Range.Start % interval;

			if (width == Time.Zero) yield break;

			for (int i = 0; i < count + 1; i++) yield return offset + i * interval;
		}
		static IEnumerable<double> GetValues(ValueRange valueRange, int count)
		{
			double height = valueRange.Range.End - valueRange.Range.Start;
			double interval = height / count;
			double offset = valueRange.Range.Start;

			if (height == 0) yield break;

			for (int i = 0; i < count + 1; i++) yield return offset + i * interval;
		}
	}
}
