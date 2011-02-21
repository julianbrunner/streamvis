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
	class PerSecondDataManagerSettings : DataManagerSettings
	{
		PerSecondDataManager PerSecondDataManager { get { return (PerSecondDataManager)Diagram.DataManager; } }

		public override XElement XElement
		{
			get
			{
				XElement xElement = base.XElement;

				xElement.Add(new XElement("SamplesPerSecond", SamplesPerSecond));

				return xElement;
			}
			set
			{
				if (value.Name != XElementName) throw new ArgumentException("value");

				SamplesPerSecond = (double)value.Element("SamplesPerSecond");
			}
		}

		[DisplayName("Samples / sec")]
		public double SamplesPerSecond
		{
			get { return PerSecondDataManager.SamplesPerSecond; }
			set { PerSecondDataManager.SamplesPerSecond = value; }
		}

		public PerSecondDataManagerSettings(string xElementName, Diagram diagram) : base(xElementName, diagram) { }
	}
}
