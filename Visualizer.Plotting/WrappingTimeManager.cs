using System.Collections.Generic;
using Visualizer.Data;

namespace Visualizer.Plotting
{
	public class WrappingTimeManager : TimeManager
	{
		readonly double gap;

		Range<long> range;
		IEnumerable<Range<long>> graphRanges;

		public override Range<long> Range { get { return range; } }
		public override IEnumerable<Range<long>> GraphRanges { get { return graphRanges; } }

		public WrappingTimeManager(Timer timer, long width, double gap)
			: base(timer, width)
		{
			this.gap = gap;
		}

		public override void Update()
		{
			base.Update();

			double intervals = (double)Time / (double)Width;
			int wholeIntervals = (int)intervals;
			double fractionalIntervals = intervals - wholeIntervals;

			long startTime = Time - Width + (long)(gap * Width);
			float startPosition = (float)(fractionalIntervals + gap) % 1;
			long endTime = Time;
			float endPosition = startPosition + (float)(1 - gap);

			if (startTime >= wholeIntervals * Width)
				graphRanges = new Range<long>[]
				{
					new Range<long>
					(
						new Marker<long>(startTime, startPosition),
						new Marker<long>(endTime, endPosition)
					)
				};
			else
			{

				graphRanges = new Range<long>[]
				{
					new Range<long>
					(
						new Marker<long>(startTime, startPosition),
						new Marker<long>(wholeIntervals * Width, 1)
					),
					new Range<long>
					(
						new Marker<long>(wholeIntervals * Width, 0),
						new Marker<long>(endTime, endPosition - 1)
					)
				};
			}

			range = new Range<long>
			(
				new Marker<long>((wholeIntervals + 0) * Width, 0),
				new Marker<long>((wholeIntervals + 1) * Width, 1)
			);
		}
	}
}
