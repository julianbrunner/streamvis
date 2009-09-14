using Visualizer.Data;
using Visualizer.Plotting.Timing;

namespace Visualizer.Plotting.Data
{
	public class DataSegment
	{
		readonly TimeRange timeRange;
		readonly Entry[] entries;

		public TimeRange TimeRange { get { return timeRange; } }
		public Entry[] Entries { get { return entries; } }

		public DataSegment(TimeRange timeRange, Entry[] entries)
		{
			this.timeRange = timeRange;
			this.entries = entries;
		}
	}
}
