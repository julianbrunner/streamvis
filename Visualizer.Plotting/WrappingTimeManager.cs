using System;
using System.Collections.Generic;
using Visualizer.Data;

namespace Visualizer.Plotting
{
	public class WrappingTimeManager : TimeManager
	{
		readonly double gap;

		Range<TimeSpan> range;
		IEnumerable<Range<TimeSpan>> graphRanges;

		public override Range<TimeSpan> Range { get { return range; } }
		public override IEnumerable<Range<TimeSpan>> GraphRanges { get { return graphRanges; } }

		public WrappingTimeManager(Timer timer, TimeSpan width, double gap)
			: base(timer, width)
		{
			this.gap = gap;
		}

		public override void Update()
		{
			base.Update();

			double intervals = (double)Time.Ticks / (double)Width.Ticks;
			int wholeIntervals = (int)intervals;
			double fractionalIntervals = intervals - wholeIntervals;

			TimeSpan startTime = Time - Width + new TimeSpan((long)(gap * Width.Ticks));
			float startPosition = (float)(fractionalIntervals + gap) % 1;
			TimeSpan endTime = Time;
			float endPosition = startPosition + (float)(1 - gap);

			if (startTime >= new TimeSpan(wholeIntervals * Width.Ticks))
				graphRanges = new Range<TimeSpan>[]
				{
					new Range<TimeSpan>
					(
						new Marker<TimeSpan>(startTime, startPosition),
						new Marker<TimeSpan>(endTime, endPosition)
					)
				};
			else
			{

				graphRanges = new Range<TimeSpan>[]
				{
					new Range<TimeSpan>
					(
						new Marker<TimeSpan>(startTime, startPosition),
						new Marker<TimeSpan>(new TimeSpan(wholeIntervals * Width.Ticks), 1)
					),
					new Range<TimeSpan>
					(
						new Marker<TimeSpan>(new TimeSpan(wholeIntervals * Width.Ticks), 0),
						new Marker<TimeSpan>(endTime, endPosition - 1)
					)
				};
			}

			range = new Range<TimeSpan>
			(
				new Marker<TimeSpan>(new TimeSpan((wholeIntervals + 0) * Width.Ticks), 0),
				new Marker<TimeSpan>(new TimeSpan((wholeIntervals + 1) * Width.Ticks), 1)
			);
		}
	}
}
