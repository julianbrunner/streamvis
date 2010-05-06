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
using Data;
using Data.Text;
using Data.Yarp;
using Data.Ros;
using Visualizer.Data;

namespace Visualizer
{
	class Session : IDisposable
	{
		readonly Capture capture;
		readonly IEnumerable<Receiver> receivers;
		readonly YarpNetwork yarpNetwork;
		readonly RosNode rosNode;

		public Capture Capture { get { return capture; } }

		bool disposed = false;

		public Session(Timer timer, IEnumerable<string> portStrings)
		{
			List<string> textPortStrings = new List<string>();
			List<string> yarpPortStrings = new List<string>();
			List<string> rosPortStrings = new List<string>();

			foreach (string portString in portStrings)
			{
				if (portString.Length < 2) throw new InvalidOperationException("\"" + portString + "\" is not a valid port string");

				switch (portString.Substring(0, 2))
				{
					case "t:": textPortStrings.Add(portString.Substring(2)); break;
					case "y:": yarpPortStrings.Add(portString.Substring(2)); break;
					case "r:": rosPortStrings.Add(portString.Substring(2)); break;
					default: throw new InvalidOperationException("\"" + portString + "\" is not a valid port string");
				}
			}

			if (yarpPortStrings.Any()) this.yarpNetwork = new YarpNetwork();
			if (rosPortStrings.Any()) this.rosNode = new RosNode();

			List<Receiver> receivers = new List<Receiver>();
			foreach (string portString in textPortStrings)
			{
				string name = portString.Split(':').First();
				Port port = name == "-" ? new TextReaderPort() : new TextReaderPort(name);
				receivers.Add(new Receiver(port, timer, portString));
			}
			foreach (string portString in yarpPortStrings)
			{
				string name = portString.Split(':').First();
				Port port = new ConnectedYarpPort(name, yarpNetwork);
				receivers.Add(new Receiver(port, timer, portString));
			}
			foreach (string portString in rosPortStrings)
			{
				string name = portString.Split(':').First();
				Port port = new RosPort(name, rosNode);
				receivers.Add(new Receiver(port, timer, portString));
			}
			this.receivers = receivers;

			if (receivers.Count(receiver => receiver.HasTimer) > 1) throw new ArgumentException("More than one timer stream was found.");

			this.capture = new Capture
			(
				from receiver in receivers
				select new PortData(receiver.PortName, receiver.PortStreams)
			);
		}
		public Session(Capture capture)
		{
			this.capture = capture;
		}
		~Session()
		{
			Dispose();
		}

		public virtual void Dispose()
		{
			if (!disposed)
			{
				if (receivers != null)
					foreach (Receiver receiver in receivers)
						receiver.Dispose();

				if (yarpNetwork != null) yarpNetwork.Dispose();

				disposed = true;
			}
		}
	}
}
