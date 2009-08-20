using System;
using System.Collections.Generic;
using Visualizer.Data;

namespace Visualizer.Plotting
{
	public class ContinuousTimeManager : TimeManager
	{
		public override Range<Time> Range
		{
			get
			{
				return new Range<Time>
				(
					new Marker<Time>(Time - Width, 0),
					new Marker<Time>(Time, 1)
				);
			}
		}
		public override IEnumerable<Range<Time>> GraphRanges
		{
			get
			{
				yield return new Range<Time>
				(
					new Marker<Time>(Time - Width, 0),
					new Marker<Time>(Time, 1)
				);
			}
		}

		public ContinuousTimeManager(Timer timer, Time width) : base(timer, width) { }
	}
}
