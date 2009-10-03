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
using Graphics;
using Visualizer.Drawing.Axes;
using Visualizer.Drawing.Data;
using Visualizer.Drawing.Timing;
using Visualizer.Drawing.Values;

namespace Visualizer.Drawing
{
	public class Diagram : IComponent, IUpdateable, IDrawable
	{
		readonly IEnumerable<Graph> graphs;
		readonly Layouter layouter;
		readonly TimeManager timeManager;
		readonly ValueManager valueManager;
		readonly DataManager dataManager;
		readonly Axis axisX;
		readonly Axis axisY;

		public bool IsUpdated { get; set; }
		public bool IsDrawn { get; set; }
		public Axis AxisX { get { return axisX; } }
		public Axis AxisY { get { return axisY; } }

		public Diagram(IEnumerable<Graph> graphs, Layouter layouter, TimeManager timeManager, ValueManager valueManager, DataManager dataManager, Axis axisX, Axis axisY)
		{
			this.graphs = graphs;
			this.layouter = layouter;
			this.timeManager = timeManager;
			this.valueManager = valueManager;
			this.dataManager = dataManager;
			this.axisX = axisX;
			this.axisY = axisY;

			IsUpdated = true;
			IsDrawn = true;
		}

		public void Update()
		{
			if (IsUpdated)
			{
				// TODO: This looks random
				timeManager.Update();
				dataManager.Update();
				foreach (Graph graph in graphs) graph.Update();
				valueManager.Update();
				layouter.Update(axisY.MaximumCaptionSize.Width, axisX.MaximumCaptionSize.Height);
			}
		}
		public void Draw()
		{
			if (IsDrawn)
			{
				foreach (Graph graph in graphs) graph.Draw();

				axisX.Draw();
				axisY.Draw();
			}
		}
	}
}