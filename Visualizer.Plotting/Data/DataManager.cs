﻿using System.Collections.Generic;
using Visualizer.Plotting.Timing;

namespace Visualizer.Plotting.Data
{
	public abstract class DataManager
	{
		readonly TimeManager timeManager;
		readonly IEnumerable<Graph> graphs;

		protected TimeManager TimeManager { get { return timeManager; } }
		protected IEnumerable<Graph> Graphs { get { return graphs; } }

		public abstract IEnumerable<DataSegment> this[Graph graph] { get; }

		protected DataManager(TimeManager timeManager, IEnumerable<Graph> graphs)
		{
			this.timeManager = timeManager;
			this.graphs = graphs;
		}

		public abstract void Update();
	}
}
