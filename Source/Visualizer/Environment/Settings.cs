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
using Graphics;
using Visualizer.Data;
using Visualizer.Drawing;

namespace Visualizer.Environment
{
	class Settings
	{
		readonly MainWindow mainWindow;
		readonly ViewportSettings viewport;
		readonly DrawerSettings drawer;
		readonly DiagramSettings diagram;
		readonly RectangleSelectorSettings zoomSelector;
		readonly FrameCounterSettings frameCounter;

		[DisplayName("Viewport")]
		public ViewportSettings Viewport { get { return viewport; } }
		[DisplayName("Drawer")]
		public DrawerSettings Drawer { get { return drawer; } }
		[DisplayName("Diagram")]
		public DiagramSettings Diagram { get { return diagram; } }
		[DisplayName("Frame Counter")]
		public FrameCounterSettings FrameCounter { get { return frameCounter; } }
		[DisplayName("Zoom Selector")]
		public RectangleSelectorSettings ZoomSelector { get { return zoomSelector; } }
		[DisplayName("Minimal Mode")]
		public bool MinimalMode
		{
			get { return mainWindow.MinimalMode; }
			set { mainWindow.MinimalMode = value; }
		}
		[DisplayName("Show Stream List")]
		public bool StreamListVisible
		{
			get { return mainWindow.StreamListVisible; }
			set { mainWindow.StreamListVisible = value; }
		}
		[DisplayName("Show Properties")]
		public bool PropertiesVisible
		{
			get { return mainWindow.PropertiesVisible; }
			set { mainWindow.PropertiesVisible = value; }
		}

		public Settings(System.Windows.Forms.PropertyGrid propertyGrid, MainWindow mainWindow, Viewport viewport, Drawer drawer, Timer timer, Diagram diagram, RectangleSelector zoomSelector, VisibleFrameCounter frameCounter)
		{
			this.mainWindow = mainWindow;
			this.viewport = new ViewportSettings(viewport);
			this.drawer = new DrawerSettings(drawer);
			this.diagram = new DiagramSettings(propertyGrid, timer, diagram);
			this.zoomSelector = new RectangleSelectorSettings(zoomSelector);
			this.frameCounter = new FrameCounterSettings(frameCounter);
		}
	}
}
