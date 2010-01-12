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
using System.Xml.Linq;
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
		readonly RectangleSelectorSettings unZoomSelector;
		readonly DraggerSettings panDragger;
		readonly FrameCounterSettings frameCounter;

		public static string XElementName { get { return "Settings"; } }

		public XElement XElement
		{
			get
			{
				return new XElement
				(
					XElementName,
					viewport.XElement,
					drawer.XElement,
					new XElement("MinimalMode", MinimalMode),
					new XElement("StreamListVisible", StreamListVisible),
					new XElement("PropertiesVisible", PropertiesVisible)
				);
			}
			set
			{
				if (value.Name != XElementName) throw new ArgumentException("value");

				viewport.XElement = value.Element(ViewportSettings.XElementName);
				drawer.XElement = value.Element(DrawerSettings.XElementName);
				MinimalMode = (bool)value.Element("VerticalSynchronization");
				StreamListVisible = (bool)value.Element("StreamListVisible");
				PropertiesVisible = (bool)value.Element("PropertiesVisible");
			}
		}

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
		[DisplayName("Un-Zoom Selector")]
		public RectangleSelectorSettings UnZoomSelector { get { return unZoomSelector; } }
		[DisplayName("Pan Dragger")]
		public DraggerSettings PanDragger { get { return panDragger; } }
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

		public Settings(System.Windows.Forms.PropertyGrid propertyGrid, MainWindow mainWindow, Viewport viewport, Drawer drawer, Timer timer, Diagram diagram, RectangleSelector zoomSelector, RectangleSelector unZoomSelector, Dragger panDragger, VisibleFrameCounter frameCounter)
		{
			this.mainWindow = mainWindow;
			this.viewport = new ViewportSettings(viewport);
			this.drawer = new DrawerSettings(drawer);
			this.diagram = new DiagramSettings(propertyGrid, timer, diagram);
			this.zoomSelector = new RectangleSelectorSettings(zoomSelector);
			this.unZoomSelector = new RectangleSelectorSettings(unZoomSelector);
			this.panDragger = new DraggerSettings(panDragger);
			this.frameCounter = new FrameCounterSettings(frameCounter);
		}
	}
}
