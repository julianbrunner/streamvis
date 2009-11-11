// Copyright © Julian Brunner 2009

// This file is part of Stream Visualizer (streamvis).
// 
// Stream Visualizer is free software: you can redistribute it and/or modify
// it under the terms of the GNU General Public License as published by
// the Free Software Foundation, either version 3 of the License, or
// (at your option) any later version.
// 
// Stream Visualizer is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU General Public License for more details.
// 
// You should have received a copy of the GNU General Public License
// along with Stream Visualizer.  If not, see <http://www.gnu.org/licenses/>.

namespace Utility
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

		public LinearMapping(Range<double> input, Range<double> output)
		{
			this.input = input;
			this.output = output;

			double divisor = input.End - input.Start;
			this.offset = (input.End * output.Start - input.Start * output.End) / divisor;
			this.factor = (output.End - output.Start) / divisor;
		}
		public LinearMapping(Range<double> input) : this(input, new Range<double>(0, 1)) { }

		public double ForwardMap(double value)
		{
			return offset + value * factor;
		}
		public double ReverseMap(double value)
		{
			return (value - offset) / factor;
		}
	}
}
