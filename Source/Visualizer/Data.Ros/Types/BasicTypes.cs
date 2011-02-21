// Copyright © Julian Brunner 2009 - 2011

// This file is part of Stream Visualizer (streamvis).
// 
// Stream Visualizer is free software: you can redistribute it and/or modify it
// under the terms of the GNU General Public License as published by the Free
// Software Foundation, either version 3 of the License, or (at your option) any
// later version.
// 
// Stream Visualizer is distributed in the hope that it will be useful, but
// WITHOUT ANY WARRANTY; without even the implied warranty of MERCHANTABILITY or
// FITNESS FOR A PARTICULAR PURPOSE. See the GNU General Public License for more
// details.
// 
// You should have received a copy of the GNU General Public License along with
// Stream Visualizer. If not, see <http://www.gnu.org/licenses/>.

using System;
using System.Collections.Generic;
using System.Linq;
using Krach.Extensions;

namespace Data.Ros.Types
{
	class RosString : RosBasicType
	{
		public RosString() : base("string") { }

		public override Packet BinaryToPacket(Queue<byte> data)
		{
			uint length = BitConverter.ToUInt32(data.Dequeue(4).ToArray(), 0);

			data.Dequeue((int)length);

			return new InvalidPacket();
		}
	}
	class RosTime : RosBasicType
	{
		public RosTime() : base("time") { }

		public override Packet BinaryToPacket(Queue<byte> data)
		{
			uint seconds = BitConverter.ToUInt32(data.Dequeue(4).ToArray(), 0);
			uint nanoseconds = BitConverter.ToUInt32(data.Dequeue(4).ToArray(), 0);

			return new Value(1.0 * seconds + 0.000000001 * nanoseconds);
		}
	}
	class RosDuration : RosBasicType
	{
		public RosDuration() : base("duration") { }

		public override Packet BinaryToPacket(Queue<byte> data)
		{
			uint seconds = BitConverter.ToUInt32(data.Dequeue(4).ToArray(), 0);
			uint nanoseconds = BitConverter.ToUInt32(data.Dequeue(4).ToArray(), 0);

			return new Value(1.0 * seconds + 0.000000001 * nanoseconds);
		}
	}
	class RosBool : RosBasicType
	{
		public RosBool() : base("bool") { }

		public override Packet BinaryToPacket(Queue<byte> data)
		{
			data.Dequeue();

			return new InvalidPacket();
		}
	}
	class RosByte : RosBasicType
	{
		public RosByte() : base("byte") { }

		public override Packet BinaryToPacket(Queue<byte> data)
		{
			return new Value(data.Dequeue());
		}
	}
	class RosChar : RosBasicType
	{
		public RosChar() : base("char") { }

		public override Packet BinaryToPacket(Queue<byte> data)
		{
			data.Dequeue();

			return new InvalidPacket();
		}
	}
	class RosFloat32 : RosBasicType
	{
		public RosFloat32() : base("float32") { }

		public override Packet BinaryToPacket(Queue<byte> data)
		{
			return new Value(BitConverter.ToSingle(data.Dequeue(4).ToArray(), 0));
		}
	}
	class RosFloat64 : RosBasicType
	{
		public RosFloat64() : base("float64") { }

		public override Packet BinaryToPacket(Queue<byte> data)
		{
			return new Value(BitConverter.ToDouble(data.Dequeue(8).ToArray(), 0));
		}
	}
	class RosInt8 : RosBasicType
	{
		public RosInt8() : base("int8") { }

		public override Packet BinaryToPacket(Queue<byte> data)
		{
			return new Value((sbyte)data.Dequeue());
		}
	}
	class RosInt16 : RosBasicType
	{
		public RosInt16() : base("int16") { }

		public override Packet BinaryToPacket(Queue<byte> data)
		{
			return new Value(BitConverter.ToInt16(data.Dequeue(2).ToArray(), 0));
		}
	}
	class RosInt32 : RosBasicType
	{
		public RosInt32() : base("int32") { }

		public override Packet BinaryToPacket(Queue<byte> data)
		{
			return new Value(BitConverter.ToInt32(data.Dequeue(4).ToArray(), 0));
		}
	}
	class RosInt64 : RosBasicType
	{
		public RosInt64() : base("int64") { }

		public override Packet BinaryToPacket(Queue<byte> data)
		{
			return new Value(BitConverter.ToInt64(data.Dequeue(8).ToArray(), 0));
		}
	}
	class RosUInt8 : RosBasicType
	{
		public RosUInt8() : base("uint8") { }

		public override Packet BinaryToPacket(Queue<byte> data)
		{
			return new Value(data.Dequeue());
		}
	}
	class RosUInt16 : RosBasicType
	{
		public RosUInt16() : base("uint16") { }

		public override Packet BinaryToPacket(Queue<byte> data)
		{
			return new Value(BitConverter.ToUInt16(data.Dequeue(2).ToArray(), 0));
		}
	}
	class RosUInt32 : RosBasicType
	{
		public RosUInt32() : base("uint32") { }

		public override Packet BinaryToPacket(Queue<byte> data)
		{
			return new Value(BitConverter.ToUInt32(data.Dequeue(4).ToArray(), 0));
		}
	}
	class RosUInt64 : RosBasicType
	{
		public RosUInt64() : base("uint64") { }

		public override Packet BinaryToPacket(Queue<byte> data)
		{
			return new Value(BitConverter.ToUInt64(data.Dequeue(8).ToArray(), 0));
		}
	}
}
