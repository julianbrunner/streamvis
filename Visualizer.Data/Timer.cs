using System;
using System.Diagnostics;

namespace Visualizer.Data
{
	public class Timer
	{
		readonly Stopwatch stopwatch = new Stopwatch();

		public Time Time { get { lock (stopwatch) return new Time(stopwatch.Elapsed.Ticks); } }

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
