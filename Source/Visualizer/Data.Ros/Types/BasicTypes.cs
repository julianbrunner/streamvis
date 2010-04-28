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

namespace Data.Ros.Types
{
	class RosString : RosBasicType<String>
	{
		public RosString() : base("string") { }
	}
	class RosBool : RosBasicType<Boolean>
	{
		public RosBool() : base("bool") { }
	}
	class RosByte : RosBasicType<Byte>
	{
		public RosByte() : base("byte") { }
	}
	class RosChar : RosBasicType<Char>
	{
		public RosChar() : base("char") { }
	}
	class RosFloat32 : RosBasicType<Single>
	{
		public RosFloat32() : base("float32") { }
	}
	class RosFloat64 : RosBasicType<Double>
	{
		public RosFloat64() : base("float64") { }
	}
	class RosInt8 : RosBasicType<SByte>
	{
		public RosInt8() : base("int8") { }
	}
	class RosInt16 : RosBasicType<Int16>
	{
		public RosInt16() : base("int16") { }
	}
	class RosInt32 : RosBasicType<Int32>
	{
		public RosInt32() : base("int32") { }
	}
	class RosInt64 : RosBasicType<Int64>
	{
		public RosInt64() : base("int64") { }
	}
	class RosUInt8 : RosBasicType<Byte>
	{
		public RosUInt8() : base("uint8") { }
	}
	class RosUInt16 : RosBasicType<UInt16>
	{
		public RosUInt16() : base("uint16") { }
	}
	class RosUInt32 : RosBasicType<UInt32>
	{
		public RosUInt32() : base("uint32") { }
	}
	class RosUInt64 : RosBasicType<UInt64>
	{
		public RosUInt64() : base("uint64") { }
	}
}
