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

using System;
using System.Drawing;
using System.Windows.Forms;
using Graphics.Components;
using Krach;
using OpenTK;

namespace Graphics
{
	public class RectangleSelector : IComponent, IDrawable
	{
		readonly Drawer drawer;

		float width = 0;
		bool selecting;
		Point startPosition;
		Point mousePosition;

		public event EventHandler<EventArgs<Point>> BeginSelect;
		public event EventHandler<EventArgs<Rectangle>> EndSelect;

		public bool IsDrawn { get; set; }
		public MouseButtons Button { get; set; }
		public Color Color { get; set; }
		public float Width
		{
			get { return width; }
			set
			{
				if (value <= 0) throw new ArgumentOutOfRangeException("value");

				width = value;
			}
		}

		public RectangleSelector(Drawer drawer, Viewport viewport)
		{
			this.drawer = drawer;

			IsDrawn = true;
			Button = MouseButtons.Left;
			Color = Color.White;
			Width = 1;

			viewport.MouseDown += viewport_MouseDown;
			viewport.MouseUp += viewport_MouseUp;
			viewport.MouseMove += viewport_MouseMove;
		}

		public void Draw()
		{
			if (selecting)
			{
				drawer.DrawLine(new Vector2(startPosition.X, startPosition.Y), new Vector2(mousePosition.X, startPosition.Y), Color, Width);
				drawer.DrawLine(new Vector2(mousePosition.X, startPosition.Y), new Vector2(mousePosition.X, mousePosition.Y), Color, Width);
				drawer.DrawLine(new Vector2(mousePosition.X, mousePosition.Y), new Vector2(startPosition.X, mousePosition.Y), Color, Width);
				drawer.DrawLine(new Vector2(startPosition.X, mousePosition.Y), new Vector2(startPosition.X, startPosition.Y), Color, Width);
			}
		}
		public void Abort()
		{
			selecting = false;
		}

		protected virtual void OnBeginSelect(Point startPosition)
		{
			if (BeginSelect != null) BeginSelect(this, new EventArgs<Point>(startPosition));
		}
		protected virtual void OnEndSelect(Rectangle selection)
		{
			if (EndSelect != null) EndSelect(this, new EventArgs<Rectangle>(selection));
		}

		void viewport_MouseDown(object sender, MouseEventArgs e)
		{
			if (e.Button == Button && !selecting)
			{
				selecting = true;

				startPosition = mousePosition = e.Location;

				OnBeginSelect(startPosition);
			}
		}
		void viewport_MouseUp(object sender, MouseEventArgs e)
		{
			if (e.Button == Button && selecting)
			{
				selecting = false;

				Rectangle selection = new Rectangle(startPosition, new Size(mousePosition.X - startPosition.X, mousePosition.Y - startPosition.Y));

				OnEndSelect(Absolute(selection));
			}
		}
		void viewport_MouseMove(object sender, MouseEventArgs e)
		{
			mousePosition = e.Location;
		}

		static Rectangle Absolute(Rectangle rectangle)
		{
			int x1 = Math.Min(rectangle.Left, rectangle.Right);
			int y1 = Math.Min(rectangle.Top, rectangle.Bottom);
			int x2 = Math.Max(rectangle.Left, rectangle.Right);
			int y2 = Math.Max(rectangle.Top, rectangle.Bottom);

			return new Rectangle(x1, y1, x2 - x1, y2 - y1);
		}
	}
}
