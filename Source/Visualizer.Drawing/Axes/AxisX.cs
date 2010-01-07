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
using Graphics;
using OpenTK;
using Utility;
using Utility.Extensions;
using Utility.Utilities;

namespace Visualizer.Drawing.Axes
{
	public class AxisX : Axis
	{
		protected override IEnumerable<double> Markers
		{
			get
			{
				LinearMapping timeMapping = Diagram.TimeManager.Mapping;

				if (timeMapping.Input.IsEmpty()) yield break;

				IEnumerable<double> markers = DoubleUtility.GetMarkers(timeMapping.Input.Start, timeMapping.Input.End, MarkerCount);

				foreach (double time in markers) yield return time;
			}
		}

		public AxisX(Drawer drawer, Diagram diagram) : base(drawer, diagram) { }

		public override void Draw()
		{
			base.Draw();

			LinearMapping timeMapping = Diagram.TimeManager.Mapping;

			Vector2 offset = new Vector2(0, 0);

			Vector2 lineStart = Diagram.Layouter.ForwardMap(Vector2.Zero) + offset;
			Vector2 lineEnd = Diagram.Layouter.ForwardMap(Vector2.UnitX) + offset;

			Drawer.DrawLine(lineStart, lineEnd, Color, 1);

			foreach (double time in Markers)
			{
				Vector2 markerStart = Diagram.Layouter.ForwardMap((float)timeMapping.ForwardMap(time) * Vector2.UnitX) + offset;
				Vector2 markerEnd = markerStart + new Vector2(0, 5);

				Drawer.DrawLine(markerStart, markerEnd, Color, 1);
				Drawer.DrawNumber(time, markerEnd + new Vector2(0, 2), Color, TextAlignment.Center);
			}
		}
	}
}