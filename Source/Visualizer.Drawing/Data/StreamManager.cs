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

using Utility;
using System.Linq;
using Visualizer.Data;
using Visualizer.Drawing.Timing;
using System.Collections.Generic;

namespace Visualizer.Drawing.Data
{
	public class StreamManager
	{
		readonly TimeManager timeManager;
		readonly EntryData entryData;
		readonly bool dataLogging;
		readonly EntryResampler entryResampler;
		readonly EntryCache entryCache;

		// TODO: Create and document visibility policy
		protected TimeManager TimeManager { get { return timeManager; } }
		protected EntryData EntryData { get { return entryData; } }
		protected EntryResampler EntryResampler { get { return entryResampler; } }
		protected EntryCache EntryCache { get { return entryCache; } }

		public IEnumerable<DataSegment> Segments { get; private set; }
		// TODO: Create and document policy about when to do deep properties
		public Time SampleDistance { get { return entryResampler.SampleDistance; } }
		public bool IsEmpty { get { return entryCache.IsEmpty; } }
		public Entry FirstEntry { get { return entryCache.FirstEntry; } }
		public Entry LastEntry { get { return entryCache.LastEntry; } }

		protected StreamManager(TimeManager timeManager, EntryData entryData, bool dataLogging)
		{
			this.timeManager = timeManager;
			this.entryData = entryData;
			this.dataLogging = dataLogging;

			// TODO: Create and document policy about where to initialize objects
			entryResampler = new EntryResampler(entryData.Entries);
			entryCache = new EntryCache(entryResampler);
		}

		public void Update()
		{
			entryData.UpdateEntries();

			if (!dataLogging && entryData.Entries.Count > 0 && timeManager.Time - entryData.Entries[0].Time > 2 * timeManager.Width)
				entryData.Entries.Remove(0, entryData.Entries.FindIndex(timeManager.Time - timeManager.Width));

			if (!entryCache.IsEmpty && timeManager.Time - entryCache.FirstEntry.Time > 10 * timeManager.Width) entryCache.Clear();

			Segments = (from timeRange in timeManager.GraphRanges select new DataSegment(timeRange, entryCache[timeRange.Range])).ToArray();
		}
	}
}
