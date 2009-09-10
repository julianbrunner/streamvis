using System.Collections.Generic;
using Visualizer.Data;

namespace Visualizer.Plotting
{
	public class DataSegment
	{
		readonly Range<Time> timeRange;
		readonly IEnumerable<Entry> entries;

		public Range<Time> TimeRange { get { return timeRange; } }
		public IEnumerable<Entry> Entries { get { return entries; } }

		public DataSegment(Range<Time> timeRange, IEnumerable<Entry> entries)
		{
			this.timeRange = timeRange;
			this.entries = entries;
		}
	}
}
