using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;

namespace Visualizer.Data
{
	public class Entry
	{
		readonly long time;
		readonly double value;

		public static string XElementName { get { return "entry"; } }

		public long Time { get { return time; } }
		public double Value { get { return value; } }
		public XElement XElement
		{
			get
			{
				return new XElement
				(
					XElementName,
					new XElement("time", time),
					new XElement("value", value)
				);
			}
		}

		public Entry(XElement entry)
		{
			this.time = (long)entry.Element("time");
			this.value = (double)entry.Element("value");
		}
		public Entry(long time, double value)
		{
			this.time = time;
			this.value = value;
		}
	}
}

//namespace Visualizer.Data
//{
//    public class Entry
//    {
//        readonly long time;
//        readonly IEnumerable<double> values;

//        public static string XElementName { get { return "entry"; } }

//        public long Time { get { return time; } }
//        public IEnumerable<double> Values { get { return values; } }
//        public XElement XElement
//        {
//            get
//            {
//                return new XElement
//                (
//                    XElementName,
//                    new XElement("time", time),
//                    from value in values select new XElement("value", value)
//                );
//            }
//        }

//        public Entry(XElement entry)
//        {
//            this.time = (long)entry.Element("time");
//            this.values = (from value in entry.Elements("value") select (double)value).ToArray();
//        }
//        public Entry(long time, IEnumerable<double> values)
//        {
//            this.time = time;
//            this.values = values;
//        }
//    }
//}

