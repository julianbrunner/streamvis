using System;
using Extensions;

namespace Data
{
	public class BinarySearcher<TSource, TItem, TPosition>
		where TSource : IIndexed<TItem, int>, ICounted
		where TItem : IPositioned<TPosition>
		where TPosition : IComparable<TPosition>
	{
		readonly TSource source;
		
		public BinarySearcher(TSource source)
		{
			this.source = source;
		}
		
		public int GetIndex(TPosition position)
		{
			return GetIndex(0, source.Count, position);
		}

		/// <summary>
		/// Returns the index of the first item which has a position that is greater than or equal to <paramref name="position"/>.
		/// If no such item is found, <paramref name="end"/> is returned.
		/// </summary>
		/// <param name="start">The start index of the range to search.</param>
		/// <param name="end">The end index of the range to search.</param>
		/// <param name="position">The position to search for.</param>
		/// <returns>The index of the first item which has a position that is greater than or equal to <paramref name="position"/>.</returns>
		int GetIndex(int start, int end, TPosition position)
		{
			if (start == end) return start;

			int index = (start + end) / 2;

			if (source[index].Position.CompareTo(position) > 0) return GetIndex(start, index, position);
			if (source[index].Position.CompareTo(position) < 0) return GetIndex(index + 1, end, position);

			return index;
		}
	}
}
