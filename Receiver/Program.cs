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
						List list = (List)port.Read();
						for (int i = 0; i < 38; i++)
						{
							double d = (Value)list[i];
							if (d == 0) d = 1;
						}
					}
				}
		}
	}
}
