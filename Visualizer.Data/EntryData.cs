using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using Utility;

namespace Visualizer.Data
{
	public class EntryData
	{
		readonly SearchList<Entry, Time> entries = new SearchList<Entry, Time>(entry => entry.Time, 4000000);
		readonly List<Entry> bufferLow = new List<Entry>();
		readonly List<Entry> bufferHigh = new List<Entry>();

		public static string XElementName { get { return "EntryData"; } }

		public SearchList<Entry, Time> Entries { get { return entries; } }
		public XElement XElement
		{
			get
			{
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
			if (entry.Value != double.NaN) bufferLow.Add(entry);

			if ((entry.Time - bufferLow[0].Time).Seconds > 0.1)
			{
				lock (bufferHigh) bufferHigh.AddRange(bufferLow);
				bufferLow.Clear();
			}
		}
		public void Update()
		{
			IEnumerable<Entry> bufferedEntries;

			lock (bufferHigh)
			{
				bufferedEntries = bufferHigh.ToArray();
				bufferHigh.Clear();
			}

			entries.Append(bufferedEntries);
		}
	}
}
