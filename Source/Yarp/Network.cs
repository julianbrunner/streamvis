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
		public bool Exists(string name)
		{
			return Network_Exists(name) > 0;
		}
		public string FindName(string prefix)
		{
			int i = 0;
			while (Exists(prefix + "/" + i)) i++;
			
			return prefix + "/" + i;
		}
		
		[DllImport("Yarp.Wrapper")]
		static extern IntPtr Network_New();
		[DllImport("Yarp.Wrapper")]
		static extern void Network_Dispose(IntPtr network);
		[DllImport("Yarp.Wrapper")]
		static extern void Network_Connect(string source, string destination);
		[DllImport("Yarp.Wrapper")]
		static extern void Network_Disconnect(string source, string destination);
		[DllImport("Yarp.Wrapper")]
		static extern byte Network_Exists(string name);
	}
}