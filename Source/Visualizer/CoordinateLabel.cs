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
	class CoordinateLabel : IComponent, IDrawable
	{
		readonly ToolStripStatusLabel label;
		readonly Viewport viewport;
		readonly Layouter layouter;
		readonly TimeManager timeManager;
		readonly ValueManager valueManager;

		Point mousePosition;

		public CoordinateLabel(ToolStripStatusLabel label, Viewport viewport, Layouter layouter, TimeManager timeManager, ValueManager valueManager)
		{
			this.label = label;
			this.viewport = viewport;
			this.layouter = layouter;
			this.timeManager = timeManager;
			this.valueManager = valueManager;

			this.viewport.MouseEnter += viewport_MouseEnter;
			this.viewport.MouseLeave += viewport_MouseLeave;
			this.viewport.MouseMove += viewport_MouseMove;
		}

		public void Draw()
		{
			if (label.Visible)
			{
				Vector2 position = layouter.ReverseMap(new Vector2(mousePosition.X, mousePosition.Y));

				TimeRange timeRange = timeManager.Range;
				ValueRange valueRange = valueManager.Range;

				Time time = timeRange.ReverseMap(position.X);
				double value = valueRange.ReverseMap(position.Y);

				label.Text = string.Format("Time: {0}, Value: {1}", time, value);
			}
		}

		void viewport_MouseEnter(object sender, EventArgs e)
		{
			label.Visible = true;
		}
		void viewport_MouseLeave(object sender, EventArgs e)
		{
			label.Visible = false;
		}
		void viewport_MouseMove(object sender, MouseEventArgs e)
		{
			mousePosition = e.Location;

			label.Visible = layouter.Area.Contains(e.Location);
		}
	}
}
