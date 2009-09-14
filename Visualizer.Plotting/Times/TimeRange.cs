using Extensions;
using Visualizer.Data;

namespace Visualizer.Plotting
{
	public struct TimeRange
	{
		readonly Range<Time> range;
		readonly LinearMapping mapping;

		public Range<Time> Range { get { return range; } }
		public LinearMapping Mapping { get { return mapping; } }

		public TimeRange(Range<Time> range, Range<double> output)
		{
			this.range = range;
			this.mapping = new LinearMapping(new Range<double>(range.Start.Seconds, range.End.Seconds), output);
		}
		public TimeRange(Range<Time> range) : this(range, new Range<double>(0, 1)) { }
	}
}
