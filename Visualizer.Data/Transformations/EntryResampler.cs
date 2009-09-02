using System;
using System.Collections.Generic;
using System.Linq;
using Extensions;
using Extensions.Searching;

namespace Visualizer.Data.Transformations
{
	public class EntryResampler
	{
		readonly EntryBuffer source;
		readonly Time sampleDistance;

		public IEnumerable<Entry> this[Time startTime, Time endTime]
		{
			get
			{
				if (source.Count == 0) yield break;

				Time firstBorder = startTime.Ceiling(sampleDistance);
				Time lastBorder = endTime.Floor(sampleDistance);

				for (Time time = firstBorder; time < lastBorder; time += sampleDistance)
				{
					Time intervalStartTime = time;
					Time intervalEndTime = time + sampleDistance;

					if (source[0].Time <= intervalStartTime && source[source.Count - 1].Time >= intervalEndTime)
						yield return Aggregate(source, intervalStartTime, intervalEndTime);
				}
			}
		}

		public EntryResampler(EntryBuffer source, Time sampleDistance)
		{
			this.source = source;
			this.sampleDistance = sampleDistance;
		}

		static Entry Aggregate(IIndexed<Entry, int> source, Time startTime, Time endTime)
		{
			if (source.Count == 0)
				throw new ArgumentException("The source stream is empty.");
			if (source[0].Time > startTime || source[source.Count - 1].Time < endTime)
				throw new ArgumentException("The specified range isn't fully covered with data.");

			int startIndex = source.FindIndex(startTime);
			int endIndex = source.FindIndex(endTime);

			Entry beforeStart = source[startIndex].Time > startTime ? source[startIndex - 1] : source[startIndex];
			Entry afterStart = source[startIndex];
			Entry beforeEnd = source[endIndex].Time > endTime ? source[endIndex - 1] : source[endIndex];
			Entry afterEnd = source[endIndex];

			double startFraction = (startTime - beforeStart.Time) / (afterStart.Time - beforeStart.Time);
			double startValue = Interpolate(beforeStart.Value, afterStart.Value, startFraction);
			Entry start = new Entry(startTime, startValue);
			double endFraction = (endTime - beforeEnd.Time) / (afterEnd.Time - beforeEnd.Time);
			double endValue = Interpolate(beforeEnd.Value, afterEnd.Value, endFraction);
			Entry end = new Entry(endTime, endValue);

			IEnumerable<Entry> entries = EnumerablePlus.Construct(start.Single(), source.GetRange(startIndex, endIndex), end.Single());
			double area = entries.Pairs().Sum(range => (range.B.Time - range.A.Time).Seconds * 0.5 * (range.A.Value + range.B.Value));
			return new Entry(0.5 * (start.Time + end.Time), area / (end.Time - start.Time).Seconds);
		}
		static double Interpolate(double a, double b, double f)
		{
			return (1 - f) * a + f * b;
		}
	}
}
