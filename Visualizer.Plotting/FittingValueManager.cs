using System.Collections.Generic;
using System.Linq;

namespace Visualizer.Plotting
{
	public class FittingValueManager : ValueManager
	{
		readonly IEnumerable<Graph> graphs;

		Range<double> range;

		public override Range<double> Range { get { return range; } }

		public FittingValueManager(IEnumerable<Graph> graphs)
		{
			this.graphs = graphs;
		}

		public override void Update()
		{
			base.Update();

			double minimum = double.NaN;
			double maximum = double.NaN;

			foreach (Graph graph in graphs.Where(graph => graph.IsDrawn))
				foreach (double value in graph.Values)
				{
					if (double.IsNaN(minimum) || value < minimum) minimum = value;
					if (double.IsNaN(maximum) || value > maximum) maximum = value;
				}

			range = new Range<double>
			(
				new Marker<double>(minimum, 0),
				new Marker<double>(maximum, 1)
			);
		}
	}
}
