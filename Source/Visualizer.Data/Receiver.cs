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
using System.Threading;
using Data;
using Utility.Extensions;
using Utility.Utilities;

namespace Visualizer.Data
{
	public class Receiver : IDisposable
	{
		readonly Port source;
		readonly Timer timer;
		readonly string portName;
		readonly Stream timeStream;
		readonly IEnumerable<Stream> portStreams;
		readonly Thread reader;

		bool disposed = false;
		bool running = true;

		public string PortName { get { return portName; } }
		public IEnumerable<Stream> PortStreams { get { return portStreams; } }
		public bool HasTimer { get { return timeStream != null && timeStream.Name == "TIMER"; } }

		public Receiver(Port source, Timer timer, string portString)
		{
			this.source = source;
			this.timer = timer;

			string[] details = portString.Split(':');

			this.portName = details[0];

			IEnumerable<Stream> streams = GetStreams(source, details);
			IEnumerable<Stream> timeStreams = from stream in streams
											  where stream.Name == "TIME" || stream.Name == "TIMER"
											  select stream;

			if (timeStreams.Count() > 1) throw new ArgumentException("More than one timestamp stream was found.");

			this.timeStream = timeStreams.SingleOrDefault();
			this.portStreams = streams.Except(timeStreams).ToArray();

			this.reader = new Thread(Read);
			this.reader.Priority = ThreadPriority.AboveNormal;
			this.reader.Start();
		}
		~Receiver()
		{
			Dispose();
		}

		public virtual void Dispose()
		{
			if (!disposed)
			{
				running = false;

				if (!reader.Join(TimeSpan.FromSeconds(1.0)))
				{
					source.AbortWait();
					reader.Join();
				}

				if (source is IDisposable) ((IDisposable)source).Dispose();

				disposed = true;
			}
		}

		void Read()
		{
			while (running)
			{
				Packet packet = source.Read();
				double time = timer.Time;

				if (!running) break;

				if (packet != null)
				{
					if (timeStream != null) time = packet.GetValue(timeStream.Path);

					foreach (Stream stream in portStreams)
						try { stream.EntryData.Add(new Entry(time, packet.GetValue(stream.Path))); }
						catch (ArgumentException) { Console.WriteLine("Packet \"{0}\" does not have a value at path \"{1}\".", packet, stream.Path); }
				}
			}
		}

		static IEnumerable<Stream> GetStreams(Port port, string[] portString)
		{
			switch (portString.Length)
			{
				case 1:
					return
					(
						from path in GetPaths(Enumerable.Empty<int>(), EnumerableUtility.Consume<Packet>(port.Read).First(packet => packet != null))
						select new Stream(path)
					)
					.ToArray();
				case 2:
					return
					(
						from range in portString[1].Split(',')
						from stream in ParseRange(range)
						select stream
					)
					.ToArray();
				default: throw new ArgumentException("portString");
			}
		}
		static IEnumerable<Stream> ParseRange(string range)
		{
			string[] details = range.Split('-');

			switch (details.Length)
			{
				case 1: return EnumerableUtility.Single(ParseStream(details[0]));
				case 2: return from path in Path.Range(new Path(details[0]), new Path(details[1]))
							   select new Stream(path);
				default: throw new ArgumentException("range");
			}
		}
		static Stream ParseStream(string streamString)
		{
			string[] details = streamString.Split('=');

			switch (details.Length)
			{
				case 1: return new Stream(new Path(details[0]));
				case 2: return new Stream(new Path(details[0]), details[1]);
				default: throw new ArgumentException("streamString");
			}
		}
		static IEnumerable<Path> GetPaths(IEnumerable<int> path, Packet packet)
		{
			if (packet is List)
			{
				int i = 0;

				foreach (Packet subPacket in (List)packet)
					foreach (Path subPath in GetPaths(path.Concat(i++), subPacket))
						yield return subPath;
			}
			if (packet is Value) yield return new Path(path);
		}
	}
}