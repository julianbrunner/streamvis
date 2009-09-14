using System.Collections.Generic;
using System.Linq;
using Visualizer.Data;

namespace Visualizer.Plotting
{
	public class FittingValueManager : ValueManager
	{
		readonly DataManager dataManager;
		readonly IEnumerable<Graph> graphs;

		_Range<double> range;

		public override _Range<double> Range { get { return range; } }

		public FittingValueManager(DataManager dataManager, IEnumerable<Graph> graphs)
		{
			this.dataManager = dataManager;
			this.graphs = graphs;
		}

		public override void Update()
		{
			base.Update();

			double minimum = double.NaN;
			double maximum = double.NaN;

			foreach (Graph graph in graphs.Where(graph => graph.IsDrawn))
				foreach (DataSegment graphSegment in dataManager[graph.EntryData])
					foreach (Entry entry in graphSegment.Entries)
					{
						if (double.IsNaN(minimum) || entry.Value < minimum) minimum = entry.Value;
						if (double.IsNaN(maximum) || entry.Value > maximum) maximum = entry.Value;
					}

			range = new _Range<double>
			(
				new Marker<double>(minimum, 0),
				new Marker<double>(maximum, 1)
			);
		}
	}
}
