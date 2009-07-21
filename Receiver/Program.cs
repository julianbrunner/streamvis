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
			Wrapper.Network_Initialize();
			
//			Network.init();
//
//			using (BufferedPortBottle port = new BufferedPortBottle())
//			{
//				port.open("/test");
//
//				Network.connect("/write", "/test");
//
//				while (!Console.KeyAvailable)
//				{
//					Bottle bottle = port.read();
//					for (int i = 0; i < 38; i++)
//					{
//						bottle.get(i).asDouble();
//					}
//				}
//
//				port.close();
//			}
//
//			Network.fini();
		}
	}
}
