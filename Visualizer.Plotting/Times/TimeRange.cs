using Extensions;
using OpenTK.Math;
using Visualizer.Data;

namespace Visualizer.Plotting
{
	public struct TimeRange
	{
		readonly Range<Time> range;
		readonly LinearMapping mapping;
		readonly Matrix4 transformation;

		public Range<Time> Range { get { return range; } }
		public LinearMapping Mapping { get { return mapping; } }
		public Matrix4 Transformation { get { return transformation; } }

		public TimeRange(Range<Time> range, Range<double> output)
		{
			this.range = range;
			this.mapping = new LinearMapping(new Range<double>(range.Start.Seconds, range.End.Seconds), output);
			this.transformation = Matrix4.Scale((float)mapping.Factor, 1, 1) * Matrix4.CreateTranslation((float)mapping.Offset, 0, 0);
		}
		public TimeRange(Range<Time> range) : this(range, new Range<double>(0, 1)) { }
	}
}
