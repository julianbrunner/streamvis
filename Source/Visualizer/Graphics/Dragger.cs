// Copyright © Julian Brunner 2009 - 2011

// This file is part of Stream Visualizer (streamvis).
// 
// Stream Visualizer is free software: you can redistribute it and/or modify it
// under the terms of the GNU General Public License as published by the Free
// Software Foundation, either version 3 of the License, or (at your option) any
// later version.
// 
// Stream Visualizer is distributed in the hope that it will be useful, but
// WITHOUT ANY WARRANTY; without even the implied warranty of MERCHANTABILITY or
// FITNESS FOR A PARTICULAR PURPOSE. See the GNU General Public License for more
// details.
// 
// You should have received a copy of the GNU General Public License along with
// Stream Visualizer. If not, see <http://www.gnu.org/licenses/>.

using System;
using System.Drawing;
using System.Windows.Forms;
using Graphics.Components;
using Krach;

namespace Graphics
{
	public class Dragger : IComponent
	{
		bool dragging;
		Point mousePosition;

		public event EventHandler<EventArgs<Point>> Drag;
		public event EventHandler BeginDrag;
		public event EventHandler EndDrag;

		public MouseButtons Button { get; set; }

		public Dragger(Viewport viewport)
		{
			Button = MouseButtons.Left;

			viewport.MouseDown += viewport_MouseDown;
			viewport.MouseUp += viewport_MouseUp;
			viewport.MouseMove += viewport_MouseMove;
		}

		protected virtual void OnDrag(Point offset)
		{
			if (Drag != null) Drag(this, new EventArgs<Point>(offset));
		}
		protected virtual void OnBeginDrag()
		{
			if (BeginDrag != null) BeginDrag(this, EventArgs.Empty);
		}
		protected virtual void OnEndDrag()
		{
			if (EndDrag != null) EndDrag(this, EventArgs.Empty);
		}

		void viewport_MouseDown(object sender, MouseEventArgs e)
		{
			if (e.Button == Button)
			{
				dragging = true;
				OnBeginDrag();
			}
		}
		void viewport_MouseUp(object sender, MouseEventArgs e)
		{
			if (e.Button == Button)
			{
				dragging = false;
				OnEndDrag();
			}
		}
		void viewport_MouseMove(object sender, MouseEventArgs e)
		{
			if (dragging) OnDrag(new Point(e.Location.X - mousePosition.X, e.Location.Y - mousePosition.Y));

			mousePosition = e.Location;
		}
	}
}
