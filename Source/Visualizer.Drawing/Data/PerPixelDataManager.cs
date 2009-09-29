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
using Visualizer.Data;
using Visualizer.Drawing.Timing;

namespace Visualizer.Drawing.Data
{
	public class PerPixelDataManager : DataManager
	{
		readonly Layouter layouter;
		readonly TimeManager timeManager;
		readonly double samplesPerPixel;

		public PerPixelDataManager(IEnumerable<Graph> graphs, bool dataLogging, Layouter layouter, TimeManager timeManager, double samplesPerPixel)
			: base(graphs, dataLogging)
		{
			// TODO: Do error checking (samplesPerPixel could be negative, etc.)
			// TODO: Create and document policy for error checking

			this.layouter = layouter;
			this.timeManager = timeManager;
			this.samplesPerPixel = samplesPerPixel;
		}

		public override void Update()
		{
			base.Update();

			double pixelsPerSecond = layouter.Area.Width / timeManager.Width.Seconds;
			double samplesPerSecond = samplesPerPixel * pixelsPerSecond;

			// TODO: Remove silent failure
			if (samplesPerSecond > 0)
				foreach (Graph graph in Graphs)
					graph.StreamManager.EntryResampler.SampleDistance = new Time(1.0) / samplesPerSecond;
		}
	}
}