using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using Extensions;
using Visualizer.Data.Transformations;

namespace Visualizer.Data
{
	public class Container
	{
		readonly SearchList<Entry, Time> entries;
		readonly EntryResampler resampler;
		readonly EntryCache cache;

		public static string XElementName { get { return "container"; } }

		public IEnumerable<Entry> this[Time startTime, Time endTime]
		{
			get
			{
				lock (entries)
					return cache[startTime, endTime];
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

		public Container()
		{
			entries = new SearchList<Entry, Time>(entry => entry.Time);
			resampler = new EntryResampler(entries, new Time(0.1));
			cache = new EntryCache(resampler);
		}
		public Container(XElement container)
			: this()
		{
			entries.Append(from entry in container.Elements(Entry.XElementName) select new Entry(entry));
		}

		public void Clear()
		{
			lock (entries)
				entries.Clear();
		}
		public void Add(Entry entry)
		{
			if (entry.Value != double.NaN)
				lock (entries)
					entries.Append(entry);
		}
	}
}
