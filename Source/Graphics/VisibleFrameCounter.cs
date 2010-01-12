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

using System.Drawing;
using OpenTK;

namespace Graphics
{
	public class VisibleFrameCounter : FrameCounter, IDrawable
	{
		readonly Drawer drawer;

		public bool IsDrawn { get; set; }
		public Color Color { get; set; }
		public TextAlignment Alignment { get; set; }
		public Vector2 Position { get; set; }

		public VisibleFrameCounter(Drawer drawer)
		{
			this.drawer = drawer;

			IsDrawn = true;
			Color = Color.White;
			Alignment = TextAlignment.Near;
			Position = Vector2.Zero;
		}

		public void Draw()
		{
			drawer.DrawNumber(FramesPerSecond, Position, Color, Alignment);
		}
	}
}
