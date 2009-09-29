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
using Visualizer.Data;
using Visualizer.Drawing.Timing;

namespace Visualizer.Drawing.Data
{
	public class StreamManager
	{
		readonly EntryData entryData;
		readonly TimeManager timeManager;
		readonly EntryResampler entryResampler;
		readonly EntryCache entryCache;

		public IEnumerable<DataSegment> Segments { get; private set; }
		// TODO: Create and document policy about when to do deep properties
		public EntryData EntryData { get { return entryData; } }
		public EntryResampler EntryResampler { get { return entryResampler; } }
		public EntryCache EntryCache { get { return entryCache; } }

		public StreamManager(EntryData entryData, TimeManager timeManager)
		{
			this.entryData = entryData;
			this.timeManager = timeManager;

			// TODO: Create and document policy about where to initialize objects
			entryResampler = new EntryResampler(entryData.Entries);
			entryCache = new EntryCache(entryResampler);
		}

		public void Update()
		{
			entryData.UpdateEntries();

			// TODO: Test if leaving out the whole segment caching is faster
			Segments =
			(
				from timeRange in timeManager.GraphRanges
				select new DataSegment(timeRange, entryCache[timeRange.Range])
			)
			.ToArray();

			if (!entryCache.IsEmpty && timeManager.Time - entryCache.FirstEntry.Time > 10 * timeManager.Width) entryCache.Clear();
		}
	}
}
