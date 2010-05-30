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

using System.Xml.Linq;
using Data;

namespace Visualizer.Data
{
	public class Stream
	{
		readonly Path path;
		readonly EntryData entryData;

		public static string XElementName { get { return "Stream"; } }

		public Path Path { get { return path; } }
		public string Name { get; set; }
		public EntryData EntryData { get { return entryData; } }
		public XElement XElement
		{
			get
			{
				return new XElement
				(
					XElementName,
					path.XElement,
					new XElement("Name", Name),
					entryData.XElement
				);
			}
		}

		public Stream(Path path, string name)
		{
			this.path = path;
			this.Name = name;
			this.entryData = new EntryData();
		}
		public Stream(Path path) : this(path, "Stream " + path) { }
		public Stream(XElement stream)
		{
			this.path = new Path(stream.Element(Path.XElementName));
			this.Name = (string)stream.Element("Name");
			this.entryData = new EntryData(stream.Element(EntryData.XElementName));
		}
	}
}
