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
using System.Diagnostics;
using System.Threading;
using Utility.Extensions;
using Yarp;

namespace Sender
{
	static class Program
	{
		static void Main(string[] args)
		{
			int streams = int.Parse(args[0]);
			int frequency = int.Parse(args[1]);
			int ms = 1000 / frequency;

			Random random = new Random();

			Stopwatch stopwatch = new Stopwatch();
			stopwatch.Reset();
			stopwatch.Start();
			int packets = 0;

			//double value = 0;
			//double variance = 1;
			//int counter = 0;

			using (Network network = new Network())
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

					//counter++;
					//if (counter == 15000)
					//{
					//    counter = 0;
					//    variance = Math.Pow(2, NextDouble(random, -10, 6));
					//}
					//value += variance * NextDouble(random, -1, 1);

					//List<Packet> values = new List<Packet>();
					//values.Add(new Value(value));

					List<Packet> values = new List<Packet>();
					for (int i = 0; i < streams; i++) values.Add(new Value(random.NextDouble(-1, 1)));

					port.Write(new List(values));

					packets++;

					Thread.Sleep(ms);
				}
		}
	}
}