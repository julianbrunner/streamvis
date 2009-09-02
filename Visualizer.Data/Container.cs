using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using Extensions;
using Visualizer.Data.Transformations;

namespace Visualizer.Data
{
	public class Container
	{
		readonly IndexedList<Entry> entries;
		readonly EntryResampler resampler;
		readonly EntryCache cache;

		public static string XElementName { get { return "container"; } }

		public IEnumerable<Entry> this[Time startTime, Time endTime] { get { lock (entries) return cache[startTime, endTime].ToArray(); } }

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

		public Container(XElement container)
		{
			entries = new IndexedList<Entry>(from entry in container.Elements(Entry.XElementName) select new Entry(entry));
			resampler = new EntryResampler(entries, new Time(1.0));
			cache = new EntryCache(resampler);
		}
		public Container()
		{
			entries = new IndexedList<Entry>();
			resampler = new EntryResampler(entries, new Time(1.0));
			cache = new EntryCache(resampler);
		}

		public void Clear()
		{
			lock (entries) entries.Clear();
		}
		public void Add(Entry entry)
		{
			// TODO: Check for ordering violation?
			lock (entries)
				if (entry.Value != double.NaN)
					entries.Add(entry);
		}
	}
}
