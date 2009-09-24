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

using System.Xml.Linq;

namespace Visualizer.Data
{
	public class Stream
	{
		readonly string name;
		readonly Path path;
		readonly EntryData entryData;

		public static string XElementName { get { return "Stream"; } }

		public XElement XElement
		{
			get
			{
				return new XElement
				(
					XElementName,
					new XElement("Name", name),
					path.XElement,
					entryData.XElement
				);
			}
		}
		public string Name { get { return name; } }
		public Path Path { get { return path; } }
		public EntryData EntryData { get { return entryData; } }

		public Stream(XElement stream)
		{
			this.name = (string)stream.Element("Name");
			this.path = new Path(stream.Element(Path.XElementName));
			this.entryData = new EntryData(stream.Element(EntryData.XElementName));
		}
		public Stream(string name, Path path)
		{
			this.name = name;
			this.path = path;
			this.entryData = new EntryData();
		}
		public Stream(Path path) : this("Stream " + path, path) { }
	}
}
