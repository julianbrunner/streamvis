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
using System.Linq;
using System.Xml.Linq;
using System.IO;
using System.Text;

namespace Visualizer.Data
{
	public class Source
	{
		readonly IEnumerable<Port> ports;

		public static string XElementName { get { return "Source"; } }

		public XElement XElement { get { return new XElement(XElementName, from port in ports select port.XElement); } }
		public IEnumerable<Port> Ports { get { return ports; } }

		public Source(XElement source)
		{
			this.ports = (from port in source.Elements(Port.XElementName) select new Port(port)).ToArray();
		}
		public Source() : this(Enumerable.Empty<Port>()) { }
		protected Source(IEnumerable<Port> ports)
		{
			this.ports = ports.ToArray();
		}

		public void ClearData()
		{
			foreach (Port port in ports) port.ClearData();
		}
		public void ExportGNUPlot(string path)
		{
			foreach (Port port in ports)
				using (StreamWriter streamWriter = new StreamWriter(System.IO.Path.ChangeExtension(path, EscapeFilename(port.Name) + ".stream")))
					if (port.Streams.Any())
					{
						IEnumerable<IEnumerable<Entry>> entries =
						(
							from stream in port.Streams
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
								streamWriter.WriteLine(stringBuilder.ToString());
							}
					}
		}

		static string EscapeFilename(string filename)
		{
			foreach (char invalidCharacter in System.IO.Path.GetInvalidFileNameChars())
				filename = filename.Replace(invalidCharacter.ToString(), string.Empty);

			return filename;
		}
	}
}
