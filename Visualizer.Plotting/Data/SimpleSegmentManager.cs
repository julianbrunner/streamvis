using System.Collections.Generic;
using System.Linq;
using Visualizer.Plotting.Timing;

namespace Visualizer.Plotting.Data
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
