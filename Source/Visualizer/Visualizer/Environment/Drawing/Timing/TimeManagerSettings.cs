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
using System.Xml.Linq;
using Visualizer.Drawing;

namespace Visualizer.Environment.Drawing.Timing
{
	[TypeConverter(typeof(ExpansionConverter))]
	abstract class TimeManagerSettings : XSerializable
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
					new XElement("IsUpdated", IsUpdated),
					new XElement("Time", Time),
					new XElement("Width", Width)
				);
			}
			set
			{
				if (value.Name != XElementName) throw new ArgumentException("value");

				IsUpdated = (bool)value.Element("IsUpdated");
				Time = (double)value.Element("Time");
				Width = (double)value.Element("Width");
			}
		}

		[DisplayName("Update")]
		public bool IsUpdated
		{
			get { return diagram.TimeManager.IsUpdated; }
			set { diagram.TimeManager.IsUpdated = value; }
		}
		[DisplayName("Time")]
		public double Time
		{
			get { return diagram.TimeManager.Time; }
			set { diagram.TimeManager.Time = value; }
		}
		[DisplayName("Width")]
		[Description("Lets you specify the width of the drawing-area in total seconds.")]
		public double Width
		{
			get { return diagram.TimeManager.Width; }
			set { diagram.TimeManager.Width = value; }
		}

		protected TimeManagerSettings(string xElementName, Diagram diagram)
			: base(xElementName)
		{
			this.diagram = diagram;
		}
	}
}
