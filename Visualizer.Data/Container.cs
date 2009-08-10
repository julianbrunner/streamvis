using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using System;

namespace Visualizer.Data
{
	public class Container
	{
		const long sampleTicks = 100 * 10000L;

		readonly List<Entry> sampleBuffer = new List<Entry>();
		readonly List<Entry> entries;

		public static string XElementName { get { return "container"; } }

		public Entry this[int index] { get { lock (entries) return entries[index]; } }
		public Entry[] this[int startIndex, int endIndex]
		{
			get
			{
				Entry[] buffer = new Entry[endIndex - startIndex];
				lock (entries) entries.CopyTo(startIndex, buffer, 0, endIndex - startIndex);
				return buffer;
			}
		}

		public XElement XElement
		{
			get
			{
				lock (entries)
					return new XElement
					(
						XElementName,
						(
							from entry in entries
							select entry.XElement
						)
						.ToArray()
					);
			}
		}
		public Entry[] Data { get { lock (entries) return entries.ToArray(); } }
		public bool IsEmpty { get { lock (entries) return entries.Count == 0; } }
		public int Count { get { lock (entries) return entries.Count; } }

		public Container(XElement container)
		{
			this.entries =
			(
				from entry in container.Elements(Entry.XElementName)
				select new Entry(entry)
			)
			.ToList();
		}
		public Container()
		{
			this.entries = new List<Entry>();
		}

		public void Clear()
		{
			lock (entries) entries.Clear();
		}
		// TOOD: Resampling should be done in Visualizer.Capturing instead of Visualizer.Data
		public void Add(Entry entry)
		{
			if (entry.Value != double.NaN && (sampleBuffer.Count == 0 || entry.Time > sampleBuffer[sampleBuffer.Count - 1].Time))
				sampleBuffer.Add(entry);

			while (sampleBuffer.Count > 1 && sampleBuffer[sampleBuffer.Count - 1].Time - sampleBuffer[0].Time >= sampleTicks)
			{
				Entry newEntry = AggregateSamples();
				// TODO: Do we have to check if the timestamp is correct?
				lock (entries) entries.Add(newEntry);
			}
		}
		public int GetIndex(long time)
		{
			lock (entries) return GetIndex(0, entries.Count, time);
		}

		/// <summary>
		/// Returns the index of the first item which has a timestamp that is greater than or equal to <paramref name="time"/>.
		/// If no such item is found, <paramref name="end"/> is returned.
		/// </summary>
		/// <param name="start">The start index of the range to search.</param>
		/// <param name="end">The end index of the range to search.</param>
		/// <param name="time">The time to search for.</param>
		/// <returns>The index of the first item which has a timestamp that is greater than or equal to <paramref name="time"/>.</returns>
		int GetIndex(int start, int end, long time)
		{
			if (start == end) return start;

			int index = (start + end) / 2;

			if (entries[index].Time > time) return GetIndex(start, index, time);
			if (entries[index].Time < time) return GetIndex(index + 1, end, time);

			return index;
		}
		Entry AggregateSamples()
		{
			Entry start = sampleBuffer[0];

			Entry lastInside = sampleBuffer[sampleBuffer.Count - 2];
			Entry last = sampleBuffer[sampleBuffer.Count - 1];
			long endTime = start.Time + sampleTicks;
			double fraction = (double)(endTime - lastInside.Time) / (double)(last.Time - lastInside.Time);

			Entry end = new Entry(endTime, Interpolate(lastInside.Value, last.Value, fraction));

			sampleBuffer.Insert(sampleBuffer.Count - 1, end);

			double value = 0;

			for (int i = 0; i < sampleBuffer.Count - 2; i++)
			{
				Entry a = sampleBuffer[i + 0];
				Entry b = sampleBuffer[i + 1];

				value += (b.Time - a.Time) * ((a.Value + b.Value) / 2);
			}

			sampleBuffer.RemoveRange(0, sampleBuffer.Count - 2);

			return new Entry((start.Time + end.Time) / 2, value / sampleTicks);
		}

		static double Interpolate(double a, double b, double f)
		{
			return (1 - f) * a + f * b;
		}
	}
}
