using System.Diagnostics;

namespace Visualizer.Data
{
	public class Timer
	{
		readonly Stopwatch stopwatch = new Stopwatch();

		public Time Time { get { lock (stopwatch) return new Time(stopwatch.Elapsed.Ticks); } }

		public Timer()
		{
			Reset();
		}

		public void Reset()
		{
			lock (stopwatch)
			{
				stopwatch.Reset();
				stopwatch.Start();
			}
		}
	}
}
