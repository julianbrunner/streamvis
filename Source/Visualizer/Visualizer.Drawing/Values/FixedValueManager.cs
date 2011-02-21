// Copyright © Julian Brunner 2009 - 2011

// This file is part of Stream Visualizer (streamvis).
// 
// Stream Visualizer is free software: you can redistribute it and/or modify it
// under the terms of the GNU General Public License as published by the Free
// Software Foundation, either version 3 of the License, or (at your option) any
// later version.
// 
// Stream Visualizer is distributed in the hope that it will be useful, but
// WITHOUT ANY WARRANTY; without even the implied warranty of MERCHANTABILITY or
// FITNESS FOR A PARTICULAR PURPOSE. See the GNU General Public License for more
// details.
// 
// You should have received a copy of the GNU General Public License along with
// Stream Visualizer. If not, see <http://www.gnu.org/licenses/>.

using System;
using Krach.Basics;
using Krach.Maps.Scalar;
using Krach.Maps;

namespace Visualizer.Drawing.Values
{
	public class FixedValueManager : ValueManager
	{
		Range<double> fixedRange;
		
		public Range<double> FixedRange
		{
			get { return fixedRange; }
			set
			{
				if (value.End - value.Start <= 0) throw new ArgumentOutOfRangeException("value");
				
				fixedRange = value;
			}
		}
		public override Range<double> Range { get { return FixedRange; } }
		public override SymmetricRangeMap Mapping { get { return new SymmetricRangeMap(FixedRange, Mappers.Linear); } }

		public FixedValueManager()
		{
			FixedRange = new Range<double>(0, 1);
		}
	}
}
