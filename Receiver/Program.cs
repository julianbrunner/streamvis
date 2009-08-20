using System;
using System.Collections.Generic;
using System.Text;
using Yarp;
using Extensions;

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
							if (d == 0) d = 1;
						}
					}
				}
		}
	}
}
