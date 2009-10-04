// Copyright © Julian Brunner 2009

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

using System.ComponentModel;
using Visualizer.Data;
using Visualizer.Drawing;

namespace Visualizer.Environment
{
	// TODO: Add description for properties
	[TypeConverter(typeof(ExpandableObjectConverter))]
	class TimeManagerSettings
	{
		readonly Diagram diagram;

		[DisplayName("Time")]
		public Time Time
		{
			get { return diagram.TimeManager.Time; }
			set { diagram.TimeManager.Time = value; }
		}
		[DisplayName("Width")]
		public Time Width
		{
			get { return diagram.TimeManager.Width; }
			set { diagram.TimeManager.Width = value; }
		}
		[DisplayName("Frozen")]
		public bool Frozen
		{
			get { return diagram.TimeManager.Frozen; }
			set { diagram.TimeManager.Frozen = value; }
		}

		public TimeManagerSettings(Diagram diagram)
		{
			this.diagram = diagram;
		}
	}
}
