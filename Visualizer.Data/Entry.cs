using System;
using System.Linq;
using System.Collections.Generic;
using System.Xml.Linq;

namespace Visualizer.Data
{
	public class Entry
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
					new XElement("time", time.Ticks),
					new XElement("value", value)
				);
			}
		}

		public Entry(XElement entry)
		{
			this.time = new Time((long)entry.Element("time"));
			this.value = (double)entry.Element("value");
		}
		public Entry(Time time, double value)
		{
			this.time = time;
			this.value = value;
		}
	}
}