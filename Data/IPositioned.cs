using System;

namespace Data
{
	public interface IPositioned<TPosition>
	{
		TPosition Position { get; }
	}
}
