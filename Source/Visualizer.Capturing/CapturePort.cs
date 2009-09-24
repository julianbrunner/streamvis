// Copyright © Julian Brunner 2009

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
using Utility;
using Visualizer.Data;
using Yarp;

namespace Visualizer.Capturing
{
	class CapturePort : Data.Port, IDisposable
	{
		readonly Network network;
		readonly Timer timer;
		readonly Yarp.Port port;
		readonly System.Threading.Thread reader;

		bool disposed = false;
		bool running = true;

		CapturePort(string name, IEnumerable<Stream> streams, Network network, Timer timer)
			: base(name, streams)
		{
			this.network = network;
			this.timer = timer;

			port = new Yarp.Port(network.FindName(name + "/visualization"));

			network.Connect(name, port.Name);

			reader = new System.Threading.Thread(ReadLoop);
			reader.Priority = System.Threading.ThreadPriority.AboveNormal;
			reader.Start();
		}
		~CapturePort()
		{
			Dispose();
		}

		public void Dispose()
		{
			if (!disposed)
			{
				disposed = true;

				running = false;

				Console.WriteLine("Trying to join reader thread of \"" + Name + "\"...");

				if (!reader.Join(1000))
				{
					Console.WriteLine("Sending a packet to \"" + Name + "\" in order to join reader thread...");
					using (Yarp.Port helperPort = new Yarp.Port(network.FindName(Name + "/activator")))
					{
						network.Connect(helperPort.Name, port.Name);
						helperPort.Write(new List());
						network.Disconnect(helperPort.Name, port.Name);
					}

					reader.Join();
				}

				network.Disconnect(Name, port.Name);

				port.Dispose();
			}
		}

		void ReadLoop()
		{
			while (running)
			{
				Packet packet = port.Read();
				Time time = timer.Time;

				if (running)
					foreach (Stream stream in Streams)
						stream.EntryData.Add(new Entry(time, packet.Get(stream.Path)));
			}
		}

		public static CapturePort Create(string portString, Network network, Timer timer)
		{
			string[] details = portString.Split(':');

			string name = details[0];

			IEnumerable<Path> paths;
			switch (details.Length)
			{
				case 1:
					Console.WriteLine("Getting packet to test size of \"" + name + "\"...");
					using (Yarp.Port testPort = new Yarp.Port(network.FindName(name + "/tester")))
					{
						network.Connect(name, testPort.Name);
						paths = GetPaths(Enumerable.Empty<int>(), testPort.Read()).ToArray();
						network.Disconnect(name, testPort.Name);
					}
					break;
				case 2: paths = ParseStreams(details[1]); break;
				default: throw new InvalidOperationException("Invalid port: \"" + portString + "\".");
			}
			return new CapturePort(name, from path in paths select new Stream(path), network, timer);
		}

		static IEnumerable<Path> GetPaths(IEnumerable<int> path, Packet packet)
		{
			if (packet is List)
			{
				int i = 0;
				foreach (Packet subPacket in (List)packet)
					foreach (Path subPath in GetPaths(path.Concat(i++.Single()), subPacket))
						yield return subPath;
			}
			if (packet is Value) yield return new Path(path);
		}
		static IEnumerable<Path> ParseStreams(string streams)
		{
			foreach (string range in streams.Split(','))
			{
				string[] delimiters = range.Split('-');
				switch (delimiters.Length)
				{
					case 1:
						Path path;
						try { path = new Path(delimiters[0]); }
						catch (ArgumentOutOfRangeException) { throw new InvalidOperationException("Invalid path: \"" + delimiters[0] + "\"."); }
						yield return path;
						break;
					case 2:
						Path start;
						Path end;
						try { start = new Path(delimiters[0]); }
						catch (ArgumentOutOfRangeException) { throw new InvalidOperationException("Invalid path: \"" + delimiters[0] + "\"."); }
						try { end = new Path(delimiters[1]); }
						catch (ArgumentOutOfRangeException) { throw new InvalidOperationException("Invalid path: \"" + delimiters[1] + "\"."); }
						foreach (Path currentPath in Path.Range(start, end)) yield return currentPath;
						break;
					default: throw new InvalidOperationException("Invalid range: \"" + range + "\".");
				}
			}
		}
	}
}
