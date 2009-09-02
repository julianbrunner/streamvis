using System;
using System.Collections;
using System.Collections.Generic;
using Extensions;
using Visualizer.Data.Searching;

namespace Visualizer.Data
{
	// TODO: There is duplicate code between this and EntryBuffer
	public class CacheFragment : IIndexed<Entry, int>
	{
		readonly List<Entry> entries;
		readonly Pair<Time> range;
		
		public Pair<Time> Range { get { return range; } }

		public Entry this[int index] { get { return entries[index]; } }
		public IEnumerable<Entry> this[Pair<Time> time]
		{
			get
			{
				int startIndex = this.GetIndex(time.A);
				int endIndex = this.GetIndex(time.B);
				
				Entry[] buffer = new Entry[endIndex - startIndex];
				
				entries.CopyTo(startIndex, buffer, 0, endIndex - startIndex);
				
				return buffer;
			}
		}
		
		public int Count { get { return entries.Count; } }
		
		public CacheFragment(IEnumerable<Entry> entries, Pair<Time> range)
		{
			this.entries = new List<Entry>(entries);
			this.range = range;
		}

		public IEnumerator<Entry> GetEnumerator ()
		{
			return entries.GetEnumerator();
		}

		IEnumerator IEnumerable.GetEnumerator ()
		{
			return GetEnumerator();
		}
	}
}
