using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using Visualizer.Data;
using Data;
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

		CapturePort(string name, Network network, IEnumerable<Stream> streams, Timer timer)
			: base(name, streams)
		{
			this.network = network;
			this.timer = timer;

			port = new Yarp.Port(network.FindName(name + "/visualization"));

			network.Connect(name, port.Name);

			reader = new System.Threading.Thread(ReadLoop);
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
					Console.WriteLine("Sending a bottle to \"" + Name + "\" in order to join reader thread...");
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
				long time = timer.Time;

				if (running)
					foreach (Stream stream in Streams)
						stream.Container.Add(new Entry(time, packet.Get(stream.Path)));
			}
		}

		public static CapturePort Create(string port, Network network, Timer timer, System.Random random)
		{
			string[] details = port.Split(':');

			string name = details[0];

			IEnumerable<Path> paths;
			switch (details.Length)
			{
				case 1:
					Console.WriteLine("Getting bottle to test size of \"" + name + "\"...");
					using (Yarp.Port testPort = new Yarp.Port(network.FindName(name + "/tester")))
					{
						network.Connect(name, testPort.Name);
						paths = GetPaths(Enumerable.Empty<int>(), testPort.Read()).ToArray();
						network.Disconnect(name, testPort.Name);
					}
					break;
				case 2: paths = ParseStreams(details[1]); break;
				default: throw new InvalidOperationException("Invalid port: \"" + port + "\".");
			}
			return new CapturePort(name, network, from path in paths select new Stream(path, Color.FromArgb(random.Next(0x100), random.Next(0x100), random.Next(0x100))), timer);
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
