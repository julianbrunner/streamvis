using System;

namespace Extensions.Searching
{
	public interface IKeyed<TKey>
	{
		TKey Key { get; }
	}
}
