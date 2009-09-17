using System;
using System.Collections.Generic;
using Utility;
using Visualizer.Data;

namespace Visualizer.Plotting.Data
{
	public class CacheFragment
	{
		readonly Range<Time> range;
		readonly IEnumerable<Entry> entries;

		public static CacheFragment Empty { get { return new CacheFragment(); } }

		public Range<Time> Range { get { return range; } }
		public IEnumerable<Entry> Entries { get { return entries; } }
		public bool IsEmpty { get { return range.IsEmpty(); } }

		public CacheFragment() { }
		public CacheFragment(Range<Time> range, IEnumerable<Entry> entries)
		{
			if (range.IsEmpty()) throw new ArgumentException("range");
			if (entries == null) throw new ArgumentNullException("entries");

			this.range = range;
			this.entries = entries;
		}
	}
}
