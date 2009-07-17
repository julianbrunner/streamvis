using System;
using System.Collections.Generic;
using Visualizer.Data;

namespace Visualizer.Plotting
{
	public class ContinuousTimeManager : TimeManager
	{
		public override Range<long> Range
		{
			get
			{
				return new Range<long>
				(
					new Marker<long>(Time - Width, 0),
					new Marker<long>(Time, 1)
				);
			}
		}
		public override IEnumerable<Range<long>> GraphRanges
		{
			get
			{
				yield return new Range<long>
				(
					new Marker<long>(Time - Width, 0),
					new Marker<long>(Time, 1)
				);
			}
		}

		public ContinuousTimeManager(Timer timer, long width) : base(timer, width) { }
	}
}
