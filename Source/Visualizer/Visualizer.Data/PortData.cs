// Copyright © Julian Brunner 2009 - 2011

// This file is part of Stream Visualizer (streamvis).
// 
// Stream Visualizer is free software: you can redistribute it and/or modify it
// under the terms of the GNU General Public License as published by the Free
// Software Foundation, either version 3 of the License, or (at your option) any
// later version.
// 
// Stream Visualizer is distributed in the hope that it will be useful, but
// WITHOUT ANY WARRANTY; without even the implied warranty of MERCHANTABILITY or
// FITNESS FOR A PARTICULAR PURPOSE. See the GNU General Public License for more
// details.
// 
// You should have received a copy of the GNU General Public License along with
// Stream Visualizer. If not, see <http://www.gnu.org/licenses/>.

using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace Visualizer.Data
{
	public class PortData
	{
		readonly string name;
		readonly IEnumerable<Stream> streams;

		public static string XElementName { get { return "Port"; } }

		public string Name { get { return name; } }
		public IEnumerable<Stream> Streams { get { return streams; } }
		public XElement XElement
		{
			get
			{
				return new XElement
				(
					XElementName,
					new XElement("Name", name),
					from stream in streams select stream.XElement
				);
			}
		}

		public PortData(string name, IEnumerable<Stream> streams)
		{
			this.name = name;
			this.streams = streams;
		}
		public PortData(XElement port)
		{
			this.name = (string)port.Element("Name");
			this.streams = (from stream in port.Elements(Stream.XElementName) select new Stream(stream)).ToArray();
		}

		public void Export(string path)
		{
			if (streams.Any())
				using (StreamWriter streamWriter = new StreamWriter(System.IO.Path.ChangeExtension(path, EscapeFilename(Name) + ".stream")))
				{
					// Add comment line describing the file layout
					streamWriter.Write("# Time");
					foreach (Stream stream in streams) streamWriter.Write("    " + stream.Name);
					streamWriter.WriteLine();
					streamWriter.WriteLine();

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

							stringBuilder.Append(leadEntry.Time);
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
		public void ClearData()
		{
			foreach (Stream stream in streams) stream.EntryData.Clear();
		}

		static string EscapeFilename(string filename)
		{
			foreach (char invalidCharacter in System.IO.Path.GetInvalidFileNameChars())
				filename = filename.Replace(invalidCharacter.ToString(), string.Empty);

			return filename;
		}
	}
}