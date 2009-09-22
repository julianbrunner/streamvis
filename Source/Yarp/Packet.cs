using System;

namespace Yarp
{
	public class Packet
	{
		public static Packet Empty { get { return new Packet(); } }
			
		protected Packet() { }
	}
}
