using System;
using System.Collections.Generic;
using Visualizer.Data;

namespace Visualizer.Plotting
{
	public class ContinuousTimeManager : TimeManager
	{
		public override Range<TimeSpan> Range
		{
			get
			{
				return new Range<TimeSpan>
				(
					new Marker<TimeSpan>(Time - Width, 0),
					new Marker<TimeSpan>(Time, 1)
				);
			}
		}
		public override IEnumerable<Range<TimeSpan>> GraphRanges
		{
			get
			{
				yield return new Range<TimeSpan>
				(
					new Marker<TimeSpan>(Time - Width, 0),
					new Marker<TimeSpan>(Time, 1)
				);
			}
		}

		public ContinuousTimeManager(Timer timer, TimeSpan width) : base(timer, width) { }
	}
}
