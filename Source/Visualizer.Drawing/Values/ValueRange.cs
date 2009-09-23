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

namespace Visualizer.Drawing.Values
{
	public class ValueRange
	{
		readonly Range<double> range;
		readonly LinearMapping mapping;
		readonly Matrix4 transformation;

		public double this[double value] { get { return mapping[value]; } }

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
