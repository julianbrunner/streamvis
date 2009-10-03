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
using Visualizer.Drawing;

namespace Visualizer.Environment
{
	// TODO: Add description for properties
	[TypeConverter(typeof(ExpandableObjectConverter))]
	class DiagramSettings
	{
		readonly Diagram diagram;
		readonly GraphSettingsSettings graphSettings;
		readonly AxisSettings axisX;
		readonly AxisSettings axisY;

		[DisplayName("Update")]
		public bool IsUpdated
		{
			get { return diagram.IsUpdated; }
			set { diagram.IsUpdated = value; }
		}
		[DisplayName("Draw")]
		public bool IsDrawn
		{
			get { return diagram.IsDrawn; }
			set { diagram.IsDrawn = value; }
		}

		[Description("Contains settings concerning the Graphs.")]
		[DisplayName("Graph Settings")]
		public GraphSettingsSettings GraphSettings { get { return graphSettings; } }
		[DisplayName("X-Axis")]
		public AxisSettings AxisX { get { return axisX; } }
		[DisplayName("Y-Axis")]
		public AxisSettings AxisY { get { return axisY; } }

		public DiagramSettings(Diagram diagram)
		{
			this.diagram = diagram;

			this.graphSettings = new GraphSettingsSettings(diagram.GraphSettings);
			this.axisX = new AxisSettings(diagram.AxisX);
			this.axisY = new AxisSettings(diagram.AxisY);
		}
	}
}
