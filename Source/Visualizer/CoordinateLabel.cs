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
using System.Drawing;
using System.Windows.Forms;
using Graphics;
using OpenTK.Math;
using Visualizer.Data;
using Visualizer.Drawing;
using Visualizer.Drawing.Timing;
using Visualizer.Drawing.Values;

namespace Visualizer
{
	class CoordinateLabel : IComponent, IUpdateable, IDrawable
	{
		readonly ToolStripStatusLabel label;
		readonly Viewport viewport;
		readonly Diagram diagram;

		bool mouseInsideViewport = false;
		Point mousePosition;

		public CoordinateLabel(ToolStripStatusLabel label, Viewport viewport, Diagram diagram)
		{
			this.label = label;
			this.viewport = viewport;
			this.diagram = diagram;

			this.viewport.MouseEnter += viewport_MouseEnter;
			this.viewport.MouseLeave += viewport_MouseLeave;
			this.viewport.MouseMove += viewport_MouseMove;
		}

		public void Update()
		{
			//label.Visible = mouseInsideViewport && diagram.Layouter.Area.Contains(mousePosition);
			label.Visible = true;
		}
		public void Draw()
		{
			//if (label.Visible)
			{
				//Vector2 position = diagram.Layouter.ReverseMap(new Vector2(mousePosition.X, mousePosition.Y));

				//TimeRange timeRange = diagram.TimeManager.Range;
				//ValueRange valueRange = diagram.ValueManager.Range;

				//Time time = timeRange.ReverseMap(position.X);
				//double value = valueRange.ReverseMap(position.Y);

				label.Text = string.Format("Time: {0}, Value: {1}, I: {2}, C: {3}", 0, 0, mouseInsideViewport, diagram.Layouter.Area.Contains(mousePosition));
			}
		}

		void viewport_MouseEnter(object sender, EventArgs e)
		{
			mouseInsideViewport = true;
		}
		void viewport_MouseLeave(object sender, EventArgs e)
		{
			mouseInsideViewport = false;
		}
		void viewport_MouseMove(object sender, MouseEventArgs e)
		{
			mousePosition = e.Location;
		}
	}
}
