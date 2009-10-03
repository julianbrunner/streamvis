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
using System.Drawing;
using System.Windows.Forms;
using Graphics;
using OpenTK.Math;

namespace Visualizer.Drawing
{
	public class Layouter
	{
		readonly Viewport viewport;

		public Padding BaseMargin { get; set; }
		public Rectangle Area { get; private set; }
		public Matrix4 Transformation { get; private set; }

		public Layouter(Viewport viewport)
		{
			this.viewport = viewport;
		}

		public Vector2 ForwardMap(Vector2 source)
		{
			if (source.X < 0 || source.X > 1 || source.Y < 0 || source.Y > 1) throw new ArgumentOutOfRangeException("source");

			return new Vector2
			(
				Area.Left + source.X * Area.Width,
				Area.Bottom - source.Y * Area.Height
			);
		}
		public Vector2 ReverseMap(Vector2 source)
		{
			if (source.X < Area.Left || source.X > Area.Right || source.Y < Area.Top || source.Y > Area.Bottom) throw new ArgumentOutOfRangeException("source");

			return new Vector2
			(
				(source.X - Area.Left) / Area.Width,
				-((source.Y - Area.Bottom) / Area.Height)
			);
		}
		public virtual void Update(int valueLabelsWidth, int timeLabelsHeight)
		{
			int borderLeft = BaseMargin.Left + valueLabelsWidth;
			int borderRight = BaseMargin.Right;
			int borderTop = BaseMargin.Top;
			int borderBottom = BaseMargin.Bottom + timeLabelsHeight;

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
