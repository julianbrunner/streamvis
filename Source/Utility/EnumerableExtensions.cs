// Copyright � Julian Brunner 2009

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

namespace Utility
{
	public static class EnumerableExtensions
	{
		public static IEnumerable<T> Separate<T>(this IEnumerable<T> source, T separator)
		{
			if (source == null) throw new ArgumentNullException("source");

			bool first = true;

			foreach (T item in source)
			{
				if (first) first = false;
				else yield return separator;
				yield return item;
			}
		}
		public static IEnumerable<T> Concat<T>(this IEnumerable<T> source, T item)
		{
			if (source == null) throw new ArgumentNullException("source");

			return source.Concat(item.Single());
		}
		public static IEnumerable<Range<T>> GetRanges<T>(this IEnumerable<T> source)
		{
			if (source == null) throw new ArgumentNullException("source");

			IEnumerator<T> enumerator = source.GetEnumerator();

			if (!enumerator.MoveNext()) yield break;

			T last = enumerator.Current;

			while (enumerator.MoveNext()) yield return new Range<T>(last, last = enumerator.Current);
		}
		public static IEnumerable<string> ToStrings<T>(this IEnumerable<T> source)
		{
			if (source == null) throw new ArgumentNullException("source");

			foreach (T item in source) yield return item.ToString();
		}
		public static string AggregateString<T>(this IEnumerable<T> source)
		{
			if (source == null) throw new ArgumentNullException("source");

			return source.Aggregate(string.Empty, (seed, current) => seed + current);
		}
	}
}
