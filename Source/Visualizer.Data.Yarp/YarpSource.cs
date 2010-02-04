// Copyright © Julian Brunner 2009 - 2010

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
using System.Collections.Generic;
using System.Linq;
using Yarp;

namespace Visualizer.Data.Yarp
{
	public class YarpSource : Source, IDisposable
	{
		readonly Network network;

		bool disposed = false;

		YarpSource(IEnumerable<Data.Port> ports, Network network)
			: base(ports)
		{
			this.network = network;
		}
		~YarpSource()
		{
			Dispose();
		}

		public static Source Create(IEnumerable<string> portStrings, Timer timer)
		{
			Network network = null;
			List<Data.Port> ports = new List<Data.Port>();

			bool finished = false;

			try
			{
				network = new Network();

				foreach (string portString in portStrings) ports.Add(YarpPort.Create(portString, network, timer));

				finished = true;
			}
			finally
			{
				if (!finished)
				{
					foreach (IDisposable port in ports.OfType<IDisposable>()) port.Dispose();

					if (network != null) network.Dispose();
				}
			}

			return new YarpSource(ports, network);
		}

		public void Dispose()
		{
			if (!disposed)
			{
				disposed = true;

				foreach (IDisposable port in Ports.OfType<IDisposable>()) port.Dispose();
				network.Dispose();
			}
		}
	}
}
