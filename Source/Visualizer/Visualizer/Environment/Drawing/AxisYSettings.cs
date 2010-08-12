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

using System;
using System.ComponentModel;
using System.Drawing;
using System.Xml.Linq;
using Visualizer.Drawing;
using Krach.Graphics;

namespace Visualizer.Environment.Drawing
{
	[TypeConverter(typeof(ExpansionConverter))]
	class AxisYSettings : XSerializable
	{
		readonly Diagram diagram;

		public override XElement XElement
		{
			get
			{
				return new XElement
				(
					XElementName,
					new XElement("MarkerCount", MarkerCount),
					new XElement("Color", ColorUtility.ToHtmlString(Color))
				);
			}
			set
			{
				if (value.Name != XElementName) throw new ArgumentException("value");

				MarkerCount = (int)value.Element("MarkerCount");
				Color = ColorUtility.FromHtmlString((string)value.Element("Color"));
			}
		}

		[DisplayName("Marker Count")]
		public int MarkerCount
		{
			get { return diagram.AxisY.MarkerCount; }
			set { diagram.AxisY.MarkerCount = value; }
		}
		[DisplayName("Color")]
		public System.Drawing.Color Color
		{
			get { return diagram.AxisY.Color; }
			set { diagram.AxisY.Color = value; }
		}

		public AxisYSettings(string xElementName, Diagram diagram)
			: base(xElementName)
		{
			this.diagram = diagram;
		}
	}
}
