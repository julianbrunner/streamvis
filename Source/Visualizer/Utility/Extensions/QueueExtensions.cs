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

using System;
using System.Collections.Generic;
using System.Linq;

namespace Utility.Extensions
{
	public static class QueueExtensions
	{
		public static IEnumerable<T> Dequeue<T>(this Queue<T> source, Func<T, bool> predicate)
		{
			if (source == null) throw new ArgumentNullException("source");
			if (predicate == null) throw new ArgumentNullException("predicate");

			List<T> result = new List<T>();

			while (source.Any())
			{
				if (!predicate(source.Peek())) break;

				result.Add(source.Dequeue());
			}

			return result;
		}
		public static IEnumerable<T> Dequeue<T>(this Queue<T> source, int count)
		{
			if (source == null) throw new ArgumentNullException("source");
			if (count > source.Count) throw new ArgumentOutOfRangeException("count");

			List<T> result = new List<T>();
			
			for (int i = 0; i < count; i++) result.Add(source.Dequeue());

			return result;
		}
	}
}
