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
using Utility;
using Yarp;

namespace Receiver
{
	static class Program
	{
		static void Main(string[] args)
		{
			using (Network network = new Network())
			using (Port port = new Port("/test"))
			{
				network.Connect("/write", port.Name);

				while (!Console.KeyAvailable)
				{
					Packet packet = port.Read();
					for (int i = 0; i < 38; i++)
					{
						double d = packet.Get(i.Single());
						if (d == 0) Console.WriteLine("Awesome");
					}
				}
			}
		}
	}
}
