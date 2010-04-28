using System;
using System.Collections.Generic;

namespace Data.Ros
{
	class RosInt32 : RosBasicType<Int32>
	{
		public override Int32 Convert(IEnumerable<byte> source)
		{
			return 0;
		}
	}
}
