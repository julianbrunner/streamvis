using System.Collections.Generic;

namespace Data
{
	public interface IRanged<TItem, TPosition>
	{
		IEnumerable<TItem> this[TPosition start, TPosition end] { get; }
	}
}
