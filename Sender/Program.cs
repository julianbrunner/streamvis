using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Yarp;
using System.Diagnostics;

namespace Sender
{
	class Program
	{
		static void Main(string[] args)
		{
//			Network.init();
//
//			int streams = int.Parse(args[0]);
//			int frequency = int.Parse(args[1]);
//			int ms = 1000 / frequency;
//
//			System.Random random = new System.Random();
//
//			Stopwatch stopwatch = new Stopwatch();
//			stopwatch.Reset();
//			stopwatch.Start();
//			int packets = 0;
//
//			using (BufferedPortBottle port = new BufferedPortBottle())
//			{
//				port.open("/write");
//
//				while (!Console.KeyAvailable)
//				{
//					if (stopwatch.Elapsed.TotalSeconds > 1)
//					{
//						Console.WriteLine("Packets/s: " + ((double)packets / stopwatch.Elapsed.TotalSeconds));
//
//						stopwatch.Reset();
//						stopwatch.Start();
//						packets = 0;
//					}
//
//					Bottle bottle = port.prepare();
//					bottle.clear();
//					for (int i = 0; i < streams; i++)
//					{
//						bottle.addDouble(random.NextDouble());
//					}
//					port.write();
//
//					packets++;
//
//					System.Threading.Thread.Sleep(ms);
//				}
//
//				port.close();
//			}
//
//			Network.fini();
		}
	}
}