using System;
using System.Collections.Generic;
using System.Linq;
using Extensions;

namespace Visualizer.Data.Transformations
{
	public class EntryResampler
	{
		readonly SearchList<Entry, Time> source;
		readonly Time sampleDistance;

		public Fragment this[Time startTime, Time endTime]
		{
			get
			{
				if (source.Count == 0) return Fragment.Empty;

				startTime = Time.Max(startTime, source[0].Time);
				endTime = Time.Min(endTime, source[source.Count - 1].Time);

				startTime = startTime.Ceiling(sampleDistance, Time.Zero);
				endTime = endTime.Floor(sampleDistance, Time.Zero);

				List<Entry> entries = new List<Entry>();

				for (Time time = startTime; time < endTime; time += sampleDistance)
				{
					Time intervalStartTime = time;
					Time intervalEndTime = time + sampleDistance;

					entries.Add(Aggregate(source, intervalStartTime, intervalEndTime));
				}

				if (entries.Count == 0) return Fragment.Empty;

				return new Fragment(new Range<Time>(startTime, endTime), entries);
			}
		}

		public EntryResampler(SearchList<Entry, Time> source, Time sampleDistance)
		{
			this.source = source;
			this.sampleDistance = sampleDistance;
		}

		static Entry Aggregate(SearchList<Entry, Time> source, Time startTime, Time endTime)
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

			IEnumerable<Entry> entries = Enumerables.Construct(start.Single(), source[startIndex, endIndex], end.Single());
			double area = entries.GetRanges().Sum(range => (range.End.Time - range.Start.Time).Seconds * 0.5 * (range.Start.Value + range.End.Value));
			return new Entry(0.5 * (start.Time + end.Time), area / (end.Time - start.Time).Seconds);
		}
		static double Interpolate(double a, double b, double f)
		{
			return (1 - f) * a + f * b;
		}
	}
}
