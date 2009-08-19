using System;
using System.Diagnostics;

namespace Visualizer.Data
{
	public class Timer
	{
		readonly Stopwatch stopwatch = new Stopwatch();

		public TimeSpan Time { get { lock (stopwatch) return stopwatch.Elapsed; } }

		public Timer()
		{
			stopwatch.Reset();
			stopwatch.Start();
		}

		public void Reset()
		{
			stopwatch.Reset();
			stopwatch.Start();
		}
	}
}
