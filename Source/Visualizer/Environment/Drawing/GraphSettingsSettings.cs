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
using Utility;
using Visualizer.Drawing;

namespace Visualizer.Environment.Drawing
{
	[TypeConverter(typeof(ExpansionConverter))]
	class GraphSettingsSettings : XSerializable
	{
		readonly Diagram diagram;

		public override XElement XElement
		{
			get
			{
				return new XElement
				(
					XElementName,
					new XElement("ExtendGraphs", ExtendGraphs.ToString().ToLowerInvariant()),
					new XElement("LineWidth", LineWidth)
				);
			}
			set
			{
				if (value.Name != XElementName) throw new ArgumentException("value");

				ExtendGraphs = (bool)value.Element("ExtendGraphs");
				LineWidth = (double)value.Element("LineWidth");
			}
		}

		[DisplayName("Extend Graphs")]
		public bool ExtendGraphs
		{
			get { return diagram.GraphSettings.ExtendGraphs; }
			set { diagram.GraphSettings.ExtendGraphs = value; }
		}
		[DisplayName("Line Width")]
		public double LineWidth
		{
			get { return diagram.GraphSettings.LineWidth; }
			set { diagram.GraphSettings.LineWidth = value; }
		}

		public GraphSettingsSettings(string xElementName, Diagram diagram)
			: base(xElementName)
		{
			this.diagram = diagram;
		}
	}
}
