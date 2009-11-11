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

namespace Visualizer.Drawing.Data
{
	public class StreamManager
	{
		readonly Diagram diagram;
		readonly EntryData entryData;
		readonly EntryResampler entryResampler;
		readonly EntryCache entryCache;

		public EntryResampler EntryResampler { get { return entryResampler; } }
		public EntryCache EntryCache { get { return entryCache; } }
		public IEnumerable<DataSegment> Segments { get; private set; }

		public StreamManager(Diagram diagram, EntryData entryData)
		{
			this.diagram = diagram;
			this.entryData = entryData;

			entryResampler = new EntryResampler(entryData.Entries);
			entryCache = new EntryCache(entryResampler);
		}

		public void Update()
		{
			entryData.UpdateEntries();

			Segments =
			(
				from timeRange in diagram.TimeManager.GraphMappings
				select new DataSegment(timeRange, entryCache[timeRange.Input])
			)
			.ToArray();

			if (!entryCache.IsEmpty && diagram.TimeManager.Time - entryCache.FirstEntry.Time > 10 * diagram.TimeManager.Width) entryCache.Clear();
		}
	}
}
