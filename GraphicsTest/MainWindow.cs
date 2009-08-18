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
		readonly IContainer components;
		readonly Viewport viewport;

		readonly Random random = new Random();
		readonly List<List<PointF>> streams = new List<List<PointF>>(38);
		readonly FrameCounter frameCounter = new FrameCounter();
		
		int mode = 0;
		int height = 500;
				
		public MainWindow()
		{
			components = new Container();
			viewport = new Viewport();
			
			InitializeComponent();
			
			InitializeStreams();
			
			mode = 1;
			System.Console.WriteLine("Drawing Mode: Naïve DrawLineStrip");
			
			viewport.KeyDown += viewport_KeyDown;
			Application.Idle += Application_Idle;
		}

		protected override void Dispose(bool disposing)
		{
			if (disposing && components != null) components.Dispose();
			
			base.Dispose(disposing);
		}
		
		void InitializeComponent()
		{
			SuspendLayout();
			
			viewport.Dock = DockStyle.Fill;
			viewport.Size = new Size(1050, 550);
			viewport.VSync = false;
			
			AutoScaleMode = AutoScaleMode.Font;
			Controls.Add(viewport);
			Text = "Graphics Test";
			
			ClientSize = new Size(1050, 550);
			
			ResumeLayout(false);
		}
		void InitializeStreams()
		{
			streams.Clear();
			
			for (int stream = 0; stream < 38; stream++)
			{
				List<PointF> points = new List<PointF>(1000);
				for (int i = 0; i < 1000; i++) points.Add(new PointF(25 + ((viewport.Width - 50) / 1000f) * i, (viewport.Height - height) / 2 + (float)Next(0, height)));
				streams.Add(points);
			}
			
			viewport.InitializeStreams(streams);
		}
		
		void viewport_KeyDown(object sender, KeyEventArgs e)
		{
			switch (e.KeyCode)
			{
				case Keys.T: viewport.ToggleTexture2D(); viewport.PrintCapabilities(); break;
				case Keys.L: viewport.ToggleLineSmooth(); viewport.PrintCapabilities(); break;
				case Keys.B: viewport.ToggleBlend(); viewport.PrintCapabilities(); break;
				case Keys.D: viewport.ToggleDither(); viewport.PrintCapabilities(); break;
				case Keys.M: viewport.ToggleMultisample(); viewport.PrintCapabilities(); break;
				case Keys.D1: mode = 1; System.Console.WriteLine("Drawing Mode: Naïve DrawLineStrip"); break;
				case Keys.D2: mode = 2; System.Console.WriteLine("Drawing Mode: VertexArray"); break;
				case Keys.D3: mode = 3; System.Console.WriteLine("Drawing Mode: VertexArray in DisplayList"); break;
				case Keys.D4: mode = 4; System.Console.WriteLine("Drawing Mode: Vertex Buffer Object"); break;
				case Keys.D5: height -= 10; System.Console.WriteLine("Height: " + height); InitializeStreams(); break;
				case Keys.D6: height += 10; System.Console.WriteLine("Height: " + height); InitializeStreams(); break;
				case Keys.R: InitializeStreams(); break;
			}
		}
		void Application_Idle(object sender, EventArgs e)
		{
			Application.DoEvents();

			while (viewport.IsIdle)
			{
				viewport.Begin();
				viewport.DrawStreams(mode);
				viewport.End();
				
				frameCounter.Update();
				Text = "GraphicsTest - FPS : " + frameCounter.FramesPerSecond.ToString("F2");
			}
		}
					
		double Next(double start, double end)
		{
			return start + random.NextDouble() * (end - start);
		}
	}
}
