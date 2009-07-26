using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Yarp;
using System.Diagnostics;
using System.Threading;

namespace Sender
{
	static class Program
	{
		static void Main(string[] args)
		{
			using (Network network = new Network())
			{
				int streams = int.Parse(args[0]);
				int frequency = int.Parse(args[1]);
				int ms = 1000 / frequency;
	
				Random random = new Random();
	
				Stopwatch stopwatch = new Stopwatch();
				stopwatch.Reset();
				stopwatch.Start();
				int packets = 0;
	
				using (Port port = new Port("/write"))
					while (!Console.KeyAvailable)
					{
						if (stopwatch.Elapsed.TotalSeconds > 1)
						{
							Console.WriteLine("Packets/s: " + ((double)packets / stopwatch.Elapsed.TotalSeconds));
	
							stopwatch.Reset();
							stopwatch.Start();
							packets = 0;
						}
	
						List<Packet> values = new List<Packet>();
						for (int i = 0; i < streams; i++) values.Add(new Value(random.NextDouble()));
						
						port.Write(new List(values));
	
						packets++;
	
						Thread.Sleep(ms);
					}
			}
		}
	}
}