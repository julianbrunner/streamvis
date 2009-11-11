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

using System.Xml.Linq;

namespace Visualizer.Data
{
	public struct Entry
	{
		readonly double time;
		readonly double value;

		public static string XElementName { get { return "Entry"; } }

		public double Time { get { return time; } }
		public double Value { get { return value; } }
		public XElement XElement
		{
			get
			{
				return new XElement
				(
					XElementName,
					new XElement("Time", Time),
					new XElement("Value", Value)
				);
			}
		}

		public Entry(double time, double value)
		{
			this.time = time;
			this.value = value;
		}
		public Entry(XElement entry)
		{
			this.time = (double)entry.Element("Time");
			this.value = (double)entry.Element("Value");
		}
	}
}