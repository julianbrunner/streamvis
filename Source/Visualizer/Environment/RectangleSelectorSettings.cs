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
using System.Windows.Forms;
using System.Xml.Linq;
using Graphics;
using Utility;
using Utility.Extensions;
using Utility.Utilities;

namespace Visualizer.Environment
{
	[TypeConverter(typeof(ExpansionConverter))]
	class RectangleSelectorSettings : XSerializable
	{
		readonly RectangleSelector rectangleSelector;

		public XElement XElement
		{
			get
			{
				return new XElement
				(
					XElementName,
					new XElement("IsDrawn", IsDrawn),
					new XElement("Button", Button),
					new XElement("Color", Color.ToHtmlString()),
					new XElement("Width", Width)
				);
			}
			set
			{
				if (value.Name != XElementName) throw new ArgumentException("value");

				IsDrawn = (bool)value.Element("IsDrawn");
				Button = (MouseButtons)Enum.Parse(typeof(MouseButtons), (string)value.Element("Button"));
				Color = ColorUtility.FromHtmlString((string)value.Element("Color"));
				Width = (float)value.Element("Width");
			}
		}

		[DisplayName("Draw")]
		public bool IsDrawn
		{
			get { return rectangleSelector.IsDrawn; }
			set { rectangleSelector.IsDrawn = value; }
		}
		[DisplayName("Button")]
		public MouseButtons Button
		{
			get { return rectangleSelector.Button; }
			set { rectangleSelector.Button = value; }
		}
		[DisplayName("Color")]
		public Color Color
		{
			get { return rectangleSelector.Color; }
			set { rectangleSelector.Color = value; }
		}
		[DisplayName("Width")]
		public float Width
		{
			get { return rectangleSelector.Width; }
			set { rectangleSelector.Width = value; }
		}

		public RectangleSelectorSettings(string xElementName, RectangleSelector rectangleSelector)
			: base(xElementName)
		{
			this.rectangleSelector = rectangleSelector;
		}
	}
}
