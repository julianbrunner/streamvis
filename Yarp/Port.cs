using System;
using System.Linq;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace Yarp
{
	public class Port : IDisposable
	{
		readonly string name;
		readonly IntPtr port;
		
		bool disposed = false;
		
		public string Name { get { return name; } }
		
		public Port(string name)
		{
			this.name = name;
			
			port = BufferedPort_Bottle_New();
			BufferedPort_Bottle_Open(port, this.name);
		}
		~Port()
		{
			Dispose();
		}
		
		public void Dispose()
		{
			if (!disposed)
			{
				disposed = true;
				
				BufferedPort_Bottle_Close(port);
				BufferedPort_Bottle_Dispose(port);
			}
		}
		public Packet Read()
		{
			return ParseBottle(BufferedPort_Bottle_Read(port));
		}
		public void Write(Packet packet)
		{
			IntPtr bottle = BufferedPort_Bottle_Prepare(port);
			Bottle_Clear(bottle);
			
			IEnumerable<Packet> packets = Enumerable.Empty<Packet>();
			if (packet is List) packets = (List)packet;
			if (packet is Value) packets = new[] { packet };
			
			WritePackets(bottle, packets);
			
			BufferedPort_Bottle_Write(port);
		}
		
		static Packet ParseBottle(IntPtr bottle)
		{
			int size = Bottle_Size(bottle);
			Packet[] packets = new Packet[size];
			
			for (int i = 0; i < size; i++) packets[i] = ParseValue(Bottle_GetValue(bottle, i));
			
			return new List(packets);
		}
		static Packet ParseValue(IntPtr value)
		{
			if (Value_IsList(value)) return ParseBottle(Value_AsList(value));
			if (Value_IsDouble(value)) return new Value(Value_AsDouble(value));
		
			return new Packet();
		}
		static void WritePackets(IntPtr bottle, IEnumerable<Packet> packets)
		{
			foreach (Packet packet in packets)
			{
				if (packet is List)
				{
					IntPtr subBottle = Bottle_AddList(bottle);
					WritePackets(subBottle, (List)packet);
				}
				if (packet is Value) Bottle_AddDouble(bottle, (Value)packet);
			}
		}
		
		[DllImport("Yarp.Wrapper")]
		static extern IntPtr BufferedPort_Bottle_New();
		[DllImport("Yarp.Wrapper")]
		static extern void BufferedPort_Bottle_Dispose(IntPtr port);
		[DllImport("Yarp.Wrapper")]
		static extern void BufferedPort_Bottle_Open(IntPtr port, string name);
		[DllImport("Yarp.Wrapper")]
		static extern void BufferedPort_Bottle_Close(IntPtr port);
		[DllImport("Yarp.Wrapper")]
		static extern IntPtr BufferedPort_Bottle_Prepare(IntPtr port);
		[DllImport("Yarp.Wrapper")]
		static extern IntPtr BufferedPort_Bottle_Read(IntPtr port);
		[DllImport("Yarp.Wrapper")]
		static extern void BufferedPort_Bottle_Write(IntPtr port);
		
		[DllImport("Yarp.Wrapper")]
		static extern void Bottle_Clear(IntPtr bottle);
		[DllImport("Yarp.Wrapper")]
		static extern int Bottle_Size(IntPtr bottle);
		[DllImport("Yarp.Wrapper")]
		static extern IntPtr Bottle_GetValue(IntPtr bottle, int index);
		[DllImport("Yarp.Wrapper")]
		static extern IntPtr Bottle_AddList(IntPtr bottle);
		[DllImport("Yarp.Wrapper")]
		static extern void Bottle_AddDouble(IntPtr bottle, double value);
		
		[DllImport("Yarp.Wrapper")]
		static extern bool Value_IsList(IntPtr value);
		[DllImport("Yarp.Wrapper")]
		static extern bool Value_IsDouble(IntPtr value);
		[DllImport("Yarp.Wrapper")]
		static extern IntPtr Value_AsList(IntPtr value);
		[DllImport("Yarp.Wrapper")]
		static extern double Value_AsDouble(IntPtr value);
	}
}
