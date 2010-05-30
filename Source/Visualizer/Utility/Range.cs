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
	public struct Range<T>
	{
		readonly T start;
		readonly T end;

		public T Start { get { return start; } }
		public T End { get { return end; } }

		public Range(T start, T end)
		{
			this.start = start;
			this.end = end;
		}

		public override string ToString()
		{
			return start + " - " + end;
		}
	}
}
