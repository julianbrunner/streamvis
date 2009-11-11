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

using System;
using System.Collections.Generic;
using Utility;
using Utility.Extensions;
using Visualizer.Data;

namespace Visualizer.Drawing.Data
{
	public class CacheFragment
	{
		readonly Range<double> range;
		readonly IEnumerable<Entry> entries;

		public static CacheFragment Empty { get { return new CacheFragment(); } }

		public Range<double> Range { get { return range; } }
		public IEnumerable<Entry> Entries { get { return entries; } }
		public bool IsEmpty { get { return range.IsEmpty(); } }

		public CacheFragment() { }
		public CacheFragment(Range<double> range, IEnumerable<Entry> entries)
		{
			if (range.IsEmpty()) throw new ArgumentException("range");
			if (entries == null) throw new ArgumentNullException("entries");

			this.range = range;
			this.entries = entries;
		}
	}
}
