using System;
using System.Collections.Generic;
using Visualizer.Data;

namespace Visualizer.Plotting
{
	public class WrappingTimeManager : TimeManager
	{
		readonly double gap;

		Range<Time> range;
		IEnumerable<Range<Time>> graphRanges;

		public override Range<Time> Range { get { return range; } }
		public override IEnumerable<Range<Time>> GraphRanges { get { return graphRanges; } }

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
			float startPosition = (float)(fractionalIntervals + gap) % 1;
			Time endTime = Time;
			float endPosition = startPosition + (float)(1 - gap);

			if (startTime >= wholeIntervals * Width)
				graphRanges = new Range<Time>[]
				{
					new Range<Time>
					(
						new Marker<Time>(startTime, startPosition),
						new Marker<Time>(endTime, endPosition)
					)
				};
			else
			{

				graphRanges = new Range<Time>[]
				{
					new Range<Time>
					(
						new Marker<Time>(startTime, startPosition),
						new Marker<Time>(wholeIntervals * Width, 1)
					),
					new Range<Time>
					(
						new Marker<Time>(wholeIntervals * Width, 0),
						new Marker<Time>(endTime, endPosition - 1)
					)
				};
			}

			range = new Range<Time>
			(
				new Marker<Time>((wholeIntervals + 0) * Width, 0),
				new Marker<Time>((wholeIntervals + 1) * Width, 1)
			);
		}
	}
}
