using System;
using System.Diagnostics;

namespace Graphics
{
	public class FrameCounter : IComponent, IUpdateable
	{
		const int frameWindow = 20;
		
		readonly Stopwatch stopwatch;
		
		TimeSpan lastOverflow = TimeSpan.Zero;
		int frames = 0;
		
		public double FramesPerSecond { get; private set; }
		
		public FrameCounter()
		{			
			stopwatch = new Stopwatch();
			stopwatch.Reset();
			stopwatch.Start();
		}
		
		public void Update()
		{
			if (++frames == frameWindow)
			{
				TimeSpan time =  stopwatch.Elapsed;
				FramesPerSecond = frameWindow / (time - lastOverflow).TotalSeconds;
				lastOverflow = time;
				frames = 0;
			}
		}
	}
}
