using System;
using System.Collections;
using System.Collections.Generic;
using Extensions;
using Data.Searching;

namespace Data
{
	public class Buffer<TItem, TPosition> : IIndexed<TItem, int>, IRanged<TItem, TPosition>
		where TItem : IPositioned<TPosition>
		where TPosition : IComparable<TPosition>
	{
		readonly List<TItem> items = new List<TItem>();
		
		public TItem this[int index] { get { return items[index]; } }
		public IEnumerable<TItem> this[TPosition start, TPosition end]
		{
			get
			{
				int startIndex = this.GetIndex(start);
				int endIndex = this.GetIndex(start);
				
				TItem[] buffer = new TItem[endIndex - startIndex];
				
				items.CopyTo(startIndex, buffer, 0, endIndex - startIndex);
				
				return buffer;
			}
		}

		public int Count { get { return items.Count; } }

		public void Add(TItem item)
		{
			// TODO: Check for ordering violation
			items.Add(item);
		}
		public IEnumerator<TItem> GetEnumerator()
		{
			return items.GetEnumerator();
		}
		
		IEnumerator IEnumerable.GetEnumerator()
		{
			return GetEnumerator();
		}
	}
}
