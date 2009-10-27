// Copyright © Julian Brunner 2009

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
using System.IO;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace Visualizer.Data
{
	public class Port
	{
		readonly string name;
		readonly IEnumerable<Stream> streams;

		public static string XElementName { get { return "Port"; } }

		public XElement XElement { get { return new XElement(XElementName, new XElement("Name", name), from stream in streams select stream.XElement); } }
		public string Name { get { return name; } }
		public IEnumerable<Stream> Streams { get { return streams; } }

		public Port(XElement port)
		{
			this.name = (string)port.Element("name");
			this.streams = (from stream in port.Elements(Stream.XElementName) select new Stream(stream)).ToArray();
		}
		protected Port(string name, IEnumerable<Stream> streams)
		{
			this.name = name;
			this.streams = streams.ToArray();
		}

		public void Export(TextWriter writer)
		{
			if (streams.Any())
			{
				IEnumerable<IEnumerable<Entry>> entries =
				(
					from stream in streams
					select stream.EntryData.Entries
				)
				.ToArray();

				IEnumerable<IEnumerator<Entry>> enumerators =
				(
					from stream in entries
					select stream.GetEnumerator()
				)
				.ToArray();

				foreach (Entry leadEntry in entries.First())
					if (enumerators.All(enumerator => enumerator.MoveNext()))
					{
						StringBuilder stringBuilder = new StringBuilder();

						stringBuilder.Append(leadEntry.Time.Seconds);
						stringBuilder.Append(" ");

						foreach (Entry entry in from enumerator in enumerators select enumerator.Current)
						{
							stringBuilder.Append(entry.Value);
							stringBuilder.Append(" ");
						}

						stringBuilder.Remove(stringBuilder.Length - 1, 1);

						writer.WriteLine(stringBuilder.ToString());
					}
			}
		}
		public void ClearData()
		{
			foreach (Stream stream in streams) stream.EntryData.Clear();
		}
	}
}