using System;
using System.Linq;
using System.Collections.Generic;
using Extensions;

namespace Data
{
	public class Cache
	{
		readonly Buffer source;
		readonly Time sampleDistance;
		readonly List<Entry> buffer = new List<Entry>();
		
		public Cache(Buffer source, Time sampleDistance)
		{
			this.source = source;
			this.sampleDistance = sampleDistance;
		}
		
		public void Refresh()
		{
			while (true)
			{
				Time startTime = buffer.Count == 0 ? Time.Zero : buffer[buffer.Count - 1].Time + 0.5 * sampleDistance;
				
				if (startTime + sampleDistance > source[source.Count - 1].Time) break;
				
				buffer.Add(Aggregate(source, startTime, startTime + sampleDistance));
			}
		}
		
		static Entry Aggregate(Buffer source, Time startTime, Time endTime)
		{
			if (source.Count == 0)
				throw new ArgumentException("The source stream was empty.");
			if (source[0].Time > startTime || source[source.Count - 1].Time < endTime)
				throw new ArgumentException("The specified range isn't fully covered with data.");
			
			BinarySearcher searcher = new BinarySearcher(source);
			
			int startIndex = searcher.GetIndex(startTime);
			int endIndex = searcher.GetIndex(endTime);
					
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
			
			return Aggregate(EnumerablePlus.Construct(start.Single(), source.Range(startIndex, endIndex), end.Single()), start, end);
		}
		static Entry Aggregate(IEnumerable<Entry> source, Entry start, Entry end)
		{
			double value = source.Pairs().Sum(range => (range.B.Time - range.A.Time).Seconds * 0.5 * (range.A.Value + range.B.Value));			
			return new Entry(0.5 * (start.Time + end.Time), value / (end.Time - start.Time).Seconds);
		}
		static double Interpolate(double a, double b, double f)
		{
			return (1 - f) * a + f * b;
		}
	}
}
