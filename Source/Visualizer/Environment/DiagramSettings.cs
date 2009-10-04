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
		readonly LayouterSettings layouter;
		readonly TimeManagerSettings timeManager;
		readonly AxisXSettings axisX;
		readonly AxisYSettings axisY;

		[Description("Contains settings concerning the Graphs.")]
		[DisplayName("Graph Setitings")]
		public GraphSettingsSettings GraphSettings { get { return graphSettings; } }
		[Description("Contains settings concerning the Layouter.")]
		[DisplayName("Layouter")]
		public LayouterSettings Layouter { get { return layouter; } }
		[Description("Contains settings concerning the Time Manager.")]
		[DisplayName("Time Manager")]
		public TimeManagerSettings TimeManager { get { return timeManager; } }
		[Description("Contains settings concerning the X-Axis.")]
		[DisplayName("X-Axis")]
		public AxisXSettings AxisX { get { return axisX; } }
		[Description("Contains settings concerning the Y-Axis.")]
		[DisplayName("Y-Axis")]
		public AxisYSettings AxisY { get { return axisY; } }

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

		public DiagramSettings(Diagram diagram)
		{
			this.diagram = diagram;

			this.graphSettings = new GraphSettingsSettings(diagram);
			this.layouter = new LayouterSettings(diagram);
			this.timeManager = new TimeManagerSettings(diagram);
			this.axisX = new AxisXSettings(diagram);
			this.axisY = new AxisYSettings(diagram);
		}
	}
}
