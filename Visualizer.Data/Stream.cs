using System.Xml.Linq;

namespace Visualizer.Data
{
	public class Stream
	{
		readonly Path path;
		readonly Container container;

		public static string XElementName { get { return "stream"; } }

		public XElement XElement
		{
			get
			{
				return new XElement
				(
					XElementName,
					path.XElement,
					container.XElement
				);
			}
		}
		public Path Path { get { return path; } }
		public Container Container { get { return container; } }

		public Stream(XElement stream)
		{
			this.path = new Path(stream.Element(Path.XElementName));
			this.container = new Container(stream.Element(Container.XElementName));
		}
		public Stream(Path path)
		{
			this.path = path;
			this.container = new Container();
		}
	}
}
