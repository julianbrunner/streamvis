using System;
using System.Collections.Generic;
using Visualizer.Data;

namespace Visualizer.Plotting
{
	public class ShiftingTimeManager : TimeManager
	{
		readonly double shiftLength;

		Range<TimeSpan> range;
		Range<TimeSpan> graphRange;

		public override Range<TimeSpan> Range { get { return range; } }
		public override IEnumerable<Range<TimeSpan>> GraphRanges { get { yield return graphRange; } }

		public ShiftingTimeManager(Timer timer, TimeSpan width, double shiftLength)
			: base(timer, width)
		{
			this.shiftLength = shiftLength;
		}

		public override void Update()
		{
			base.Update();

			TimeSpan interval = new TimeSpan((long)(Width.Ticks * shiftLength));

			double intervals = (double)Time.Ticks / (double)interval.Ticks;
			int wholeIntervals = (int)intervals;
			double fractionalIntervals = intervals - wholeIntervals;

			TimeSpan startTime = new TimeSpan(interval.Ticks * (wholeIntervals + 1)) - Width;
			float startPosition = 0;
			TimeSpan endTime = Time;
			float endPosition = (float)((1 - shiftLength) + shiftLength * fractionalIntervals);

			graphRange = new Range<TimeSpan>
			(
				new Marker<TimeSpan>(startTime, startPosition),
				new Marker<TimeSpan>(endTime, endPosition)
			);

			range = new Range<TimeSpan>
			(
				new Marker<TimeSpan>(startTime, 0),
				new Marker<TimeSpan>(startTime + Width, 1)
			);
		}
	}
}
