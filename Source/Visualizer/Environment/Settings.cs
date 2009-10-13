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
		readonly DiagramSettings diagram;
		readonly ViewportSettings viewport;
		readonly DrawerSettings drawer;

		[Description("Contains settings concerning the Diagram.")]
		[DisplayName("Diagram")]
		public DiagramSettings Diagram { get { return diagram; } }
		[Description("Contains settings concerning the Viewport.")]
		[DisplayName("Viewport")]
		public ViewportSettings Viewport { get { return viewport; } }
		[Description("Contains settings concerning the Drawer.")]
		[DisplayName("Drawer")]
		public DrawerSettings Drawer { get { return drawer; } }

		public Settings(System.Windows.Forms.PropertyGrid propertyGrid, Viewport viewport, Drawer drawer, Timer timer, Diagram diagram)
		{
			this.diagram = new DiagramSettings(propertyGrid, timer, diagram);
			this.viewport = new ViewportSettings(viewport);
			this.drawer = new DrawerSettings(drawer);
		}
	}
}
