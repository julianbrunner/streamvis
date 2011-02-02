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

namespace Data.Ros.Types
{
	abstract class RosType
	{
		readonly string name;
		
		public string Name { get { return name; } }
		
		public static IEnumerable<RosType> BasicTypes
		{
			get
			{
				yield return new RosString();
				yield return new RosTime();
				yield return new RosBool();
				yield return new RosByte();
				yield return new RosChar();
				yield return new RosFloat32(); 
				yield return new RosFloat64();
				yield return new RosInt8();
				yield return new RosInt16();
				yield return new RosInt32();
				yield return new RosInt64();
				yield return new RosUInt8();
				yield return new RosUInt16();
				yield return new RosUInt32();
				yield return new RosUInt64();
			}
		}

		protected RosType(string name)
		{
			if (name == null) throw new ArgumentNullException("name");
			
			this.name = name;
		}

		public override string ToString()
		{
			return name;
		}
		public abstract Packet ToPacket(Queue<byte> data);
		public abstract string GetName(Path path);
	}
}
