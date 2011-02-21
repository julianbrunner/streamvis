// Copyright © Julian Brunner 2009 - 2011

// This file is part of Stream Visualizer (streamvis).
// 
// Stream Visualizer is free software: you can redistribute it and/or modify it
// under the terms of the GNU General Public License as published by the Free
// Software Foundation, either version 3 of the License, or (at your option) any
// later version.
// 
// Stream Visualizer is distributed in the hope that it will be useful, but
// WITHOUT ANY WARRANTY; without even the implied warranty of MERCHANTABILITY or
// FITNESS FOR A PARTICULAR PURPOSE. See the GNU General Public License for more
// details.
// 
// You should have received a copy of the GNU General Public License along with
// Stream Visualizer. If not, see <http://www.gnu.org/licenses/>.

using System;
using System.ComponentModel;
using System.Drawing;
using System.Xml.Linq;
using Graphics;

namespace Visualizer.Environment
{
	[TypeConverter(typeof(ExpansionConverter))]
	class ViewportSettings : XSerializable
	{
		readonly Viewport viewport;

		public override XElement XElement
		{
			get
			{
				return new XElement
				(
					XElementName,
					new XElement("VerticalSynchronization", VerticalSynchronization),
					new XElement("BackgroundColor", ColorUtility.ToHtmlString(BackgroundColor))
				);
			}
			set
			{
				if (value.Name != XElementName) throw new ArgumentException("value");

				VerticalSynchronization = (bool)value.Element("VerticalSynchronization");
				BackgroundColor = ColorUtility.FromHtmlString((string)value.Element("BackgroundColor"));
			}
		}

		[DisplayName("Vertical Synchronization")]
		public bool VerticalSynchronization
		{
			get { return viewport.VSync; }
			set { viewport.VSync = value; }
		}
		[DisplayName("Background Color")]
		public Color BackgroundColor
		{
			get { return viewport.BackColor; }
			set { viewport.BackColor = value; }
		}

		public ViewportSettings(string xElementName, Viewport viewport)
			: base(xElementName)
		{
			this.viewport = viewport;
		}
	}
}
