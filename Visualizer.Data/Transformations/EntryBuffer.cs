using System;
using System.Collections;
using System.Collections.Generic;
using Extensions;
using Visualizer.Data.Searching;

namespace Visualizer.Data.Transformations
{
	public class EntryBuffer : IIndexed<Entry, int>
	{
		readonly List<Entry> items;
		
		public Entry this[int index] { get { return items[index]; } }
		public IEnumerable<Entry> this[Time start, Time end]
		{
			get
			{
				int startIndex = this.GetIndex(start);
				int endIndex = this.GetIndex(start);
				
				Entry[] buffer = new Entry[endIndex - startIndex];
				
				items.CopyTo(startIndex, buffer, 0, endIndex - startIndex);
				
				return buffer;
			}
		}

		public int Count { get { return items.Count; } }
		
		public EntryBuffer()
		{
			this.items = new List<Entry>();
		}
		public EntryBuffer(IEnumerable<Entry> items)
		{
			this.items = new List<Entry>(items);
		}

		public void Add(Entry item)
		{
			// TODO: Check for ordering violation?
			items.Add(item);
		}
		public void Clear()
		{
			items.Clear();
		}
		public IEnumerator<Entry> GetEnumerator()
		{
			return items.GetEnumerator();
		}
		
		IEnumerator IEnumerable.GetEnumerator()
		{
			return GetEnumerator();
		}
	}
}
