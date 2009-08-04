using System;
using System.ComponentModel;
using System.Windows.Forms;
using Graphics;

namespace GraphicsTest
{
	class MainWindow : Form
	{
		IContainer components;
		Viewport viewport;
		
		public MainWindow()
		{
			components = new Container();
			viewport = new Viewport();
			
			SuspendLayout();
			
			viewport.Dock = DockStyle.Fill;
			viewport.VSync = true;
			
			AutoScaleMode = AutoScaleMode.Font;
			Controls.Add(viewport);
			Text = "Graphics Test";
			
			ResumeLayout(false);
			
			Application.Idle += Application_Idle;
		}
		
		void Application_Idle(object sender, EventArgs e)
		{
			Application.DoEvents();

			while (viewport.IsIdle)
			{
				viewport.Begin();
				viewport.End();
			}
		}
		
		protected override void Dispose(bool disposing)
		{
			if (disposing && components != null) components.Dispose();
			
			base.Dispose(disposing);
		}
	}
}
