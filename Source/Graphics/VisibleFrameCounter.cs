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

using System.Drawing;
using OpenTK.Math;

namespace Graphics
{
	public class VisibleFrameCounter : FrameCounter, IDrawable
	{
		readonly Drawer drawer;
		readonly Color color;
		readonly TextAlignment alignment;

		public bool IsDrawn { get; set; }
		public Vector2 Position { get; set; }

		public VisibleFrameCounter(Drawer drawer, Color color, TextAlignment alignment)
		{
			this.drawer = drawer;
			this.color = color;
			this.alignment = alignment;

			Position = Vector2.Zero;
			IsDrawn = true;
		}

		public void Draw()
		{
			if (IsDrawn) drawer.DrawNumber(FramesPerSecond, Position, color, alignment);
		}
	}
}
