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
	class RosType
	{
		readonly string type;
		readonly string name;
		readonly IEnumerable<RosType> members;
		
		RosType(string type, string name, IEnumerable<RosType> members)
		{
			this.type = type;
			this.name = name;
			this.members = members;
		}
		
		public override string ToString()
		{
			if (members.Any()) 
			{
				string result = string.Empty;
				
				result += string.Format("<{0} type=\"{1}\" members=\"{2}\">\n", name, type, members.Count());
				foreach (RosType member in members) result += member.ToString();
				result += string.Format("</{0}>\n", name);
				
				return result;
			}
			else return string.Format("<{0} type=\"{1}\" />\n", name, type);	
		}
		
		public static RosType Parse(string declaration, string definition)
		{	
			return Parse(declaration, definition.Split('\n'));
		}

		static RosType Parse(string declaration, IEnumerable<string> lines)
		{
			string[] declarationDetails = declaration.Split(' ');
			
			return new RosType(declarationDetails[0], declarationDetails[1], ParseMembers(lines).ToArray());
		}
		static IEnumerable<RosType> ParseMembers(IEnumerable<string> lines)
		{
			while (lines.Any())
			{
				string memberDeclaration = lines.First();
				lines = lines.Skip(1);

				IEnumerable<string> memberLines = lines.TakeWhile(line => line.Length >= 2 && line.Substring(0, 2) == "  ").Select(line => line.Substring(2));
				lines = lines.Skip(memberLines.Count());
				
				yield return Parse(memberDeclaration, memberLines);
			}
		}
	}
}