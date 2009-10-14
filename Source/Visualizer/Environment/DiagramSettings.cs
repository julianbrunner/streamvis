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
using Visualizer.Data;
using Visualizer.Drawing;
using Visualizer.Drawing.Data;
using Visualizer.Drawing.Timing;
using Visualizer.Drawing.Values;
using Visualizer.Environment.Drawing;
using Visualizer.Environment.Drawing.Data;
using Visualizer.Environment.Drawing.Timing;
using Visualizer.Environment.Drawing.Values;

namespace Visualizer.Environment
{
	[TypeConverter(typeof(ExpansionConverter))]
	class DiagramSettings
	{
		readonly System.Windows.Forms.PropertyGrid propertyGrid;
		readonly Timer timer;
		readonly Diagram diagram;

		GraphSettingsSettings graphSettings;
		LayouterSettings layouter;
		TimeManagerType timeManagerType;
		TimeManagerSettings timeManager;
		ValueManagerType valueManagerType;
		ValueManagerSettings valueManager;
		DataManagerType dataManagerType;
		DataManagerSettings dataManager;
		AxisXSettings axisX;
		AxisYSettings axisY;

		#region Graph Settings
		[DisplayName("Graph Setitings")]
		public GraphSettingsSettings GraphSettings { get { return graphSettings; } }
		#endregion
		#region Layouter
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
				switch (value)
				{
					case TimeManagerType.Continuous: diagram.TimeManager = new ContinuousTimeManager(timer); break;
					case TimeManagerType.Shiftting: diagram.TimeManager = new ShiftingTimeManager(timer); break;
					case TimeManagerType.Wrapping: diagram.TimeManager = new WrappingTimeManager(timer); break;
					default: throw new InvalidOperationException();
				}

				Initialize();
			}
		}
		[DisplayName("Time Manager")]
		public TimeManagerSettings TimeManager { get { return timeManager; } }
		#endregion
		#region Value Manager
		[DisplayName("Value Manager Type")]
		public ValueManagerType ValueManagerType
		{
			get { return valueManagerType; }
			set
			{
				switch (value)
				{
					case ValueManagerType.Fixed: diagram.ValueManager = new FixedValueManager(); break;
					case ValueManagerType.Fitting: diagram.ValueManager = new FittingValueManager(diagram); break;
					default: throw new InvalidOperationException();
				}

				Initialize();
			}
		}
		[DisplayName("Value Manager")]
		public ValueManagerSettings ValueManager { get { return valueManager; } }
		#endregion
		#region Data Manager
		[DisplayName("Data Manager Type")]
		public DataManagerType DataManagerType
		{
			get { return dataManagerType; }
			set
			{
				switch (value)
				{
					case DataManagerType.PerSecond: diagram.DataManager = new PerSecondDataManager(diagram); break;
					case DataManagerType.PerPixel: diagram.DataManager = new PerPixelDataManager(diagram); break;
					default: throw new InvalidOperationException();
				}

				Initialize();
			}
		}
		[DisplayName("Data Manager")]
		public DataManagerSettings DataManager { get { return dataManager; } }
		#endregion
		#region Axes
		[DisplayName("X-Axis")]
		public AxisXSettings AxisX { get { return axisX; } }
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

		public DiagramSettings(System.Windows.Forms.PropertyGrid propertyGrid, Timer timer, Diagram diagram)
		{
			this.propertyGrid = propertyGrid;
			this.timer = timer;
			this.diagram = diagram;

			Initialize();
		}

		void Initialize()
		{
			graphSettings = new GraphSettingsSettings(diagram);
			layouter = new LayouterSettings(diagram);

			switch (timeManagerType = GetTimeManagerType(diagram.TimeManager))
			{
				case TimeManagerType.Continuous: timeManager = new ContinuousTimeManagerSettings(diagram); break;
				case TimeManagerType.Shiftting: timeManager = new ShiftingTimeManagerSettings(diagram); break;
				case TimeManagerType.Wrapping: timeManager = new WrappingTimeManagerSettings(diagram); break;
				default: throw new InvalidOperationException();
			}

			switch (valueManagerType = GetValueManagerType(diagram.ValueManager))
			{
				case ValueManagerType.Fixed: valueManager = new FixedValueManagerSettings(diagram); break;
				case ValueManagerType.Fitting: valueManager = new FittingValueManagerSettings(diagram); break;
				default: throw new InvalidOperationException();
			}

			switch (dataManagerType = GetDataManagerType(diagram.DataManager))
			{
				case DataManagerType.PerSecond: dataManager = new PerSecondDataManagerSettings(diagram); break;
				case DataManagerType.PerPixel: dataManager = new PerPixelDataManagerSettings(diagram); break;
				default: throw new InvalidOperationException();
			}

			axisX = new AxisXSettings(diagram);
			axisY = new AxisYSettings(diagram);

			propertyGrid.Refresh();
		}

		static TimeManagerType GetTimeManagerType(TimeManager timeManager)
		{
			if (timeManager is ContinuousTimeManager) return TimeManagerType.Continuous;
			if (timeManager is ShiftingTimeManager) return TimeManagerType.Shiftting;
			if (timeManager is WrappingTimeManager) return TimeManagerType.Wrapping;

			throw new ArgumentException();
		}
		static ValueManagerType GetValueManagerType(ValueManager valueManager)
		{
			if (valueManager is FixedValueManager) return ValueManagerType.Fixed;
			if (valueManager is FittingValueManager) return ValueManagerType.Fitting;

			throw new ArgumentException();
		}
		static DataManagerType GetDataManagerType(DataManager dataManager)
		{
			if (dataManager is PerSecondDataManager) return DataManagerType.PerSecond;
			if (dataManager is PerPixelDataManager) return DataManagerType.PerPixel;

			throw new ArgumentException();
		}
	}
}
