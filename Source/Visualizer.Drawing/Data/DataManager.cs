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
using Visualizer.Data;

namespace Visualizer.Drawing.Data
{
	public abstract class DataManager
	{
		readonly EntryData entryData;
		readonly EntryResampler entryResampler;
		readonly EntryCache entryCache;

		public Entry[] this[Range<Time> range] { get { return entryCache[range]; } }

		protected EntryData EntryData { get { return entryData; } }
		protected EntryResampler EntryResampler { get { return entryResampler; } }
		protected EntryCache EntryCache { get { return entryCache; } }

		public Time SampleDistance { get { return entryResampler.SampleDistance; } }
		public bool IsEmpty { get { return entryCache.IsEmpty; } }
		public Entry FirstEntry { get { return entryCache.FirstEntry; } }
		public Entry LastEntry { get { return entryCache.LastEntry; } }

		protected DataManager(EntryData entryData)
		{
			this.entryData = entryData;
			this.entryResampler = new EntryResampler(entryData.Entries);
			this.entryCache = new EntryCache(entryResampler);
		}

		public virtual void Update()
		{
			entryData.Update();
		}
	}
}
