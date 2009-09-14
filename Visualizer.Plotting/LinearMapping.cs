using Extensions;

namespace Visualizer.Plotting
{
	public struct LinearMapping
	{
		readonly Range<double> input;
		readonly Range<double> output;

		public Range<double> Input { get { return input; } }
		public Range<double> Output { get { return output; } }

		public double this[double value] { get { return ToOutput(FromInput(value)); } }

		public LinearMapping(Range<double> input, Range<double> output)
		{
			this.input = input;
			this.output = output;
		}

		public double FromInput(double value)
		{
			return (value - input.Start) / (input.End - input.Start);
		}
		public double ToOutput(double value)
		{
			return output.Start + value * (output.End - output.Start);
		}
	}
}
