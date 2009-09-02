using System;
using System.Collections.Generic;

namespace Visualizer.Data
{
	public class CacheFragment
	{
		readonly Time startTime;
		readonly Time endTime;
		readonly IEnumerable<Entry> entries;
		
		public CacheFragment(Time startTime, Time endTIme, IEnumerable<Entry> entries)
		{
			this.startTime = startTime;
			this.endTime = endTime;
			this.entries = entries;
		}
	}
}
