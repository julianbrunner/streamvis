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

using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using Graphics;

namespace Visualizer.Drawing.Axes
{
	public abstract class Axis
	{
		readonly Drawer drawer;
		readonly Diagram diagram;

		protected Drawer Drawer { get { return drawer; } }
		protected Diagram Diagram { get { return diagram; } }
		protected abstract IEnumerable<double> Markers { get; }

		public int MarkerCount { get; set; }
		public Color Color { get; set; }
		public Size MaximumCaptionSize
		{
			get
			{
				IEnumerable<double> markers = Markers;

				int maximumWidth = markers.Any() ? markers.Max(time => Drawer.GetTextSize(time).Width) : 0;
				int maximumHeight = markers.Any() ? markers.Max(time => Drawer.GetTextSize(time).Height) : 0;

				return new Size(maximumWidth, maximumHeight);
			}
		}

		protected Axis(Drawer drawer, Diagram diagram)
		{
			this.drawer = drawer;
			this.diagram = diagram;
			
			MarkerCount = 5;
			Color = Color.White;
		}

		public virtual void Draw() { }
	}
}