using System;

namespace Extensions.Searching
{
	public interface IPositioned<TPosition>
	{
		TPosition Position { get; }
	}
}
