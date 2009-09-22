using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using Utility;

namespace Visualizer.Data
{
	public class EntryData
	{
		readonly SearchList<Entry, Time> entries = new SearchList<Entry, Time>(entry => entry.Time);
		readonly List<Entry> buffer = new List<Entry>();

		public static string XElementName { get { return "EntryData"; } }

		public SearchList<Entry, Time> Entries { get { return entries; } }
		public XElement XElement { get { return new XElement(XElementName, from entry in entries select entry.XElement); } }

		public EntryData() { }
		public EntryData(XElement entryData)
		{
			entries.Append(from entry in entryData.Elements(Entry.XElementName) select new Entry(entry));
		}

		public void Clear()
		{
			entries.Clear();
		}
		public void Add(Entry entry)
		{
			if (entry.Value != double.NaN)
				lock (buffer)
					buffer.Add(entry);
		}
		public void Update()
		{
			IEnumerable<Entry> bufferedEntries;

			lock (buffer)
			{
				bufferedEntries = buffer.ToArray();
				buffer.Clear();
			}

			entries.Append(bufferedEntries);
		}
	}
}
