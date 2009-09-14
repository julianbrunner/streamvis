using System.Collections.Generic;
using Visualizer.Data;

namespace Visualizer.Plotting
{
	public class DataSegment
	{
		readonly TimeRange timeRange;
		readonly IEnumerable<Entry> entries;

		public TimeRange TimeRange { get { return timeRange; } }
		public IEnumerable<Entry> Entries { get { return entries; } }

		public DataSegment(TimeRange timeRange, IEnumerable<Entry> entries)
		{
			this.timeRange = timeRange;
			this.entries = entries;
		}
	}
}
