using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;

namespace Visualizer.Data
{
	public class Source
	{
		readonly IEnumerable<Port> ports;

		public static string XElementName { get { return "source"; } }

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
	}
}
