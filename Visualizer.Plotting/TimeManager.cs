using System.Collections.Generic;
using Visualizer.Data;

namespace Visualizer.Plotting
{
	public abstract class TimeManager
	{
		readonly Timer timer;
		readonly Time width;

		protected Time Time { get; private set; }
		protected Time Width { get { return width; } }

		public bool Frozen { get; set; }
		/// <summary>
		/// Gets the overall Range, in which graphs are drawn.
		/// </summary>
		public abstract _Range<Time> Range { get; }
		/// <summary>
		/// Gets the specific sub Ranges, in which the graphs are drawn.
		/// </summary>
		public abstract IEnumerable<_Range<Time>> GraphRanges { get; }

		protected TimeManager(Timer timer, Time width)
		{
			this.timer = timer;
			this.width = width;
		}

		public virtual void Update()
		{
			if (!Frozen) Time = timer.Time;
		}
	}
}
