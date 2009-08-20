using System;
using System.Collections.Generic;

namespace Data.Streams
{
	public interface IStream
	{
		IEnumerable<Entry> this[Time start, Time end] { get; }
	}
}
