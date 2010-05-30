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
using System.Xml.Linq;
using Graphics;
using Utility;

namespace Visualizer.Environment
{
	[TypeConverter(typeof(ExpansionConverter))]
	class DrawerSettings : XSerializable
	{
		readonly Drawer drawer;

		public override XElement XElement
		{
			get
			{
				return new XElement
				(
					XElementName,
					new XElement("LineSmoothing", LineSmoothing.ToString().ToLowerInvariant()),
					new XElement("AlphaBlending", AlphaBlending.ToString().ToLowerInvariant())
				);
			}
			set
			{
				if (value.Name != XElementName) throw new ArgumentException("value");

				LineSmoothing = (bool)value.Element("LineSmoothing");
				AlphaBlending = (bool)value.Element("AlphaBlending");
			}
		}

		[DisplayName("Line Smoothing")]
		public bool LineSmoothing
		{
			get { return drawer.LineSmoothing; }
			set { drawer.LineSmoothing = value; }
		}
		[DisplayName("Alpha Blending")]
		public bool AlphaBlending
		{
			get { return drawer.AlphaBlending; }
			set { drawer.AlphaBlending = value; }
		}

		public DrawerSettings(string xElementName, Drawer drawer)
			: base(xElementName)
		{
			this.drawer = drawer;
		}
	}
}
