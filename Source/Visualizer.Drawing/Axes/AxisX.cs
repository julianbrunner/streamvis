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
using System.Linq;
using Graphics;
using OpenTK.Math;
using Utility.Extensions;
using Utility.Utilities;
using Visualizer.Data;
using Visualizer.Drawing.Timing;

namespace Visualizer.Drawing.Axes
{
	public class AxisX : Axis
	{
		readonly TimeManager timeManager;

		protected override IEnumerable<double> Markers
		{
			get
			{
				TimeRange timeRange = timeManager.Range;

				if (timeRange.Range.IsEmpty()) yield break;

				IEnumerable<double> markers = DoubleUtility.GetMarkers(timeRange.Range.Start.Seconds, timeRange.Range.End.Seconds, MarkerCount);

				foreach (double time in markers) yield return time;
			}
		}

		public AxisX(Drawer drawer, Layouter layouter, TimeManager timeManager)
			: base(drawer, layouter)
		{
			this.timeManager = timeManager;
		}

		public override void Draw()
		{
			base.Draw();

			TimeRange timeRange = timeManager.Range;

			Vector2 offset = new Vector2(0, 0);

			Vector2 lineStart = Layouter.ForwardMap(Vector2.Zero) + offset;
			Vector2 lineEnd = Layouter.ForwardMap(Vector2.UnitX) + offset;

			Drawer.DrawLine(lineStart, lineEnd, Color, 1);

			// TODO: Remove the selector once TimeManager and ValueManager are unified
			foreach (Time time in Markers.Select(time => new Time(time)))
			{
				Vector2 markerStart = Layouter.ForwardMap((float)timeRange.ForwardMap(time) * Vector2.UnitX) + offset;
				Vector2 markerEnd = markerStart + new Vector2(0, 5);

				Drawer.DrawLine(markerStart, markerEnd, Color, 1);
				Drawer.DrawNumber(time.Seconds, markerEnd + new Vector2(0, 2), Color, TextAlignment.Center);
			}
		}
	}
}