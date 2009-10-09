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

using System;
using System.ComponentModel;
using Visualizer.Drawing;
using Visualizer.Drawing.Timing;
using Visualizer.Data;

namespace Visualizer.Environment
{
	// TODO: Add description for properties
	[TypeConverter(typeof(ExpandableObjectConverter))]
	class DiagramSettings
	{
		readonly Timer timer;
		readonly Diagram diagram;
		
		GraphSettingsSettings graphSettings;
		LayouterSettings layouter;
		TimeManagerType timeManagerType;
		TimeManagerSettings timeManager;
		AxisXSettings axisX;
		AxisYSettings axisY;

		#region Graph Settings
		[Description("Contains setings concerning the Graphs.")]
		[DisplayName("Graph Setitings")]
		public GraphSettingsSettings GraphSettings { get{ return graphSettings; } }
		#endregion
		#region Layouter
		[Description("Contains settings concerning the Layouter.")]
		[DisplayName("Layouter")]
		public LayouterSettings Layouter { get { return layouter; } }
		#endregion
		#region Time Manager
		[DisplayName("Time Manager Type")]
		public TimeManagerType TimeManagerType
		{
			get { return timeManagerType; }
			set
			{
				timeManagerType = value;
				diagram.TimeManager = timeManagerType.Create(timer);
				timeManager = new TimeManagerSettings(diagram);
			}
		}
		[Description("Contains settings concerning the Time Manager.")]
		[DisplayName("Time Manager")]
		public TimeManagerSettings TimeManager { get { return timeManager; } }
		#endregion
		#region Axes
		[Description("Contains settings concerning the X-Axis.")]
		[DisplayName("X-Axis")]
		public AxisXSettings AxisX { get { return axisX; } }
		[Description("Contains settings concerning the Y-Axis.")]
		[DisplayName("Y-Axis")]
		public AxisYSettings AxisY { get { return axisY; } }
		#endregion

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

		public DiagramSettings(Timer timer, Diagram diagram)
		{
			this.timer = timer;
			this.diagram = diagram;

			graphSettings = new GraphSettingsSettings(diagram);
			layouter = new LayouterSettings(diagram);
			timeManager = new TimeManagerSettings(diagram);
			axisX = new AxisXSettings(diagram);
			axisY = new AxisYSettings(diagram);
		}
	}
}
