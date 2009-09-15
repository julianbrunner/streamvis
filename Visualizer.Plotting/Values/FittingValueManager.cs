using System.Collections.Generic;
using System.Linq;
using Utility;
using Visualizer.Data;
using Visualizer.Plotting.Data;

namespace Visualizer.Plotting.Values
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
