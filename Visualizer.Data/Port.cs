using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;

namespace Visualizer.Data
{
	public class Port
	{
		readonly string name;
		readonly IEnumerable<Stream> streams;

		public static string XElementName { get { return "port"; } }

		public XElement XElement { get { return new XElement(XElementName, new XElement("name", name), from stream in streams select stream.XElement); } }
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

		public void ClearData()
		{
			foreach (Stream stream in streams) stream.Container.Clear();
		}
	}
}