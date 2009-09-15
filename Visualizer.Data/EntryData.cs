using System.Linq;
using System.Xml.Linq;
using Utility;

namespace Visualizer.Data
{
	public class EntryData
	{
		readonly SearchList<Entry, Time> entries;
		readonly EntryCache cache;

		public static string XElementName { get { return "EntryData"; } }

		public Entry[] this[Range<Time> range] { get { lock (entries) return cache[range]; } }

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

		public EntryData()
		{
			entries = new SearchList<Entry, Time>(entry => entry.Time);
			cache = new EntryCache(new EntryResampler(entries, new Time(0.02)));
		}
		public EntryData(XElement entryData)
			: this()
		{
			entries.Append(from entry in entryData.Elements(Entry.XElementName) select new Entry(entry));
		}

		public void Clear()
		{
			lock (entries) entries.Clear();
		}
		public void Add(Entry entry)
		{
			if (entry.Value != double.NaN)
				lock (entries)
					entries.Append(entry);
		}
	}
}
