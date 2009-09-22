﻿namespace Utility
{
	public class LinearMapping
	{
		readonly Range<double> input;
		readonly Range<double> output;
		readonly double offset;
		readonly double factor;

		public Range<double> Input { get { return input; } }
		public Range<double> Output { get { return output; } }
		public double Offset { get { return offset; } }
		public double Factor { get { return factor; } }

		public double this[double value] { get { return offset + value * factor; } }

		public LinearMapping(Range<double> input, Range<double> output)
		{
			this.input = input;
			this.output = output;

			double divisor = input.End - input.Start;
			this.offset = (input.End * output.Start - input.Start * output.End) / divisor;
			this.factor = (output.End - output.Start) / divisor;
		}
	}
}