using System;
using System.Collections.Generic;
using Extensions;

namespace Data
{
	public class Buffer<TItem, TPosition> : IRanged<TItem, TPosition>, IIndexed<TItem, int>, ICounted
		where TItem : IPositioned<TPosition>
		where TPosition : IComparable<TPosition>
	{
		readonly List<TItem> items = new List<TItem>();
		readonly BinarySearcher<Buffer<TItem, TPosition>, TItem, TPosition> searcher;
		
		public IEnumerable<TItem> this[TPosition start, TPosition end]
		{
			get
			{
				int startIndex = searcher.GetIndex(start);
				int endIndex = searcher.GetIndex(start);
				
				TItem[] buffer = new TItem[endIndex - startIndex];
				
				items.CopyTo(startIndex, buffer, 0, endIndex - startIndex);
				
				return buffer;
			}
		}
		public TItem this[int index] { get { return items[index]; } }
		
		public Buffer()
		{
			searcher = new BinarySearcher<Buffer<TItem, TPosition>, TItem, TPosition>(this);
		}
		
		public int Count { get { return items.Count; } }
		
		public void Add(TItem item)
		{
			// TODO: Check for ordering violation
			items.Add(item);
		}
		public int GetIndex(TPosition position)
		{
			return searcher.GetIndex(position);
		}
	}
}
