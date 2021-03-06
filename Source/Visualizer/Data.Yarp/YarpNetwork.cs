// Copyright © Julian Brunner 2009 - 2011

// This file is part of Stream Visualizer (streamvis).
// 
// Stream Visualizer is free software: you can redistribute it and/or modify it
// under the terms of the GNU General Public License as published by the Free
// Software Foundation, either version 3 of the License, or (at your option) any
// later version.
// 
// Stream Visualizer is distributed in the hope that it will be useful, but
// WITHOUT ANY WARRANTY; without even the implied warranty of MERCHANTABILITY or
// FITNESS FOR A PARTICULAR PURPOSE. See the GNU General Public License for more
// details.
// 
// You should have received a copy of the GNU General Public License along with
// Stream Visualizer. If not, see <http://www.gnu.org/licenses/>.

using System;
using System.Runtime.InteropServices;

namespace Data.Yarp
{
	public class YarpNetwork : IDisposable
	{
		readonly IntPtr network;

		bool disposed = false;

		public static bool Available
		{
			get
			{
				try { using (YarpNetwork network = new YarpNetwork()) return true; }
				catch (InvalidOperationException) { return false; }
			}
		}

		public YarpNetwork()
		{
			try { network = Network_New(); }
			catch (DllNotFoundException) { throw new InvalidOperationException("YARP could not be found."); }
		}
		~YarpNetwork()
		{
			Dispose();
		}

		public virtual void Dispose()
		{
			if (!disposed)
			{
				if (network != IntPtr.Zero) Network_Dispose(network);
				
				disposed = true;
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

		[DllImport("streamvis-wrappers-yarp")]
		static extern IntPtr Network_New();
		[DllImport("streamvis-wrappers-yarp")]
		static extern void Network_Dispose(IntPtr network);
		[DllImport("streamvis-wrappers-yarp")]
		static extern void Network_Connect(string source, string destination);
		[DllImport("streamvis-wrappers-yarp")]
		static extern void Network_Disconnect(string source, string destination);
		[DllImport("streamvis-wrappers-yarp")]
		static extern byte Network_Exists(string name);
	}
}