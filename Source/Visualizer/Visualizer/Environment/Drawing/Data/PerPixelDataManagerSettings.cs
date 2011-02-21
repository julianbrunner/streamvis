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
using Visualizer.Drawing.Data;

namespace Visualizer.Environment.Drawing.Data
{
	[TypeConverter(typeof(ExpansionConverter))]
	class PerPixelDataManagerSettings : DataManagerSettings
	{
		PerPixelDataManager PerPixelDataManager { get { return (PerPixelDataManager)Diagram.DataManager; } }

		public override XElement XElement
		{
			get
			{
				XElement xElement = base.XElement;

				xElement.Add(new XElement("SamplesPerPixel", SamplesPerPixel));

				return xElement;
			}
			set
			{
				if (value.Name != XElementName) throw new ArgumentException("value");

				SamplesPerPixel = (double)value.Element("SamplesPerPixel");
			}
		}

		[DisplayName("Samples / pixel")]
		public double SamplesPerPixel
		{
			get { return PerPixelDataManager.SamplesPerPixel; }
			set { PerPixelDataManager.SamplesPerPixel = value; }
		}

		public PerPixelDataManagerSettings(string xElementName, Diagram diagram) : base(xElementName, diagram) { }
	}
}
