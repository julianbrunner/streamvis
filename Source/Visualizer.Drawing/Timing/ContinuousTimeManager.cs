using System.Collections.Generic;
using Utility;
using Visualizer.Data;

namespace Visualizer.Drawing.Timing
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
