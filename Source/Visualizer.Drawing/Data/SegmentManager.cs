using System.Collections.Generic;
using Visualizer.Drawing.Timing;

namespace Visualizer.Drawing.Data
{
	public abstract class SegmentManager
	{
		readonly TimeManager timeManager;
		readonly IEnumerable<Graph> graphs;

		protected TimeManager TimeManager { get { return timeManager; } }
		protected IEnumerable<Graph> Graphs { get { return graphs; } }

		public abstract IEnumerable<DataSegment> this[Graph graph] { get; }
		public bool ExtendGraphs { get; set; }

		protected SegmentManager(TimeManager timeManager, IEnumerable<Graph> graphs)
		{
			this.timeManager = timeManager;
			this.graphs = graphs;
		}

		public abstract void Update();
	}
}
