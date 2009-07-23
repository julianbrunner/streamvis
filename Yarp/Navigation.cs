using System;
using System.Collections.Generic;
using System.Linq;

namespace Yarp
{
	public static class Navigation
	{
		public static double Get(this Packet packet, IEnumerable<int> path)
		{
			return (Value)path.Aggregate(packet, (seed, index) => ((List)seed)[index]);
		}
	}
}
