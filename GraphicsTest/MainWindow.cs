using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;
using System.Drawing;
using Graphics;

namespace GraphicsTest
{
	class MainWindow : Form
	{
		IContainer components;
		Viewport viewport;
		
		List<List<PointF>> streams = new List<List<PointF>>(38);
		Random random = new Random();
		
		readonly Visualizer.Data.Timer timer = new Visualizer.Data.Timer();
		const int frameWindow = 20;
		int frames = 0;
		int totalFrames = 0;
		TimeSpan last = TimeSpan.Zero;
		double fps = 0;
		
		public MainWindow()
		{
			components = new Container();
			viewport = new Viewport();
			
			SuspendLayout();
			
			viewport.Dock = DockStyle.Fill;
			viewport.VSync = true;
			viewport.Size = new Size(500, 500);
			
			AutoScaleMode = AutoScaleMode.Font;
			Controls.Add(viewport);
			Text = "Graphics Test";
			
			ResumeLayout(false);
			
			for (int stream = 0; stream < 38; stream++)
			{
				List<PointF> points = new List<PointF>(1000);
				for (int i = 0; i < 1000; i++) points.Add(NextPoint());
				streams.Add(points);
			}
			
			Application.Idle += Application_Idle;
		}
		
		void Application_Idle(object sender, EventArgs e)
		{
			Application.DoEvents();

			while (viewport.IsIdle)
			{
				viewport.Begin();
				
				for (int stream = 0; stream < 38; stream++)
					viewport.DrawLineStrip(streams[stream], Color.White, 1.5f);
				
				viewport.DrawNumber(fps, new Point(viewport.Right, viewport.Top), Color.Yellow, TextAlignment.Far);
				viewport.DrawNumber(totalFrames, new Point(viewport.Right, viewport.Top + 20), Color.Yellow, TextAlignment.Far);
				
				viewport.End();
				
				totalFrames++;
				
				if (++frames == frameWindow)
				{
					TimeSpan time = new TimeSpan(timer.Time);
					fps = frameWindow / (time - last).TotalSeconds;
					last = time;
					frames = 0;
				}
			}
		}
		
		protected override void Dispose(bool disposing)
		{
			if (disposing && components != null) components.Dispose();
			
			base.Dispose(disposing);
		}
					
		double Next(double start, double end)
		{
			return start + random.NextDouble() * (end - start);
		}
		PointF NextPoint()
		{
			return new PointF((float)Next(viewport.Left, viewport.Right), (float)Next(viewport.Top, viewport.Bottom));
		}
//		Color NextColor()
//		{
//			return Color.FromArgb(random.Next(256), random.Next(256), random.Next(256));
//		}
	}
}
