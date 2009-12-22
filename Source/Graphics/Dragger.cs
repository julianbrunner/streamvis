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
using Utility;

namespace Graphics
{
	public class Dragger : IComponent, IUpdateable, IDrawable
	{
		readonly Drawer drawer;

		bool dragging;
		Point mousePosition;

		public event EventHandler<EventArgs<Point>> Drag;

		public bool IsUpdated { get; set; }
		public bool IsDrawn { get; set; }
		public MouseButtons Button { get; set; }

		public Dragger(Drawer drawer, Viewport viewport)
		{
			this.drawer = drawer;

			IsUpdated = true;
			IsDrawn = true;
			Button = MouseButtons.Left;

			viewport.MouseDown += viewport_MouseDown;
			viewport.MouseUp += viewport_MouseUp;
			viewport.MouseMove += viewport_MouseMove;
		}

		public void Update() { }
		public void Draw() { }

		protected virtual void OnDrag(Point offset)
		{
			if (Drag != null) Drag(this, new EventArgs<Point>(offset));
		}

		void viewport_MouseDown(object sender, MouseEventArgs e)
		{
			if (e.Button == Button) dragging = true;
		}
		void viewport_MouseUp(object sender, MouseEventArgs e)
		{
			if (e.Button == Button) dragging = false;
		}
		void viewport_MouseMove(object sender, MouseEventArgs e)
		{
			if (dragging) OnDrag(new Point(e.Location.X - mousePosition.X, e.Location.Y - mousePosition.Y));

			mousePosition = e.Location;
		}
	}
}
