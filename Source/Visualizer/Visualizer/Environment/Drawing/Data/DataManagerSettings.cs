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
using Visualizer.Drawing;

namespace Visualizer.Environment.Drawing.Data
{
	[TypeConverter(typeof(ExpansionConverter))]
	abstract class DataManagerSettings : XSerializable
	{
		readonly Diagram diagram;

		protected Diagram Diagram { get { return diagram; } }

		public override XElement XElement
		{
			get
			{
				return new XElement
				(
					XElementName,
					new XElement("ClearData", ClearData)
				);
			}
			set
			{
				if (value.Name != XElementName) throw new ArgumentException("value");

				ClearData = (bool)value.Element("ClearData");
			}
		}

		[DisplayName("Clear Data")]
		[Description("Enables or disables the regular clearing of old received data. Enable if you plan to run the visualizer for a long time and you don't want to save the results.")]
		public bool ClearData
		{
			get { return Diagram.DataManager.ClearData; }
			set { Diagram.DataManager.ClearData = value; }
		}

		protected DataManagerSettings(string xElementName, Diagram diagram)
			: base(xElementName)
		{
			this.diagram = diagram;
		}
	}
}
