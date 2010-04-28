using System;
using System.Collections.Generic;

namespace Data.Ros
{
	abstract class RosBasicType<TValue>
	{
		public abstract TValue Convert(IEnumerable<byte> source);
	}
}
