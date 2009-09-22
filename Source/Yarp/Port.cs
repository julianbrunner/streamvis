using System;
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
		public List Read()
		{
			return ParseBottle(BufferedPort_Bottle_Read(port));
		}
		public void Write(List list)
		{
			IntPtr bottle = BufferedPort_Bottle_Prepare(port);
			Bottle_Clear(bottle);

			foreach (Packet packet in list) WritePacket(bottle, packet);

			BufferedPort_Bottle_Write(port);
		}

		static List ParseBottle(IntPtr bottle)
		{
			Packet[] packets = new Packet[Bottle_Size(bottle)];

			for (int i = 0; i < packets.Length; i++) packets[i] = ParseValue(Bottle_GetValue(bottle, i));

			return new List(packets);
		}
		static Packet ParseValue(IntPtr value)
		{
			if (Value_IsList(value) > 0) return ParseBottle(Value_AsList(value));
			if (Value_IsDouble(value) > 0) return new Value(Value_AsDouble(value));

			return Packet.Empty;
		}
		static void WritePacket(IntPtr bottle, Packet packet)
		{
			if (packet is List)
			{
				IntPtr subBottle = Bottle_AddList(bottle);
				foreach (Packet subPacket in (List)packet) WritePacket(subBottle, subPacket);
			}
			if (packet is Value) Bottle_AddDouble(bottle, (Value)packet);
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
		static extern byte Value_IsList(IntPtr value);
		[DllImport("Yarp.Wrapper")]
		static extern byte Value_IsDouble(IntPtr value);
		[DllImport("Yarp.Wrapper")]
		static extern IntPtr Value_AsList(IntPtr value);
		[DllImport("Yarp.Wrapper")]
		static extern double Value_AsDouble(IntPtr value);
	}
}
