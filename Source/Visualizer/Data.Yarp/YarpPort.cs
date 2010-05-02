// Copyright © Julian Brunner 2009 - 2010

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
using System.Linq;
using System.Runtime.InteropServices;

namespace Data.Yarp
{
	public class YarpPort : Port, IDisposable
	{
		readonly YarpNetwork network;
		readonly IntPtr port;

		bool disposed = false;

		public YarpPort(string name, YarpNetwork network)
			: base(name)
		{
			if (network == null) throw new ArgumentNullException("network");
			
			this.network = network;
			this.port = BufferedPort_Bottle_New();

			BufferedPort_Bottle_Open(port, Name);
		}
		~YarpPort()
		{
			Dispose();
		}

		public virtual void Dispose()
		{
			if (!disposed)
			{
				BufferedPort_Bottle_Close(port);
				BufferedPort_Bottle_Dispose(port);
				
				disposed = true;
			}
		}
		public override Packet Read()
		{
			return new List
			(
				from value in GetValues(BufferedPort_Bottle_Read(port))
				select ValueToPacket(value)
			);
		}
		public override void Write(Packet packet)
		{
			if (packet is Value) throw new ArgumentException("Cannot directly write a value to a YARP port.");
			if (packet is List)
			{
				IntPtr bottle = BufferedPort_Bottle_Prepare(port);
				Bottle_Clear(bottle);

				foreach (Packet subPacket in (List)packet) PacketToValue(bottle, subPacket);
	
				BufferedPort_Bottle_Write(port);
			}
		}
		public override void AbortWait()
		{
			using (YarpPort activator = new ConnectedYarpPort(Name, network)) activator.Write(new List());
		}

		static Packet ValueToPacket(IntPtr value)
		{
			if (Value_IsList(value) > 0)
				return new List
				(
					from subValue in GetValues(Value_AsList(value))
					select ValueToPacket(subValue)
				);
			if (Value_IsInt(value) > 0) return new Value(Value_AsInt(value));
			if (Value_IsDouble(value) > 0) return new Value(Value_AsDouble(value));

			throw new ArgumentException("value");
		}
		static void PacketToValue(IntPtr bottle, Packet packet)
		{
			if (packet is List)
			{
				IntPtr subBottle = Bottle_AddList(bottle);
				foreach (Packet subPacket in (List)packet) PacketToValue(subBottle, subPacket);
			}
			if (packet is Value) Bottle_AddDouble(bottle, (Value)packet);
		}
		static IEnumerable<IntPtr> GetValues(IntPtr bottle)
		{
			int length = Bottle_Size(bottle);

			for (int i = 0; i < length; i++) yield return Bottle_GetValue(bottle, i);
		}

		[DllImport("streamvis-wrappers-yarp")]
		static extern IntPtr BufferedPort_Bottle_New();
		[DllImport("streamvis-wrappers-yarp")]
		static extern void BufferedPort_Bottle_Dispose(IntPtr port);
		[DllImport("streamvis-wrappers-yarp")]
		static extern void BufferedPort_Bottle_Open(IntPtr port, string name);
		[DllImport("streamvis-wrappers-yarp")]
		static extern void BufferedPort_Bottle_Close(IntPtr port);
		[DllImport("streamvis-wrappers-yarp")]
		static extern IntPtr BufferedPort_Bottle_Prepare(IntPtr port);
		[DllImport("streamvis-wrappers-yarp")]
		static extern IntPtr BufferedPort_Bottle_Read(IntPtr port);
		[DllImport("streamvis-wrappers-yarp")]
		static extern void BufferedPort_Bottle_Write(IntPtr port);

		[DllImport("streamvis-wrappers-yarp")]
		static extern void Bottle_Clear(IntPtr bottle);
		[DllImport("streamvis-wrappers-yarp")]
		static extern int Bottle_Size(IntPtr bottle);
		[DllImport("streamvis-wrappers-yarp")]
		static extern IntPtr Bottle_GetValue(IntPtr bottle, int index);
		[DllImport("streamvis-wrappers-yarp")]
		static extern IntPtr Bottle_AddList(IntPtr bottle);
		[DllImport("streamvis-wrappers-yarp")]
		static extern void Bottle_AddDouble(IntPtr bottle, double value);

		[DllImport("streamvis-wrappers-yarp")]
		static extern byte Value_IsList(IntPtr value);
		[DllImport("streamvis-wrappers-yarp")]
		static extern byte Value_IsInt(IntPtr value);
		[DllImport("streamvis-wrappers-yarp")]
		static extern byte Value_IsDouble(IntPtr value);
		[DllImport("streamvis-wrappers-yarp")]
		static extern IntPtr Value_AsList(IntPtr value);
		[DllImport("streamvis-wrappers-yarp")]
		static extern int Value_AsInt(IntPtr value);
		[DllImport("streamvis-wrappers-yarp")]
		static extern double Value_AsDouble(IntPtr value);
	}
}
