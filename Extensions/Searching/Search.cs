using System;
using System.Collections.Generic;

namespace Extensions.Searching
{
	public static class Search
	{
		public static IEnumerable<TItem> FindRange<TItem, TPosition>(this IIndexed<TItem, int> source, Pair<TPosition> range)
			where TItem : IPositioned<TPosition>
			where TPosition : IComparable<TPosition>
		{
			return source.GetRange(source.FindIndex(range.A), source.FindIndex(range.B));
		}
		public static IEnumerable<TItem> FindRange<TItem, TPosition>(this IIndexed<TItem, int> source, TPosition startPosition, TPosition endPosition)
			where TItem : IPositioned<TPosition>
			where TPosition : IComparable<TPosition>
		{
			return source.GetRange(source.FindIndex(startPosition), source.FindIndex(endPosition));
		}
		public static int FindIndex<TItem, TPosition>(this IIndexed<TItem, int> source, TPosition position)
			where TItem : IPositioned<TPosition>
			where TPosition : IComparable<TPosition>
		{
			return GetIndex(source, position, 0, source.Count);
		}

		/// <summary>
		/// Returns the index of the first item which has a position that is greater than or equal to <paramref name="position"/>.
		/// If no such item is found, <paramref name="end"/> is returned.
		/// </summary>
		/// <param name="source">The collection to search in.</param>
		/// <param name="position">The position to search for.</param>
		/// <param name="start">The start index of the range to search.</param>
		/// <param name="end">The end index of the range to search.</param>
		/// <returns>The index of the first item which has a position that is greater than or equal to <paramref name="position"/>.</returns>
		static int GetIndex<TItem, TPosition>(IIndexed<TItem, int> source, TPosition position, int start, int end)
			where TItem : IPositioned<TPosition>
			where TPosition : IComparable<TPosition>
		{
			if (start == end) return start;

			int index = (start + end) / 2;

			if (source[index].Position.CompareTo(position) > 0) return GetIndex(source, position, start, index);
			if (source[index].Position.CompareTo(position) < 0) return GetIndex(source, position, index + 1, end);

			return index;
		}
	}
}
