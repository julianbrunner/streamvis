using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using Visualizer.Data.Transformations;

namespace Visualizer.Data
{
	public class Container
	{
		readonly EntryBuffer entryBuffer;
		readonly EntryResampler resampler;
		readonly EntryCache cache;

		public static string XElementName { get { return "container"; } }

		public IEnumerable<Entry> this[Time startTime, Time endTime] { get { lock (entryBuffer) return cache[startTime, endTime].ToArray(); } }

		public XElement XElement
		{
			get
			{
				lock (entryBuffer)
					return new XElement
					(
						XElementName,
						(
							from entry in entryBuffer
							select entry.XElement
						)
						.ToArray()
					);
			}
		}

		public Container(XElement container)
		{
			entryBuffer = new EntryBuffer(from entry in container.Elements(Entry.XElementName) select new Entry(entry));
			resampler = new EntryResampler(entryBuffer, new Time(1.0));
			cache = new EntryCache(resampler);
		}
		public Container()
		{
			entryBuffer = new EntryBuffer();
			resampler = new EntryResampler(entryBuffer, new Time(1.0));
			cache = new EntryCache(resampler);
		}

		public void Clear()
		{
			lock (entryBuffer) entryBuffer.Clear();
		}
		public void Add(Entry entry)
		{
			lock (entryBuffer)
				if (entry.Value != double.NaN)
					entryBuffer.Add(entry);
		}
	}
}
