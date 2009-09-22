// Copyright � Julian Brunner 2009

// This file is part of Stream Visualizer (streamvis).
// 
// Stream Visualizer is free software: you can redistribute it and/or modify
// it under the terms of the GNU General Public License as published by
// the Free Software Foundation, either version 3 of the License, or
// (at your option) any later version.
// 
// Stream Visualizer is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU General Public License for more details.
// 
// You should have received a copy of the GNU General Public License
// along with Stream Visualizer.  If not, see <http://www.gnu.org/licenses/>.

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