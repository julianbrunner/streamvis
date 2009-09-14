using System.Collections.Generic;
using Utility;
using Visualizer.Data;

namespace Visualizer.Plotting
{
	public class WrappingTimeManager : TimeManager
	{
		readonly double gap;

		TimeRange range;
		IEnumerable<TimeRange> graphRanges;

		public override TimeRange Range { get { return range; } }
		public override IEnumerable<TimeRange> GraphRanges { get { return graphRanges; } }

		public WrappingTimeManager(Timer timer, Time width, double gap)
			: base(timer, width)
		{
			this.gap = gap;
		}

		public override void Update()
		{
			base.Update();

			double intervals = Time / Width;
			int wholeIntervals = (int)intervals;
			double fractionalIntervals = intervals - wholeIntervals;

			Time startTime = Time - (1 - gap) * Width;
			double startPosition = (fractionalIntervals + gap) % 1;
			Time endTime = Time;
			double endPosition = startPosition + (1 - gap);

			range = new TimeRange(new Range<Time>((wholeIntervals + 0) * Width, (wholeIntervals + 1) * Width));

			if (startTime >= wholeIntervals * Width)
				graphRanges = new TimeRange[]
				{
					new TimeRange(new Range<Time>(startTime, endTime), new Range<double>(startPosition,endPosition))
				};
			else
				graphRanges = new TimeRange[]
				{
					new TimeRange(new Range<Time>(startTime, wholeIntervals * Width), new Range<double>(startPosition, 1)),
					new TimeRange(new Range<Time>(wholeIntervals * Width, endTime), new Range<double>(0, endPosition - 1))
				};
		}
	}
}
