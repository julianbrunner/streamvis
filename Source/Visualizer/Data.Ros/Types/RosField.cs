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
using System.Linq;
using System.Collections.Generic;

namespace Data.Ros.Types
{
	class RosField
	{
		readonly RosType type;
		readonly string name;
		
		RosField(RosType type, string name)
		{
			if (type == null) throw new ArgumentNullException("type");
			if (name == null) throw new ArgumentNullException("name");
			
			this.type = type;
			this.name = name;
		}

		public override string ToString()
		{
			return name + " : " + type;
		}
		public Packet Parse(IEnumerable<byte> data)
		{
			return type.ToPacket(data);
		}

		// TODO: Implement a Stream<T> class which supports FIFO, or use Queue<T>.
		public static RosField Parse(string field)
		{
			IEnumerable<string> lines = field.Split('\n');
			
			return Parse(ref lines);
		}
		public static RosField Parse(ref IEnumerable<string> lines)
		{
			string[] declarationDetails = lines.First().Split(' ');
			lines = lines.Skip(1);
			
			string typeName = declarationDetails[0];
			bool isArray = typeName.EndsWith("[]");
			if (isArray) typeName = typeName.Substring(0, typeName.Length - 2);
			string fieldName = declarationDetails[1];
			
			IEnumerable<string> members = lines.TakeWhile(line => line.Length >= 2 && line.Substring(0, 2) == "  ").Select(line => line.Substring(2));
			lines = lines.Skip(members.Count());
			
			RosType type =
				members.Any() || !RosType.BasicTypes.Any(basicType => basicType.Name == typeName)
				?
				new RosStruct(typeName, members)
				:
				RosType.BasicTypes.Single(basicType => basicType.Name == typeName);
			
			if (isArray) type = new RosArray(type);
			
			return new RosField(type, fieldName);
		}
	}
}