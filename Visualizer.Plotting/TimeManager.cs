using System;
using System.Collections.Generic;
using Visualizer.Data;

namespace Visualizer.Plotting
{
	public abstract class TimeManager
	{
		readonly Timer timer;
		readonly TimeSpan width;

		protected TimeSpan Time { get; private set; }
		protected TimeSpan Width { get { return width; } }

		/// <summary>
		/// Gets the overall Range, in which graphs are drawn.
		/// </summary>
		public abstract Range<TimeSpan> Range { get; }
		/// <summary>
		/// Gets the specific sub Ranges, in which the graphs are drawn.
		/// </summary>
		public abstract IEnumerable<Range<TimeSpan>> GraphRanges { get; }

		protected TimeManager(Timer timer, TimeSpan width)
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
