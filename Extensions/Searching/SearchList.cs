using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Extensions.Searching
{
	public class SearchList<TValue, TKey> : IEnumerable<TValue>
		where TValue : IKeyed<TKey>
	{
		readonly IComparer<TKey> comparer;
		readonly List<TValue> items;

		public TValue this[int index]
		{
			get
			{
				if (index < 0 || index >= items.Count) throw new ArgumentOutOfRangeException("index");
				
				return items[index];
			}
		}
		public TValue this[TKey key]
		{
			get
			{
				int index = FindIndex(key);
				
				if (index < 0 || index >= items.Count) throw new KeyNotFoundException();
				
				return items[index];
			}
		}
		public IEnumerable<TValue> this[int startIndex, int endIndex]
		{
			get
			{
				if (startIndex < 0 || startIndex > items.Count) throw new ArgumentOutOfRangeException("startIndex");
				if (endIndex < 0 || endIndex > items.Count) throw new ArgumentOutOfRangeException("endIndex");
				if (endIndex - startIndex < 0) throw new ArgumentException();

				for (int i = startIndex; i < endIndex; i++) yield return items[i];
			}
		}
		public IEnumerable<TValue> this[TKey startKey, TKey endKey]
		{
			get
			{
				int startIndex = FindIndex(startKey);
				int endIndex = FindIndex(endKey);
				
				if (startIndex == endIndex) return Enumerable.Empty<TValue>();
				
				return this[startIndex, endIndex];
			}
		}
		public IEnumerable<TValue> this[Range<TKey> range] { get { return this[range.Start, range.End]; } }

		public int Count { get { return items.Count; } }

		public SearchList()
		{
			this.items = new List<TValue>();
			this.comparer = Comparer<TKey>.Default;
		}
		public SearchList(int capacity)
		{
			if (capacity < 0) throw new ArgumentOutOfRangeException("capacity");
			
			this.items = new List<TValue>(capacity);
			this.comparer = Comparer<TKey>.Default;
		}
		public SearchList(IEnumerable<TValue> items)
		{
			if (items == null) throw new ArgumentNullException("items");
			
			if (!AreOrdered(items, Comparer<TKey>.Default)) throw new ArgumentException("items");

			this.items = new List<TValue>(items);
			this.comparer = Comparer<TKey>.Default;
		}
		public SearchList(IComparer<TKey> comparer)
		{
			if (comparer == null) throw new ArgumentNullException("comparer");
			
			this.items = new List<TValue>();
			this.comparer = comparer;
		}
		public SearchList(int capacity, IComparer<TKey> comparer)
		{
			if (capacity < 0) throw new ArgumentOutOfRangeException("capacity");
			if (comparer == null) throw new ArgumentNullException("comparer");
			
			this.items = new List<TValue>(capacity);
			this.comparer = comparer;
		}
		public SearchList(IEnumerable<TValue> items, IComparer<TKey> comparer)
		{
			if (items == null) throw new ArgumentNullException("items");
			if (comparer == null) throw new ArgumentNullException("comparer");
			
			if (!AreOrdered(items, comparer)) throw new ArgumentException("items");

			this.items = new List<TValue>(items);
			this.comparer = comparer;
		}

		public void Clear()
		{
			items.Clear();
		}
		public void Append(TValue item)
		{
			if (items.Count > 0 && comparer.Compare(item.Key, items[items.Count - 1].Key) < 0) throw new ArgumentException("item");

			items.Add(item);
		}
		public void Append(IEnumerable<TValue> items)
		{
			foreach (TValue item in items) Append(item);
		}
		public void Insert(TValue item)
		{
			items.Insert(FindIndex(item.Key), item);
		}
		public void Insert(IEnumerable<TValue> items)
		{
			foreach (TValue item in items) Insert(item);
		}
		public void Remove(TValue item)
		{
			items.Remove(item);
		}
		public void Remove(IEnumerable<TValue> items)
		{
			foreach (TValue item in items) Remove(item);
		}
		public int FindIndex(TKey key)
		{
			return FindIndex(key, 0, items.Count);
		}
		public IEnumerator<TValue> GetEnumerator()
		{
			return items.GetEnumerator();
		}

		IEnumerator IEnumerable.GetEnumerator()
		{
			return GetEnumerator();
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

			if (comparer.Compare(items[index].Key, key) > 0) return FindIndex(key, startIndex, index);
			if (comparer.Compare(items[index].Key, key) < 0) return FindIndex(key, index + 1, endIndex);

			return index;
		}

		static bool AreOrdered(IEnumerable<TValue> items, IComparer<TKey> comparer)
		{
			if (items == null) throw new ArgumentNullException("items");
			if (comparer == null) throw new ArgumentNullException("comparer");
			
			if (!items.Any()) return true;

			TKey lastKey = items.First().Key;

			foreach (TValue item in items.Skip(1))
				if (comparer.Compare(lastKey, item.Key) > 0)
					return false;

			return true;
		}
	}
}