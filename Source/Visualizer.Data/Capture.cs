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

namespace Visualizer.Data
{
	public class Capture
	{
		readonly IEnumerable<PortData> portsData;

		public static string XElementName { get { return "Capture"; } }

		public IEnumerable<PortData> PortsData { get { return portsData; } }
		public XElement XElement { get { return new XElement(XElementName, from port in portsData select port.XElement); } }

		public Capture() : this(Enumerable.Empty<PortData>()) { }
		public Capture(IEnumerable<PortData> portsData)
		{
			this.portsData = portsData.ToArray();
		}
		public Capture(XElement source)
		{
			this.portsData =
			(
				from portData in source.Elements(PortData.XElementName)
				select new PortData(portData)
			)
			.ToArray();
		}

		public void Export(string path)
		{
			foreach (PortData portData in portsData) portData.Export(path);
		}
		public void ClearData()
		{
			foreach (PortData portData in portsData) portData.ClearData();
		}
	}
}
