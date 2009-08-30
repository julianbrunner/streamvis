using System;

namespace Data.Searching
{
	public interface IPositioned<TPosition>
	{
		TPosition Position { get; }
	}
}
