using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Yarp;

namespace Receiver
{
	class Program
	{
		static void Main(string[] args)
		{	
			using (Network network = new Network())
				using (Port port = new Port("/test"))
				{	
					network.Connect("/write", port.Name);
	
					while (!Console.KeyAvailable)
					{
						Bottle bottle = port.Read();
						for (int i = 0; i < 38; i++)
						{
							double d = bottle[i];
							if (d == 0) d = 1;
						}
					}
				}
		}
	}
}
