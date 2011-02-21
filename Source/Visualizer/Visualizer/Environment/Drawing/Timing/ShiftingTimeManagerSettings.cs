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
using Visualizer.Drawing.Timing;

namespace Visualizer.Environment.Drawing.Timing
{
	[TypeConverter(typeof(ExpansionConverter))]
	class ShiftingTimeManagerSettings : TimeManagerSettings
	{
		ShiftingTimeManager ShiftingTimeManager { get { return (ShiftingTimeManager)Diagram.TimeManager; } }

		public override XElement XElement
		{
			get
			{
				XElement xElement = base.XElement;

				xElement.Add(new XElement("ShiftLength", ShiftLength));

				return xElement;
			}
			set
			{
				if (value.Name != XElementName) throw new ArgumentException("value");

				ShiftLength = (double)value.Element("ShiftLength");
			}
		}

		[DisplayName("Shift Length")]
		public double ShiftLength
		{
			get { return ShiftingTimeManager.ShiftLength; }
			set { ShiftingTimeManager.ShiftLength = value; }
		}

		public ShiftingTimeManagerSettings(string xElementName, Diagram diagram) : base(xElementName, diagram) { }
	}
}
