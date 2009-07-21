using System;
using System.Runtime.InteropServices;

namespace Yarp
{	
	public static class Wrapper
	{
		[DllImport("Yarp.Wrapper")]
		public static extern void Network_Initialize();
		[DllImport("Yarp.Wrapper")]
		public static extern void Network_Dispose();
	}
}
