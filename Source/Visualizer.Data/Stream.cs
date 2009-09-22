using System.Xml.Linq;

namespace Visualizer.Data
{
	public class Stream
	{
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
					path.XElement,
					entryData.XElement
				);
			}
		}
		public Path Path { get { return path; } }
		public EntryData EntryData { get { return entryData; } }

		public Stream(XElement stream)
		{
			this.path = new Path(stream.Element(Path.XElementName));
			this.entryData = new EntryData(stream.Element(EntryData.XElementName));
		}
		public Stream(Path path)
		{
			this.path = path;
			this.entryData = new EntryData();
		}
	}
}
