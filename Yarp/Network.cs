using System;
using System.Runtime.InteropServices;

namespace Yarp
{
	public class Network : IDisposable
	{
		readonly IntPtr network;
		
		bool disposed = false;
		
		public Network()
		{
			network = Network_New();
		}
		~Network()
		{
			Dispose();
		}
		
		public void Dispose()
		{
			if (!disposed)
			{
				disposed = true;
				
				Network_Dispose(network);
			}
		}
		public void Connect(string source, string destination)
		{
			Network_Connect(source, destination);
		}
		public void Disconnect(string source, string destination)
		{
			Network_Disconnect(source, destination);
		}
		
		[DllImport("Yarp.Wrapper")]
		static extern IntPtr Network_New();
		[DllImport("Yarp.Wrapper")]
		static extern void Network_Dispose(IntPtr network);
		[DllImport("Yarp.Wrapper")]
		static extern void Network_Connect(string source, string destination);
		[DllImport("Yarp.Wrapper")]
		static extern void Network_Disconnect(string source, string destination);
	}
}