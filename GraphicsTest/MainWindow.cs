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
		
		Random random = new Random();
		List<List<PointF>> streams = new List<List<PointF>>(38);
		int mode = 0;
		
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
			viewport.VSync = false;
			
			AutoScaleMode = AutoScaleMode.Font;
			Controls.Add(viewport);
			Text = "Graphics Test";
			
			ClientSize = new Size(1050, 550);
			
			ResumeLayout(false);
			
			for (int stream = 0; stream < 38; stream++)
			{
				List<PointF> points = new List<PointF>(1000);
				for (int i = 0; i < 1000; i++) points.Add(new PointF(25 + i / 1.0f, 25 + (float)Next(0, 500)));
				streams.Add(points);
			}
			
			viewport.InitializeStreams(streams);
			
			mode = 1;
			System.Console.WriteLine("Drawing Mode: Naïve DrawLineStrip");
			
			viewport.KeyDown += viewport_KeyDown;
			Application.Idle += Application_Idle;
		}

		void viewport_KeyDown(object sender, KeyEventArgs e)
		{
			switch (e.KeyData)
			{
				case Keys.T: viewport.ToggleTexture2D(); viewport.PrintCapabilities(); break;
				case Keys.L: viewport.ToggleLineSmooth(); viewport.PrintCapabilities(); break;
				case Keys.B: viewport.ToggleBlend(); viewport.PrintCapabilities(); break;
				case Keys.D: viewport.ToggleDither(); viewport.PrintCapabilities(); break;
				case Keys.M: viewport.ToggleMultisample(); viewport.PrintCapabilities(); break;
				case Keys.D1: mode = 1; System.Console.WriteLine("Drawing Mode: Naïve DrawLineStrip"); break;
				case Keys.D2: mode = 2; System.Console.WriteLine("Drawing Mode: VertexArray"); break;
				case Keys.D3: mode = 3; System.Console.WriteLine("Drawing Mode: VertexArray in DisplayList"); break;
			}
		}
		void Application_Idle(object sender, EventArgs e)
		{
			Application.DoEvents();

			while (viewport.IsIdle)
			{
				viewport.Begin();
				
				for (int stream = 0; stream < 38; stream++)
					viewport.DrawStream(mode, streams[stream], stream);
				
				viewport.DrawNumber(fps, new Point(viewport.Right, viewport.Top), Color.Yellow, TextAlignment.Far);
				viewport.DrawNumber(totalFrames, new Point(viewport.Right - 50, viewport.Top), Color.Yellow, TextAlignment.Far);
				
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
//		PointF NextPoint()
//		{
//			return new PointF((float)Next(viewport.Left, viewport.Right), (float)Next(viewport.Top, viewport.Bottom));
//		}
//		Color NextColor()
//		{
//			return Color.FromArgb(random.Next(256), random.Next(256), random.Next(256));
//		}
	}
}
