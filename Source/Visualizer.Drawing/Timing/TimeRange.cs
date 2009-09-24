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

using OpenTK.Math;
using Utility;
using Visualizer.Data;

namespace Visualizer.Drawing.Timing
{
	public class TimeRange
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

		public double ForwardMap(Time time) { return mapping.ForwardMap(time.Seconds); }
		public Time ReverseMap(double value) { return new Time(mapping.ReverseMap(value)); }
	}
}
