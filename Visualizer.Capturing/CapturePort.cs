using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using Visualizer.Data;
using Yarp;

namespace Visualizer.Capturing
{
	class CapturePort : Data.Port, IDisposable
	{
		readonly Timer timer;
		readonly BufferedPortBottle port;
		readonly System.Threading.Thread reader;

		bool disposed = false;
		bool running = true;

		CapturePort(string name, IEnumerable<Stream> streams, Timer timer)
			: base(name, streams)
		{
			this.timer = timer;

			port = new BufferedPortBottle();
			port.setStrict();
			port.open(name + "/visualization");

			Network.connect(name, name + "/visualization");

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
					using (Yarp.Port helperPort = new Yarp.Port())
					{
						helperPort.open("/portActivator");
						Network.connect("/portActivator", Name + "/visualization");
						helperPort.write(new Bottle());
						Network.disconnect("/portActivator", Name + "/visualization");
						helperPort.close();
					}

					reader.Join();
				}

				Network.disconnect(Name, Name + "/visualization");
				port.close();

				port.Dispose();
			}
		}

		void ReadLoop()
		{
			while (running)
			{
				Bottle bottle = port.read();
				long time = timer.Time;

				if (running)
					foreach (Stream stream in Streams)
						stream.Container.Add
						(
							new Entry
							(
								time,
								stream.Path.Head.Aggregate
								(
									bottle,
									(current, part) => current.get(part).asList()
								)
								.get(stream.Path.Tail).asDouble()
							)
						);
			}
		}

		public static CapturePort Create(string port, Timer timer, System.Random random)
		{
			string[] details = port.Split(':');

			string name = details[0];

			IEnumerable<Path> paths;
			switch (details.Length)
			{
				case 1:
					Console.WriteLine("Getting bottle to test size of \"" + name + "\"...");
					using (BufferedPortBottle testPort = new BufferedPortBottle())
					{
						testPort.open(name + "/test");
						Network.connect(name, name + "/test");
						paths = GetSubPaths(Enumerable.Empty<int>(), testPort.read()).ToArray();
						Network.disconnect(name, name + "/test");
						testPort.close();
					}
					break;
				case 2: paths = ParseStreams(details[1]); break;
				default: throw new InvalidOperationException("Invalid port: \"" + port + "\".");
			}
			return new CapturePort(name, from path in paths select new Stream(path, Color.FromArgb(random.Next(0x100), random.Next(0x100), random.Next(0x100))), timer);
		}

		static IEnumerable<Path> GetSubPaths(IEnumerable<int> parts, Bottle bottle)
		{
			for (int i = 0; i < bottle.size(); i++)
			{
				Value value = bottle.get(i);
				IEnumerable<int> current = parts.Concat(new int[] { i });

				if (value.isList())
					foreach (Path path in GetSubPaths(current, value.asList()))
						yield return path;
				else yield return new Path(current);
			}
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
