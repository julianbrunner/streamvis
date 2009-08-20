using System;
using System.Collections.Generic;

namespace Data.Streams
{
	public class ResampleProjection : IStream
	{
		readonly IStream source;
		readonly int resolution;
		
		public IEnumerable<Entry> this[Time start, Time end] { get { return null; } }
		
		public ResampleProjection(IStream source, int resolution)
		{
			this.source = source;
			this.resolution = resolution;
		}
	}
}
