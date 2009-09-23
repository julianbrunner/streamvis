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
using Graphics;
using OpenTK.Math;
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
				timeManager.Update();
				foreach (Graph graph in graphs) graph.Update();
				segmentManager.Update();
				valueManager.Update();

				IEnumerable<Time> times = GetTimes(timeManager.Range, intervalsX);
				int maxHeight = times.Any() ? times.Max(time => drawer.GetTextSize(time.Seconds).Height) : 0;

				IEnumerable<double> values = GetValues(valueManager.Range, intervalsY);
				int maxWidth = values.Any() ? values.Max(value => drawer.GetTextSize(value).Width) : 0;

				layouter.Update(maxHeight, maxWidth);
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
			Vector2 offset = new Vector2(0, 5);

			Vector2 start = layouter[0, 0] + offset;
			Vector2 end = layouter[1, 0] + offset;

			drawer.DrawLine(start, end, color, 1);

			foreach (Time time in GetTimes(timeRange, intervalsX))
			{
				Vector2 markerStart = layouter[timeRange[time], 0] + offset;
				Vector2 markerEnd = markerStart + new Vector2(0, 5);
				drawer.DrawLine(markerStart, markerEnd, color, 1);
				drawer.DrawNumber(time.Seconds, markerEnd + new Vector2(0, 2), color, TextAlignment.Center);
			}
		}
		void DrawAxisY(TimeRange timeRange, ValueRange valueRange)
		{
			Vector2 offset = new Vector2(0, 0);

			Vector2 start = layouter[0, 0] + offset;
			Vector2 end = layouter[0, 1] + offset;

			drawer.DrawLine(start, end, color, 1);

			foreach (double value in GetValues(valueRange, intervalsY))
			{
				Vector2 markerStart = layouter[0, valueRange[value]] + offset;
				Vector2 markerEnd = markerStart + new Vector2(-5, 0);
				drawer.DrawLine(markerStart, markerEnd, color, 1);
				drawer.DrawNumber(value, markerEnd + new Vector2(-2, -6), color, TextAlignment.Far);
			}
		}

		static IEnumerable<Time> GetTimes(TimeRange timeRange, int count)
		{
			if (timeRange.Range.IsEmpty()) yield break;

			foreach (double time in GetMarkers(timeRange.Range.Start.Seconds, timeRange.Range.End.Seconds, count)) yield return new Time(time);
		}
		static IEnumerable<double> GetValues(ValueRange valueRange, int count)
		{
			if (valueRange.Range.IsEmpty()) yield break;

			foreach (double value in GetMarkers(valueRange.Range.Start, valueRange.Range.End, count)) yield return value;
		}
		static IEnumerable<double> GetMarkers(double start, double end, int count)
		{
			double difference = end - start;
			int magnitude = (int)Math.Floor(Math.Log10(difference));
			double rawIntervalLength = difference * Math.Pow(10, -magnitude) / count;
			double intervalLength = rawIntervalLength.FractionRound() * Math.Pow(10, magnitude);

			start = start.Ceiling(intervalLength);
			end = end.Floor(intervalLength);

			for (double value = start; value <= end; value += intervalLength) yield return value;
		}
	}
}