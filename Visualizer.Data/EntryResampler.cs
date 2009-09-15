using System;
using System.Collections.Generic;
using System.Linq;
using Utility;

namespace Visualizer.Data
{
	public class EntryResampler
	{
		readonly SearchList<Entry, Time> entries;
		readonly Time sampleDistance;

		public Fragment this[Range<Time> range]
		{
			get
			{
				if (entries.Count == 0) return Fragment.Empty;

				Time startTime = range.Start;
				Time endTime = range.End;

				startTime = Time.Max(startTime, entries[0].Time);
				endTime = Time.Min(endTime, entries[entries.Count - 1].Time);

				startTime = startTime.Ceiling(sampleDistance, Time.Zero);
				endTime = endTime.Floor(sampleDistance, Time.Zero);

				if (startTime >= endTime) return Fragment.Empty;

				List<Entry> samples = new List<Entry>();

				for (Time time = startTime; time < endTime; time += sampleDistance)
					samples.Add(Aggregate(entries, time, time + sampleDistance));

				return new Fragment(new Range<Time>(startTime, endTime), samples);
			}
		}

		public EntryResampler(SearchList<Entry, Time> source, Time sampleDistance)
		{
			this.entries = source;
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

			IEnumerable<Entry> entries = EnumerableUtility.Construct(start.Single(), source[startIndex, endIndex], end.Single());
			double area = entries.GetRanges().Sum(range => (range.End.Time - range.Start.Time).Seconds * 0.5 * (range.Start.Value + range.End.Value));
			return new Entry(0.5 * (start.Time + end.Time), area / (end.Time - start.Time).Seconds);
		}
		static double Interpolate(double a, double b, double f)
		{
			return (1 - f) * a + f * b;
		}
	}
}
