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
using Visualizer.Drawing.Timing;
using Visualizer.Data;
using Utility;

namespace Visualizer.Drawing.Data
{
	public abstract class DataManager
	{
		readonly IEnumerable<Graph> graphs;
		readonly TimeManager timeManager;
		readonly bool dataLogging;

		protected IEnumerable<Graph> Graphs { get { return graphs; } }

		protected DataManager(IEnumerable<Graph> graphs, TimeManager timeManager, bool dataLogging)
		{
			this.graphs = graphs;
			this.timeManager = timeManager;
			this.dataLogging = dataLogging;
		}

		public virtual void Update()
		{
			if (!dataLogging)
				foreach (Graph graph in graphs)
				{
					SearchList<Entry, Time> entries = graph.EntryData.Entries;

					if (entries.Count > 0 && timeManager.Time - entries[0].Time > 2 * timeManager.Width)
						entries.Remove(0, entries.FindIndex(timeManager.Time - timeManager.Width));
				}
		}
	}
}
