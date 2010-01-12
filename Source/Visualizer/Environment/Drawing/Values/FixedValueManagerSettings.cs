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
using Visualizer.Drawing.Values;

namespace Visualizer.Environment.Drawing.Values
{
	[TypeConverter(typeof(ExpansionConverter))]
	class FixedValueManagerSettings : ValueManagerSettings
	{
		FixedValueManager FixedValueManager { get { return (FixedValueManager)Diagram.ValueManager; } }

		public override XElement XElement
		{
			get
			{
				XElement xElement = base.XElement;

				xElement.Add(new XElement("RangeStart", RangeStart));
				xElement.Add(new XElement("RangeEnd", RangeEnd));

				return xElement;
			}
			set
			{
				if (value.Name != XElementName) throw new ArgumentException("value");

				RangeStart = (double)value.Element("RangeStart");
				RangeEnd = (double)value.Element("RangeEnd");
			}
		}

		[DisplayName("Range Start")]
		public double RangeStart
		{
			get { return FixedValueManager.FixedRange.Start; }
			set { FixedValueManager.FixedRange = new Range<double>(value, FixedValueManager.FixedRange.End); }
		}
		[DisplayName("Range End")]
		public double RangeEnd
		{
			get { return FixedValueManager.FixedRange.End; }
			set { FixedValueManager.FixedRange = new Range<double>(FixedValueManager.FixedRange.Start, value); }
		}

		public FixedValueManagerSettings(string xElementName, Diagram diagram) : base(xElementName, diagram) { }
	}
}
