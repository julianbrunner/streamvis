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
using System.Linq;
using Utility;
using Visualizer.Data;
using Visualizer.Drawing.Data;

namespace Visualizer.Drawing.Values
{
	public class FittingValueManager : ValueManager
	{
		readonly SegmentManager segmentManager;
		readonly IEnumerable<Graph> graphs;

		ValueRange range;

		public override ValueRange Range { get { return range; } }

		public FittingValueManager(SegmentManager segmentManager, IEnumerable<Graph> graphs)
		{
			this.segmentManager = segmentManager;
			this.graphs = graphs;
		}

		public override void Update()
		{
			base.Update();

			double minimum = double.NaN;
			double maximum = double.NaN;

			foreach (Graph graph in graphs.Where(graph => graph.IsDrawn))
				foreach (DataSegment graphSegment in segmentManager[graph])
					foreach (Entry entry in graphSegment.Entries)
					{
						if (double.IsNaN(minimum) || entry.Value < minimum) minimum = entry.Value;
						if (double.IsNaN(maximum) || entry.Value > maximum) maximum = entry.Value;
					}

			range = new ValueRange(new Range<double>(minimum, maximum));
		}
	}
}
