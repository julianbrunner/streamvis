using System.Xml.Linq;
using Extensions.Searching;

namespace Visualizer.Data
{
	public class Entry : IKeyed<Time>
	{
		readonly Time time;
		readonly double value;

		public static string XElementName { get { return "entry"; } }

		public Time Time { get { return time; } }
		public double Value { get { return value; } }
		public XElement XElement
		{
			get
			{
				return new XElement
				(
					XElementName,
					new XElement("time", Time.Ticks),
					new XElement("value", Value)
				);
			}
		}

		public Entry(Time time, double value)
		{
			this.time = time;
			this.value = value;
		}
		public Entry(XElement entry)
		{
			this.time = new Time((long)entry.Element("time"));
			this.value = (double)entry.Element("value");
		}

		Time IKeyed<Time>.Key { get { return Time; } }
	}
}