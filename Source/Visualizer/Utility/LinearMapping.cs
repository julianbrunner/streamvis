// Copyright © Julian Brunner 2009 - 2010

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
		readonly Range<double> source;
		readonly Range<double> destination;
		readonly double offset;
		readonly double factor;

		public Range<double> Source { get { return source; } }
		public Range<double> Destination { get { return destination; } }
		public double Offset { get { return offset; } }
		public double Factor { get { return factor; } }

		public LinearMapping(Range<double> source, Range<double> destination)
		{
			this.source = source;
			this.destination = destination;

			double divisor = source.End - source.Start;
			this.offset = (source.End * destination.Start - source.Start * destination.End) / divisor;
			this.factor = (destination.End - destination.Start) / divisor;
		}
		public LinearMapping(Range<double> source) : this(source, new Range<double>(0, 1)) { }

		public double ForwardMap(double value)
		{
			return offset + value * factor;
		}
		public Range<double> ForwardMap(Range<double> range)
		{
			return new Range<double>(ForwardMap(range.Start), ForwardMap(range.End));
		}
		public double ReverseMap(double value)
		{
			return (value - offset) / factor;
		}
		public Range<double> ReverseMap(Range<double> range)
		{
			return new Range<double>(ReverseMap(range.Start), ReverseMap(range.End));
		}
	}
}
