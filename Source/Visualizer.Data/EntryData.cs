// Copyright © Julian Brunner 2009 - 2010

// This file is part of Stream Visualizer (streamvis).
// 
// Stream Visualizer is free software: you can redistribute it and/or modify
// it under the terms of the GNU General Public License as published by
// the Free Software Foundation, either version 3 of the License, or
// (at your option) any later version.
// 
// Stream Visualizer is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU General Public License for more details.
// 
// You should have received a copy of the GNU General Public License
// along with Stream Visualizer.  If not, see <http://www.gnu.org/licenses/>.

using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using Utility;

namespace Visualizer.Data
{
	public class EntryData
	{
		readonly SearchList<Entry, double> entries = new SearchList<Entry, double>(entry => entry.Time);
		readonly List<Entry> buffer = new List<Entry>();

		public static string XElementName { get { return "EntryData"; } }

		public SearchList<Entry, double> Entries { get { return entries; } }
		public XElement XElement { get { return new XElement(XElementName, from entry in entries select entry.XElement); } }

		public EntryData() { }
		public EntryData(XElement entryData)
		{
			entries.Append(from entry in entryData.Elements(Entry.XElementName) select new Entry(entry));
		}

		public void Clear()
		{
			entries.Clear();
			lock (buffer) buffer.Clear();
		}
		public void Add(Entry entry)
		{
			if (entry.Value != double.NaN)
				lock (buffer)
					buffer.Add(entry);
		}
		public void UpdateEntries()
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