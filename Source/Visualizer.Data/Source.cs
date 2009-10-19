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
using System.Xml.Linq;

namespace Visualizer.Data
{
	public class Source
	{
		readonly IEnumerable<Port> ports;

		public static string XElementName { get { return "Source"; } }

		public XElement XElement { get { return new XElement(XElementName, from port in ports select port.XElement); } }
		public IEnumerable<Port> Ports { get { return ports; } }

		public Source() : this(Enumerable.Empty<Port>()) { }
		public Source(XElement source)
		{
			this.ports = (from port in source.Elements(Port.XElementName) select new Port(port)).ToArray();
		}
		protected Source(IEnumerable<Port> ports)
		{
			this.ports = ports.ToArray();
		}

		public void Export(string path)
		{
			foreach (Port port in ports)
				using (StreamWriter streamWriter = new StreamWriter(System.IO.Path.ChangeExtension(path, EscapeFilename(port.Name) + ".stream")))
					port.Export(streamWriter);
		}
		public void ClearData()
		{
			foreach (Port port in ports) port.ClearData();
		}

		static string EscapeFilename(string filename)
		{
			foreach (char invalidCharacter in System.IO.Path.GetInvalidFileNameChars())
				filename = filename.Replace(invalidCharacter.ToString(), string.Empty);

			return filename;
		}
	}
}
