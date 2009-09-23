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
using Visualizer.Drawing.Timing;

namespace Visualizer.Drawing.Data
{
	public class SimpleSegmentManager : SegmentManager
	{
		Dictionary<Graph, IEnumerable<DataSegment>> segments = new Dictionary<Graph, IEnumerable<DataSegment>>();

		public override IEnumerable<DataSegment> this[Graph graph] { get { return segments[graph]; } }

		public SimpleSegmentManager(TimeManager timeManager, IEnumerable<Graph> graphs) : base(timeManager, graphs) { }

		public override void Update()
		{
			segments.Clear();

			TimeRange[] graphRanges = TimeManager.GraphRanges.ToArray();

			foreach (Graph graph in Graphs)
			{
				List<DataSegment> segmentList = new List<DataSegment>();

				foreach (TimeRange timeRange in graphRanges) segmentList.Add(new DataSegment(timeRange, graph.DataManager[timeRange.Range]));

				segments[graph] = segmentList;
			}
		}
	}
}
