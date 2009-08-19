using System.Drawing;
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
					new XElement("color", new ColorConverter().ConvertToInvariantString(Color)),
					container.XElement
				);
			}
		}
		public Path Path { get { return path; } }
		public Color Color { get; set; }
		public Container Container { get { return container; } }

		public Stream(XElement stream)
		{
			this.path = new Path(stream.Element(Path.XElementName));
			this.Color = (Color)new ColorConverter().ConvertFromInvariantString(stream.Element("color").Value);
			this.container = new Container(stream.Element(Container.XElementName));
		}
		public Stream(Path path, Color color)
		{
			this.path = path;
			this.Color = color;
			this.container = new Container();
		}
	}
}
