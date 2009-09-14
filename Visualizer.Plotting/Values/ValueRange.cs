using OpenTK.Math;
using Utility;

namespace Visualizer.Plotting
{
	public struct ValueRange
	{
		readonly Range<double> range;
		readonly LinearMapping mapping;
		readonly Matrix4 transformation;

		public Range<double> Range { get { return range; } }
		public LinearMapping Mapping { get { return mapping; } }
		public Matrix4 Transformation { get { return transformation; } }

		public ValueRange(Range<double> range, Range<double> output)
		{
			this.range = range;
			this.mapping = new LinearMapping(range, output);
			this.transformation = Matrix4.Scale(1, (float)mapping.Factor, 1) * Matrix4.CreateTranslation(0, (float)mapping.Offset, 0);
		}
		public ValueRange(Range<double> range) : this(range, new Range<double>(0, 1)) { }
	}
}
