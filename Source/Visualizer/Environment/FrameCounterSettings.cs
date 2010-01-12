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
using Graphics;
using Utility;
using Utility.Extensions;
using Utility.Utilities;

namespace Visualizer.Environment
{
	[TypeConverter(typeof(ExpansionConverter))]
	class FrameCounterSettings : XSerializable
	{
		readonly VisibleFrameCounter frameCounter;

		public override XElement XElement
		{
			get
			{
				return new XElement
				(
					XElementName,
					new XElement("IsUpdated", IsUpdated),
					new XElement("IsDrawn", IsDrawn),
					new XElement("CycleLength", CycleLength),
					new XElement("Color", Color.ToHtmlString())
				);
			}
			set
			{
				if (value.Name != XElementName) throw new ArgumentException("value");

				IsUpdated = (bool)value.Element("IsUpdated");
				IsDrawn = (bool)value.Element("IsDrawn");
				CycleLength = (int)value.Element("CycleLength");
				Color = ColorUtility.FromHtmlString((string)value.Element("Color"));
			}
		}

		[DisplayName("Update")]
		public bool IsUpdated
		{
			get { return frameCounter.IsUpdated; }
			set { frameCounter.IsUpdated = value; }
		}
		[DisplayName("Draw")]
		public bool IsDrawn
		{
			get { return frameCounter.IsDrawn; }
			set { frameCounter.IsDrawn = value; }
		}
		[DisplayName("Cycle Length")]
		public int CycleLength
		{
			get { return frameCounter.CycleLength; }
			set { frameCounter.CycleLength = value; }
		}
		[DisplayName("Color")]
		public Color Color
		{
			get { return frameCounter.Color; }
			set { frameCounter.Color = value; }
		}

		public FrameCounterSettings(string xElementName, VisibleFrameCounter frameCounter)
			: base(xElementName)
		{
			this.frameCounter = frameCounter;
		}
	}
}
