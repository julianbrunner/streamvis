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
	class RosStruct : RosType
	{
		readonly List<RosField> members;
		
		public RosStruct(string name, IEnumerable<string> members) : base(name)
		{
			if (members == null) throw new ArgumentNullException("members");

			Queue<string> lines = new Queue<string>(members);
			
			this.members = new List<RosField>();
			while (lines.Any()) this.members.Add(RosField.Parse(lines));
		}
		
		public override string ToString()
		{
			string result = base.ToString() + "\n" + "{" + "\n";
			
			foreach (RosField member in members) result += member.ToString() + "\n";
			
			result += "}";
			
			return result;
		}
		public override Packet ToPacket(Queue<byte> data)
		{
			List<Packet> items = new List<Packet>();

			foreach (RosField member in members) items.Add(member.ToPacket(data));

			return new List(items);
		}
		public override string GetName(Path path)
		{
			if (!path.Any()) throw new ArgumentException("Parameter 'path' cannot be empty.");

			return "." + members[path.First()].GetName(new Path(path.Skip(1)));
		}
	}
}
