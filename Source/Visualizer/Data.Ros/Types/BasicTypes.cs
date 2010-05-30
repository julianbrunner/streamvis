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
using Utility.Extensions;

namespace Data.Ros.Types
{
	class RosString : RosType
	{
		public RosString() : base("string") { }

		public override Packet ToPacket(Queue<byte> data)
		{
			data.Dequeue(character => character != 0);
			data.Dequeue();

			return new InvalidPacket();
		}
		public override string GetName(Path path)
		{
			if (path.Any()) throw new ArgumentException("Parameter 'path' cannot be non-empty.");

			return string.Empty;
		}
	}
	class RosBool : RosType
	{
		public RosBool() : base("bool") { }

		public override Packet ToPacket(Queue<byte> data)
		{
			data.Dequeue();

			return new InvalidPacket();
		}
		public override string GetName(Path path)
		{
			if (path.Any()) throw new ArgumentException("Parameter 'path' cannot be non-empty.");

			return string.Empty;
		}
	}
	class RosByte : RosType
	{
		public RosByte() : base("byte") { }

		public override Packet ToPacket(Queue<byte> data)
		{
			byte value = data.Dequeue();

			return new Value(value);
		}
		public override string GetName(Path path)
		{
			if (path.Any()) throw new ArgumentException("Parameter 'path' cannot be non-empty.");

			return string.Empty;
		}
	}
	class RosChar : RosType
	{
		public RosChar() : base("char") { }

		public override Packet ToPacket(Queue<byte> data)
		{
			data.Dequeue();

			return new InvalidPacket();
		}
		public override string GetName(Path path)
		{
			if (path.Any()) throw new ArgumentException("Parameter 'path' cannot be non-empty.");

			return string.Empty;
		}
	}
	class RosFloat32 : RosType
	{
		public RosFloat32() : base("float32") { }

		public override Packet ToPacket(Queue<byte> data)
		{
			byte[] value = data.Dequeue(4).ToArray();

			return new Value(BitConverter.ToSingle(value, 0));
		}
		public override string GetName(Path path)
		{
			if (path.Any()) throw new ArgumentException("Parameter 'path' cannot be non-empty.");

			return string.Empty;
		}
	}
	class RosFloat64 : RosType
	{
		public RosFloat64() : base("float64") { }

		public override Packet ToPacket(Queue<byte> data)
		{
			byte[] value = data.Dequeue(8).ToArray();

			return new Value(BitConverter.ToDouble(value, 0));
		}
		public override string GetName(Path path)
		{
			if (path.Any()) throw new ArgumentException("Parameter 'path' cannot be non-empty.");

			return string.Empty;
		}
	}
	class RosInt8 : RosType
	{
		public RosInt8() : base("int8") { }

		public override Packet ToPacket(Queue<byte> data)
		{
			sbyte value = (sbyte)data.Dequeue();

			return new Value(value);
		}
		public override string GetName(Path path)
		{
			if (path.Any()) throw new ArgumentException("Parameter 'path' cannot be non-empty.");

			return string.Empty;
		}
	}
	class RosInt16 : RosType
	{
		public RosInt16() : base("int16") { }

		public override Packet ToPacket(Queue<byte> data)
		{
			byte[] value = data.Dequeue(2).ToArray();

			return new Value(BitConverter.ToInt16(value, 0));
		}
		public override string GetName(Path path)
		{
			if (path.Any()) throw new ArgumentException("Parameter 'path' cannot be non-empty.");

			return string.Empty;
		}
	}
	class RosInt32 : RosType
	{
		public RosInt32() : base("int32") { }

		public override Packet ToPacket(Queue<byte> data)
		{
			byte[] value = data.Dequeue(4).ToArray();

			return new Value(BitConverter.ToInt32(value, 0));
		}
		public override string GetName(Path path)
		{
			if (path.Any()) throw new ArgumentException("Parameter 'path' cannot be non-empty.");

			return string.Empty;
		}
	}
	class RosInt64 : RosType
	{
		public RosInt64() : base("int64") { }

		public override Packet ToPacket(Queue<byte> data)
		{
			byte[] value = data.Dequeue(8).ToArray();

			return new Value(BitConverter.ToInt64(value, 0));
		}
		public override string GetName(Path path)
		{
			if (path.Any()) throw new ArgumentException("Parameter 'path' cannot be non-empty.");

			return string.Empty;
		}
	}
	class RosUInt8 : RosType
	{
		public RosUInt8() : base("uint8") { }

		public override Packet ToPacket(Queue<byte> data)
		{
			byte value = data.Dequeue();

			return new Value(value);
		}
		public override string GetName(Path path)
		{
			if (path.Any()) throw new ArgumentException("Parameter 'path' cannot be non-empty.");

			return string.Empty;
		}
	}
	class RosUInt16 : RosType
	{
		public RosUInt16() : base("uint16") { }

		public override Packet ToPacket(Queue<byte> data)
		{
			byte[] value = data.Dequeue(2).ToArray();

			return new Value(BitConverter.ToUInt16(value, 0));
		}
		public override string GetName(Path path)
		{
			if (path.Any()) throw new ArgumentException("Parameter 'path' cannot be non-empty.");

			return string.Empty;
		}
	}
	class RosUInt32 : RosType
	{
		public RosUInt32() : base("uint32") { }

		public override Packet ToPacket(Queue<byte> data)
		{
			byte[] value = data.Dequeue(4).ToArray();

			return new Value(BitConverter.ToUInt32(value, 0));
		}
		public override string GetName(Path path)
		{
			if (path.Any()) throw new ArgumentException("Parameter 'path' cannot be non-empty.");

			return string.Empty;
		}
	}
	class RosUInt64 : RosType
	{
		public RosUInt64() : base("uint64") { }

		public override Packet ToPacket(Queue<byte> data)
		{
			byte[] value = data.Dequeue(8).ToArray();

			return new Value(BitConverter.ToUInt64(value, 0));
		}
		public override string GetName(Path path)
		{
			if (path.Any()) throw new ArgumentException("Parameter 'path' cannot be non-empty.");

			return string.Empty;
		}
	}
}
