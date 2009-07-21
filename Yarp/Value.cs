using System;

namespace Yarp
{
	public class Value : Packet
	{
		readonly double value;
		
		public Value(double value)
		{
			this.value = value;
		}
		
		public static implicit operator double(Value value)
		{
			return value.value;
		}
	}
}
