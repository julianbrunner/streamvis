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
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL;

namespace Graphics
{
	public class Viewport : GLControl
	{
		readonly List<IComponent> components = new List<IComponent>();

		bool initialized = false;

		public override Color BackColor
		{
			get { return base.BackColor; }
			set
			{
				base.BackColor = value;

				if (initialized) GL.ClearColor(base.BackColor);
			}
		}

		public Viewport() : base(new GraphicsMode(DisplayDevice.Default.BitsPerPixel, 0, 0, 0, 0, 2, false)) { }

		public void Initialize()
		{
			GL.ClearColor(base.BackColor);

			Layout += Viewport_Layout;
			Application.Idle += Application_Idle;

			initialized = true;
		}
		public void AddComponent(IComponent component)
		{
			components.Add(component);
		}

		void Viewport_Layout(object sender, LayoutEventArgs e)
		{
			GL.Viewport(ClientRectangle.X, ClientRectangle.Y, ClientRectangle.Width, ClientRectangle.Height);

			GL.MatrixMode(MatrixMode.Projection);
			GL.LoadIdentity();
			GL.Ortho(ClientRectangle.Left, ClientRectangle.Right, ClientRectangle.Bottom, ClientRectangle.Top, -1, 1);
		}
		void Application_Idle(object sender, EventArgs e)
		{
			Application.DoEvents();

			while (IsIdle)
			{
				GL.Clear(ClearBufferMask.ColorBufferBit);

				foreach (IUpdateable updateable in components.OfType<IUpdateable>())
					if (updateable.IsUpdated)
						updateable.Update();

				foreach (IDrawable drawable in components.OfType<IDrawable>())
					if (drawable.IsDrawn)
						drawable.Draw();

				SwapBuffers();
			}
		}
	}
}