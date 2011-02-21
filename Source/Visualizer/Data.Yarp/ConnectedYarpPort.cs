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

namespace Data.Yarp
{
	public class ConnectedYarpPort : YarpPort
	{
		readonly string target;
		readonly YarpNetwork network;

		bool disposed = false;

		public ConnectedYarpPort(string target, YarpNetwork network)
			: base(network.FindName(target + "/client"), network)
		{
			this.target = target;
			this.network = network;

			network.Connect(target, Name);
			network.Connect(Name, target);
		}
		~ConnectedYarpPort()
		{
			Dispose();
		}

		public override void Dispose()
		{
			if (!disposed)
			{
				disposed = true;

				network.Disconnect(target, Name);
				network.Disconnect(Name, target);
			}

			base.Dispose();
		}
	}
}
