using System;
using System.Collections.Generic;
using Visualizer.Data;

namespace Visualizer.Plotting
{
	public class ShiftingTimeManager : TimeManager
	{
		readonly double shiftLength;

		Range<long> range;
		Range<long> graphRange;

		public override Range<long> Range { get { return range; } }
		public override IEnumerable<Range<long>> GraphRanges { get { yield return graphRange; } }

		public ShiftingTimeManager(Timer timer, long width, double shiftLength)
			: base(timer, width)
		{
			this.shiftLength = shiftLength;
		}

		public override void Update()
		{
			base.Update();

			long interval = (long)(Width * shiftLength);

			double intervals = (double)Time / (double)interval;
			int wholeIntervals = (int)intervals;
			double fractionalIntervals = intervals - wholeIntervals;

			long startTime = interval * (wholeIntervals + 1) - Width;
			float startPosition = 0;
			long endTime = Time;
			float endPosition = (float)((1 - shiftLength) + shiftLength * fractionalIntervals);

			graphRange = new Range<long>
			(
				new Marker<long>(startTime, startPosition),
				new Marker<long>(endTime, endPosition)
			);

			range = new Range<long>
			(
				new Marker<long>(startTime, 0),
				new Marker<long>(startTime + Width, 1)
			);
		}
	}
}
