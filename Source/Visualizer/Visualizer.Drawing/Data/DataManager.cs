// Copyright © Julian Brunner 2009 - 2010

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

using Visualizer.Data;
using Krach.Collections;

namespace Visualizer.Drawing.Data
{
	public abstract class DataManager
	{
		readonly Diagram diagram;

		protected Diagram Diagram { get { return diagram; } }

		public bool ClearData { get; set; }

		protected DataManager(Diagram diagram)
		{
			this.diagram = diagram;

			ClearData = false;
		}

		public virtual void Update()
		{
			if (ClearData)
				foreach (Graph graph in diagram.Graphs)
				{
					SearchList<Entry, double> entries = graph.EntryData.Entries;

					if (!entries.IsEmpty && diagram.TimeManager.Time - entries[0].Time > 2 * diagram.TimeManager.Width)
						entries.Remove(0, entries.FindIndex(diagram.TimeManager.Time - diagram.TimeManager.Width));
				}
		}

		protected static void SetSampleDistance(EntryResampler resampler, double sampleDistance)
		{
			if (resampler.SampleDistance == 0) resampler.SampleDistance = sampleDistance;
			else
			{
				double factor = sampleDistance / resampler.SampleDistance;

				if (factor < 0.8 || factor > 1.25) resampler.SampleDistance = sampleDistance;
			}
		}
	}
}
