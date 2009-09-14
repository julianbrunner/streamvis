using System.Collections.Generic;
using Extensions;
using Visualizer.Data;

namespace Visualizer.Plotting
{
	public class ContinuousTimeManager : TimeManager
	{
		TimeRange range;

		public override TimeRange Range { get { return range; } }
		public override IEnumerable<TimeRange> GraphRanges { get { yield return range; } }

		public ContinuousTimeManager(Timer timer, Time width) : base(timer, width) { }

		public override void Update()
		{			
			base.Update();

			range = new TimeRange(new Range<Time>(Time - Width, Time));
		}
	}
}
