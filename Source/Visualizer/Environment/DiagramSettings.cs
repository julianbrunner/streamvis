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

using Visualizer.Drawing;
using System.Drawing;
using System.ComponentModel;

namespace Visualizer.Environment
{
	// TODO: Add description for properties
	[TypeConverter(typeof(ExpandableObjectConverter))]
	class DiagramSettings
	{
		[Category("Behaviour")]
		[DisplayName("Update")]
		public bool IsUpdated { get; set; }
		[Category("Behaviour")]
		[DisplayName("Draw")]
		public bool IsDrawn { get; set; }

		[Category("Appearance")]
		[DisplayName("Extend Graphs")]
		public bool ExtendGraphs { get; set; }
		[Category("Appearance")]
		[DisplayName("Line Width")]
		public double LineWidth { get; set; }
		[Category("Appearance")]
		[DisplayName("X-Axis Markers")]
		public int MarkersX { get; set; }
		[Category("Appearance")]
		[DisplayName("Y-Axis Markers")]
		public int MarkersY { get; set; }
		[Category("Appearance")]
		[DisplayName("Color")]
		public Color Color { get; set; }


	}
}
