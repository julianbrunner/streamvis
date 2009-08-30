using System.Collections.Generic;

namespace Extensions
{
	public interface IIndexed<TItem, TIndex> : IEnumerable<TItem>
	{
		TItem this[TIndex index] { get; }
		TIndex Count { get; }
	}
}
