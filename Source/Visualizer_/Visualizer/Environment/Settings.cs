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
using Utility;
using Visualizer.Data;
using Visualizer.Drawing;

namespace Visualizer.Environment
{
	class Settings : XSerializable
	{
		readonly MainWindow mainWindow;
		readonly ViewportSettings viewport;
		readonly DrawerSettings drawer;
		readonly DiagramSettings diagram;
		readonly RectangleSelectorSettings zoomSelector;
		readonly RectangleSelectorSettings unZoomSelector;
		readonly DraggerSettings panDragger;
		readonly FrameCounterSettings frameCounter;

		public override XElement XElement
		{
			get
			{
				return new XElement
				(
					XElementName,
					viewport.XElement,
					drawer.XElement,
					diagram.XElement,
					zoomSelector.XElement,
					unZoomSelector.XElement,
					panDragger.XElement,
					frameCounter.XElement,
					new XElement("MinimalMode", MinimalMode.ToString().ToLowerInvariant()),
					new XElement("StreamListVisible", StreamListVisible.ToString().ToLowerInvariant()),
					new XElement("PropertiesVisible", PropertiesVisible.ToString().ToLowerInvariant())
				);
			}
			set
			{
				if (value.Name != XElementName) throw new ArgumentException("value");

				viewport.XElement = value.Element(viewport.XElementName);
				drawer.XElement = value.Element(drawer.XElementName);
				diagram.XElement = value.Element(diagram.XElementName);
				zoomSelector.XElement = value.Element(zoomSelector.XElementName);
				unZoomSelector.XElement = value.Element(unZoomSelector.XElementName);
				panDragger.XElement = value.Element(panDragger.XElementName);
				frameCounter.XElement = value.Element(frameCounter.XElementName);
				MinimalMode = (bool)value.Element("MinimalMode");
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
		[DisplayName("Zoom Selector")]
		public RectangleSelectorSettings ZoomSelector { get { return zoomSelector; } }
		[DisplayName("Un-Zoom Selector")]
		public RectangleSelectorSettings UnZoomSelector { get { return unZoomSelector; } }
		[DisplayName("Pan Dragger")]
		public DraggerSettings PanDragger { get { return panDragger; } }
		[DisplayName("Frame Counter")]
		public FrameCounterSettings FrameCounter { get { return frameCounter; } }
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
			: base("Settings")
		{
			this.mainWindow = mainWindow;
			this.viewport = new ViewportSettings("ViewportSettings", viewport);
			this.drawer = new DrawerSettings("DrawerSettings", drawer);
			this.diagram = new DiagramSettings("DiagramSettings", propertyGrid, timer, diagram);
			this.zoomSelector = new RectangleSelectorSettings("ZoomSelectorSettings", zoomSelector);
			this.unZoomSelector = new RectangleSelectorSettings("UnZoomSelectorSettings", unZoomSelector);
			this.panDragger = new DraggerSettings("PanDraggerSettings", panDragger);
			this.frameCounter = new FrameCounterSettings("FrameCounterSettings", frameCounter);
		}
	}
}
