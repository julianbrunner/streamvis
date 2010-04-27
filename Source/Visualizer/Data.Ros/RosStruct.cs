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

namespace Data.Ros
{
	class RosStruct
	{
		readonly string name;
		readonly IEnumerable<RosStruct> members;
		
		RosStruct(string name, IEnumerable<string> lines)
		{
			this.name = name;
			this.members = ParseMembers(lines).ToArray();
		}
		RosStruct(string name)
		{
			this.name = name;
		}
		public RosStruct(string name, string definition) : this(name, definition.Split('\n')) { }
		
		public override string ToString()
		{
			if (members == null) return "<" + name + "/>" + "\n";
			else
			{
				string result = string.Empty;
				
				result += "<" + name + ">" + "\n";
				
				foreach (RosStruct member in members)
					result += member.ToString();
				
				result += "</" + name + ">" + "\n";
				
				return result;
			}
		}

		
		static IEnumerable<RosStruct> ParseMembers(IEnumerable<string> lines)
		{
			while (lines.Any())
			{
				string memberName = lines.First();
				lines = lines.Skip(1);

				IEnumerable<string> memberLines = lines.TakeWhile(line => line.Length >= 2 && line.Substring(0, 2) == "  ").Select(line => line.Substring(2));
				lines = lines.Skip(memberLines.Count());
				
				yield return memberLines.Any() ? new RosStruct(memberName, memberLines) : new RosStruct(memberName);
			}
		}
	}
}