using System.Collections.Generic;
using Extensions;
using Visualizer.Data;

namespace Visualizer.Plotting
{
	public class ShiftingTimeManager : TimeManager
	{
		readonly double shiftLength;

		TimeRange range;
		TimeRange graphRange;

		public override TimeRange Range { get { return range; } }
		public override IEnumerable<TimeRange> GraphRanges { get { yield return graphRange; } }

		public ShiftingTimeManager(Timer timer, Time width, double shiftLength)
			: base(timer, width)
		{
			this.shiftLength = shiftLength;
		}

		public override void Update()
		{
			base.Update();

			Time interval = Width * shiftLength;

			double intervals = Time / interval;
			int wholeIntervals = (int)intervals;
			double fractionalIntervals = intervals - wholeIntervals;

			Time startTime = interval * (wholeIntervals + 1) - Width;
			double startPosition = 0;
			Time endTime = Time;
			double endPosition = (1 - shiftLength) + shiftLength * fractionalIntervals;

			range = new TimeRange(new Range<Time>(startTime, startTime + Width));
			graphRange = new TimeRange(new Range<Time>(startTime, endTime), new Range<double>(startPosition, endPosition));
		}
	}
}
