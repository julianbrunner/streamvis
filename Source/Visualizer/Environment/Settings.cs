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
using Graphics;

namespace Visualizer.Environment
{
	class Settings
	{
		readonly System.Windows.Forms.PropertyGrid propertyGrid;
		readonly DiagramSettings diagram;
		readonly ViewportSettings viewport;

		[Description("Contains settings concerning the Diagram.")]
		[DisplayName("Diagram")]
		public DiagramSettings Diagram { get { return diagram; } }
		[Description("Contains settings concerning the Viewport.")]
		[DisplayName("Viewport")]
		public ViewportSettings Viewport { get { return viewport; } }

		public Settings(System.Windows.Forms.PropertyGrid propertyGrid, Viewport viewport, Timer timer, Diagram diagram)
		{
			this.diagram = new DiagramSettings(propertyGrid, timer, diagram);
			this.viewport = new ViewportSettings(viewport);
		}
	}
}
