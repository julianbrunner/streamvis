using System.Collections.Generic;

namespace Data
{
	public abstract class Transformation<TItem, TPosition>
	{
		public abstract IEnumerable<TItem> this[TPosition start, TPosition end] { get; }
	}
}
