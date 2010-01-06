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

namespace Visualizer.Drawing.Data
{
	public class PerSecondDataManager : DataManager
	{
		double samplesPerSecond = 0;
		
		public double SamplesPerSecond
		{
			get { return samplesPerSecond; }
			set
			{
				if (value <= 0) throw new ArgumentOutOfRangeException("value");
				
				samplesPerSecond = value;
			}
		}

		public PerSecondDataManager(Diagram diagram)
			: base(diagram)
		{
			SamplesPerSecond = 10;
		}

		public override void Update()
		{
			base.Update();

			foreach (Graph graph in Diagram.Graphs)
				SetSampleDistance(graph.StreamManager.EntryResampler, 1.0 / SamplesPerSecond);
		}
	}
}
