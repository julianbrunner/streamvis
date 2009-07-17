using System;
using System.Collections.Generic;
using Visualizer.Data;

namespace Visualizer.Plotting
{
	public abstract class TimeManager
	{
		readonly Timer timer;
		readonly long width;

		protected long Time { get; private set; }
		protected long Width { get { return width; } }

		public abstract Range<long> Range { get; }
		public abstract IEnumerable<Range<long>> GraphRanges { get; }

		protected TimeManager(Timer timer, long width)
		{
			this.timer = timer;
			this.width = width;
		}

		public virtual void Update()
		{
			Time = timer.Time;
		}
	}
}
