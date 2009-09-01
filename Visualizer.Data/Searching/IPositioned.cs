using System;

namespace Visualizer.Data.Searching
{
	public interface IPositioned<TPosition>
	{
		TPosition Position { get; }
	}
}
