using System;
using System.Linq;
using System.Drawing;
using System.Collections.Generic;
using System.Windows.Forms;
using OpenTK;
using OpenTK.Graphics;

namespace Graphics
{
	public class Viewport : GLControl
	{
		readonly List<IComponent> components = new List<IComponent>();
		
		Color clearColor;
		
		public Color ClearColor
		{
			get { return clearColor; }
			set { GL.ClearColor(clearColor = value); }
		}

		public Viewport() : base(new GraphicsMode(DisplayDevice.Default.BitsPerPixel, 0, 0, 0, 0, 2, false))
		{			
			ClearColor = Color.Black;
			
			Application.Idle += Application_Idle;
		}

		public void AddComponent(IComponent component)
		{
			components.Add(component);
		}

		protected override void OnLayout(LayoutEventArgs e)
		{
			GL.Viewport(ClientRectangle.X, ClientRectangle.Y, ClientRectangle.Width, ClientRectangle.Height);

			GL.MatrixMode(MatrixMode.Projection);
			GL.LoadIdentity();
			Glu.Ortho2D(ClientRectangle.Left, ClientRectangle.Right, ClientRectangle.Bottom, ClientRectangle.Top);
			
			base.OnLayout(e);
		}
		
		void Application_Idle(object sender, EventArgs e)
		{
			Application.DoEvents();

			while (IsIdle)
			{
				GL.Clear(ClearBufferMask.ColorBufferBit);

				foreach (IUpdateable updateable in components.OfType<IUpdateable>()) updateable.Update();
				foreach (IDrawable drawable in components.OfType<IDrawable>()) drawable.Draw();

				SwapBuffers();
			}
		}
	}
}