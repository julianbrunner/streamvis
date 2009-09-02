using System.Collections.Generic;

namespace Extensions
{
	public class IndexedList<T> : List<T>, IIndexed<T, int>
	{
		public IndexedList() : base() { }
		public IndexedList(IEnumerable<T> source) : base(source) { }
		public IndexedList(int capacity) : base(capacity) { }
	}
}
