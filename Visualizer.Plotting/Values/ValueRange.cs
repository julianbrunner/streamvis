using Extensions;

namespace Visualizer.Plotting
{
	public struct ValueRange
	{
		readonly Range<double> range;
		readonly LinearMapping mapping;

		public ValueRange(Range<double> range, Range<double> output)
		{
			this.range = range;
			this.mapping = new LinearMapping(range, output);
		}
		public ValueRange(Range<double> range) : this(range, new Range<double>(0, 1)) { }
	}
}
