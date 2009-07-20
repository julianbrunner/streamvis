using System;
using System.Runtime.InteropServices;

namespace Yarp
{	
	static class Wrapper
	{
		[DllImport("Yarp.Wrapper")]
		public static extern void Network_Init();
		[DllImport("Yarp.Wrapper")]
		public static extern void Network_Fini();
	}
}
