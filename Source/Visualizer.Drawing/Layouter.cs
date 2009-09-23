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
using Graphics;
using OpenTK.Math;

namespace Visualizer.Drawing
{
	public class Layouter
	{
		const int baseBorderLeft = 9;
		const int baseBorderRight = 0;
		const int baseBorderTop = 8;
		const int baseBorderBottom = 14;

		readonly Viewport viewport;

		public Vector2 this[double x, double y]
		{
			get
			{
				return new Vector2
				(
					(float)(Area.Left + x * Area.Width),
					(float)(Area.Bottom - y * Area.Height)
				);
			}
		}

		public Rectangle Area { get; private set; }
		public Matrix4 Transformation { get; private set; }

		public Layouter(Viewport viewport)
		{
			this.viewport = viewport;
		}

		public virtual void Update(int timeLabelsHeight, int valueLabelsWidth)
		{
			int borderLeft = baseBorderLeft + valueLabelsWidth;
			int borderRight = baseBorderRight;
			int borderTop = baseBorderTop;
			int borderBottom = baseBorderBottom + timeLabelsHeight;

			Rectangle clientArea = viewport.ClientRectangle;

			Area = new Rectangle
			(
				clientArea.Left + borderLeft,
				clientArea.Top + borderTop,
				clientArea.Width - borderLeft - borderRight,
				clientArea.Height - borderTop - borderBottom
			);

			Transformation = Matrix4.Scale(Area.Width, -Area.Height, 1) * Matrix4.CreateTranslation(Area.Left, Area.Bottom, 0);
		}
	}
}
