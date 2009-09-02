using System;
using System.Collections.Generic;

namespace Extensions.Searching
{
	public class SearchList<TValue, TKey> : List<TValue>
		where TValue : IKeyed<TKey>
	{
		readonly IComparer<TKey> comparer;

		public TValue this[TKey key] { get { return this[FindIndex(key)]; } }
		public IEnumerable<TValue> this[int startIndex, int endIndex]
		{
			get
			{
				if (startIndex < 0 || startIndex >= Count) throw new ArgumentOutOfRangeException("startIndex");
				if (endIndex < 0 || endIndex >= Count) throw new ArgumentOutOfRangeException("endIndex");

				for (int i = startIndex; i < endIndex; i++) yield return this[i];
			}
		}
		public IEnumerable<TValue> this[TKey startKey, TKey endKey] { get { return this[FindIndex(startKey), FindIndex(endKey)]; } }
		public IEnumerable<TValue> this[Range<TKey> range] { get { return this[range.Start, range.End]; } }

		public SearchList()
			: base()
		{
			this.comparer = Comparer<TKey>.Default;
		}
		public SearchList(int capacity)
			: base(capacity)
		{
			this.comparer = Comparer<TKey>.Default;
		}
		public SearchList(IEnumerable<TValue> items)
			: base(items)
		{
			this.comparer = Comparer<TKey>.Default;
		}
		public SearchList(IComparer<TKey> comparer)
			: base()
		{
			this.comparer = comparer;
		}
		public SearchList(int capacity, IComparer<TKey> comparer)
			: base(capacity)
		{
			this.comparer = comparer;
		}
		public SearchList(IEnumerable<TValue> items, IComparer<TKey> comparer)
			: base(items)
		{
			this.comparer = comparer;
		}

		public int FindIndex(TKey key)
		{
			return FindIndex(key, 0, Count);
		}

		/// <summary>
		/// Returns the index of the first item which has a key that is greater than or equal to <paramref name="key"/>.
		/// If no such item is found, <paramref name="endIndex"/> is returned.
		/// </summary>
		/// <param name="key">The key to search for.</param>
		/// <param name="startIndex">The start index of the range to search.</param>
		/// <param name="endIndex">The end index of the range to search.</param>
		/// <returns>The index of the first item which has a key that is greater than or equal to <paramref name="key"/>.</returns>
		int FindIndex(TKey key, int startIndex, int endIndex)
		{
			if (startIndex == endIndex) return startIndex;

			int index = (startIndex + endIndex) / 2;

			if (comparer.Compare(this[index].Key, key) > 0) return FindIndex(key, startIndex, index);
			if (comparer.Compare(this[index].Key, key) < 0) return FindIndex(key, index + 1, endIndex);

			return index;
		}
	}
}
