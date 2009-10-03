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

using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using Graphics;
using OpenTK.Math;
using Utility.Extensions;
using Utility.Utilities;
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
		readonly Layouter layouter;
		readonly TimeManager timeManager;
		readonly ValueManager valueManager;
		readonly DataManager dataManager;
		readonly int markersX;
		readonly int markersY;
		readonly Color color;

		public bool IsUpdated { get; set; }
		public bool IsDrawn { get; set; }
		// TODO: Move those to a new GraphParameters class
		public bool ExtendGraphs { get; set; }
		public double LineWidth { get; set; }

		public Diagram(Drawer drawer, IEnumerable<Graph> graphs, Layouter layouter, TimeManager timeManager, ValueManager valueManager, DataManager dataManager, int markersX, int markersY, Color color)
		{
			this.drawer = drawer;
			this.graphs = graphs;
			this.layouter = layouter;
			this.timeManager = timeManager;
			this.valueManager = valueManager;
			this.dataManager = dataManager;
			this.markersX = markersX;
			this.markersY = markersY;
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
				dataManager.Update();
				foreach (Graph graph in graphs) graph.Update();
				valueManager.Update();

				IEnumerable<Time> times = GetTimeMarkers(timeManager.Range, markersX);
				int maxHeight = times.Any() ? times.Max(time => drawer.GetTextSize(time.Seconds).Height) : 0;

				IEnumerable<double> values = GetValueMarkers(valueManager.Range, markersY);
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
			Vector2 offset = new Vector2(0, 0);

			Vector2 start = layouter.ForwardMap(Vector2.Zero) + offset;
			Vector2 end = layouter.ForwardMap(Vector2.UnitX) + offset;

			drawer.DrawLine(start, end, color, 1);

			foreach (Time time in GetTimeMarkers(timeRange, markersX))
			{
				Vector2 markerStart = layouter.ForwardMap((float)timeRange.ForwardMap(time) * Vector2.UnitX) + offset;
				Vector2 markerEnd = markerStart + new Vector2(0, 5);
				drawer.DrawLine(markerStart, markerEnd, color, 1);
				drawer.DrawNumber(time.Seconds, markerEnd + new Vector2(0, 2), color, TextAlignment.Center);
			}
		}
		void DrawAxisY(TimeRange timeRange, ValueRange valueRange)
		{
			Vector2 offset = new Vector2(0, 0);

			Vector2 start = layouter.ForwardMap(Vector2.Zero) + offset;
			Vector2 end = layouter.ForwardMap(Vector2.UnitY) + offset;

			drawer.DrawLine(start, end, color, 1);

			foreach (double value in GetValueMarkers(valueRange, markersY))
			{
				Vector2 markerStart = layouter.ForwardMap((float)valueRange.ForwardMap(value) * Vector2.UnitY) + offset;
				Vector2 markerEnd = markerStart + new Vector2(-5, 0);
				drawer.DrawLine(markerStart, markerEnd, color, 1);
				drawer.DrawNumber(value, markerEnd + new Vector2(-2, -6), color, TextAlignment.Far);
			}
		}

		static IEnumerable<Time> GetTimeMarkers(TimeRange timeRange, int count)
		{
			if (timeRange.Range.IsEmpty()) yield break;

			foreach (double time in DoubleUtility.GetMarkers(timeRange.Range.Start.Seconds, timeRange.Range.End.Seconds, count)) yield return new Time(time);
		}
		static IEnumerable<double> GetValueMarkers(ValueRange valueRange, int count)
		{
			if (valueRange.Range.IsEmpty()) yield break;

			foreach (double value in DoubleUtility.GetMarkers(valueRange.Range.Start, valueRange.Range.End, count)) yield return value;
		}
	}
}