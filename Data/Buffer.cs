using System;
using System.Collections.Generic;
using Extensions;

namespace Data
{
	public class Buffer : IBinarySearchable
	{
		readonly List<Entry> entries = new List<Entry>();
		
		public Entry this[int index] { get { return entries[index]; } }
		
		public int Count { get { return entries.Count; } }
		
		public void Add(Entry entry)
		{
			entries.Add(entry);
		}
	}
}
