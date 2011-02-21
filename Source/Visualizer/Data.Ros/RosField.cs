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
using Krach.Extensions;
using Data.Ros.Types;

namespace Data.Ros
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
		public Packet BinaryToPacket(IEnumerable<byte> data)
		{
			return BinaryToPacket(new Queue<byte>(data));
		}
		public Packet BinaryToPacket(Queue<byte> data)
		{
			return type.BinaryToPacket(data);
		}
		public string GetName(Path path)
		{
			return path.Any() ? name + "." + type.GetName(path) : name;
		}

		public static RosField Parse(string field)
		{
			return Parse(new Queue<string>(field.Split('\n')));
		}
		public static RosField Parse(Queue<string> lines)
		{
			string[] declarationDetails = lines.Dequeue().Split(' ');
			IEnumerable<string> members = lines.Dequeue(line => line.Length >= 2 && line.Substring(0, 2) == "  ").Select(line => line.Substring(2));

			string typeName = declarationDetails[0];
			bool isArray = typeName.EndsWith("[]");
			if (isArray) typeName = typeName.Substring(0, typeName.Length - 2);
			string fieldName = declarationDetails[1];

			RosType type;
			if (members.Any() || !RosType.BasicTypes.Any(basicType => basicType.Name == typeName)) type = new RosStruct(typeName, members);
			else type = RosType.BasicTypes.Single(basicType => basicType.Name == typeName);

			if (fieldName.Contains("=")) type = new RosConstant(type);

			if (isArray) type = new RosArray(type);

			return new RosField(type, fieldName);
		}
	}
}
