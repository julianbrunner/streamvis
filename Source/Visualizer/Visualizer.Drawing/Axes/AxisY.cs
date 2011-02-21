// Copyright © Julian Brunner 2009 - 2010

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
using Krach.Basics;
using Krach.Maps.Scalar;
using Krach.Extensions;

namespace Visualizer.Drawing.Axes
{
	public class AxisY : Axis
	{
		protected override Range<double> MarkersRange { get { return Diagram.ValueManager.Mapping.Source; } }

		public AxisY(Drawer drawer, Diagram diagram) : base(drawer, diagram) { }

		public override void Draw()
		{
			SymmetricRangeMap valueMapping = Diagram.ValueManager.Mapping;

			Vector2 offset = new Vector2(0, 0);

			Vector2 lineStart = Diagram.Layouter.ForwardMap(Vector2.Zero) + offset;
			Vector2 lineEnd = Diagram.Layouter.ForwardMap(Vector2.UnitY) + offset;

			Drawer.DrawLine(lineStart, lineEnd, Color, 1);

			foreach (double value in Markers)
			{
				Vector2 markerStart = Diagram.Layouter.ForwardMap((float)valueMapping.Forward.Map(value) * Vector2.UnitY) + offset;
				Vector2 markerEnd = markerStart + new Vector2(-5, 0);

				Drawer.DrawLine(markerStart, markerEnd, Color, 1);
				Drawer.DrawNumber(value, markerEnd + new Vector2(-2, -6), Color, TextAlignment.Far);
			}
		}
	}
}
