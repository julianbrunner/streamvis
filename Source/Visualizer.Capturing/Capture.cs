using System;
using System.Collections.Generic;
using System.Linq;
using Visualizer.Data;
using Yarp;

namespace Visualizer.Capturing
{
	public class Capture : Source, IDisposable
	{
		readonly Network network;

		bool disposed = false;

		Capture(IEnumerable<Data.Port> ports, Network network)
			: base(ports)
		{
			this.network = network;
		}
		~Capture()
		{
			Dispose();
		}

		public static Source Create(IEnumerable<string> portStrings, Timer timer)
		{
			// TODO: Can this be done in a nicer way?

			Network network = new Network();
			List<Data.Port> ports = new List<Data.Port>();

			bool finished = false;

			try
			{
				foreach (string portString in portStrings) ports.Add(CapturePort.Create(portString, network, timer));

				finished = true;
			}
			finally
			{
				if (!finished)
				{
					foreach (IDisposable port in ports.OfType<IDisposable>()) port.Dispose();
					network.Dispose();
				}
			}

			return new Capture(ports, network);
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
