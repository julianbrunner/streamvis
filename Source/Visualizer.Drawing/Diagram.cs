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
		public IEnumerable<Graph> Graphs { get; set; }
		public GraphSettings GraphSettings { get; set; }
		public TimeManager TimeManager { get; set; }
		public ValueManager ValueManager { get; set; }
		public DataManager DataManager { get; set; }
		public Axis AxisX { get; set; }
		public Axis AxisY { get; set; }
		public Layouter Layouter { get; set; }

		public bool IsUpdated { get; set; }
		public bool IsDrawn { get; set; }

		public Diagram()
		{
			IsUpdated = true;
			IsDrawn = true;
		}

		public void Update()
		{
			if (IsUpdated)
			{
				TimeManager.Update();
				DataManager.Update();
				foreach (Graph graph in Graphs) graph.Update();
				ValueManager.Update();
				Layouter.Update();
			}
		}
		public void Draw()
		{
			if (IsDrawn)
			{
				foreach (Graph graph in Graphs) graph.Draw();

				AxisX.Draw();
				AxisY.Draw();
			}
		}
	}
}