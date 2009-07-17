using System;
using System.Collections.Generic;
using System.Linq;
using Visualizer.Data;
using Yarp;

namespace Visualizer.Capturing
{
	public class Capture : Source, IDisposable
	{
		bool disposed = false;

		public Capture(IEnumerable<Data.Port> ports) : base(ports) { }
		~Capture()
		{
			Dispose();
		}

		public static Source Create(IEnumerable<string> portStrings, Timer timer, System.Random random)
		{
			Network.init();

			List<CapturePort> ports = new List<CapturePort>();

			try
			{
				foreach (string portString in portStrings) ports.Add(CapturePort.Create(portString, timer, random));
			}
			catch (Exception)
			{
				foreach (CapturePort port in ports) port.Dispose();
				throw;
			}

			return new Capture(ports.Cast<Data.Port>());
		}

		public void Dispose()
		{
			if (!disposed)
			{
				disposed = true;

				foreach (IDisposable port in Ports.OfType<IDisposable>()) port.Dispose();

				Network.fini();
			}
		}
	}
}
