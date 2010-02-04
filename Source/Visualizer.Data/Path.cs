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
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using Utility.Extensions;

namespace Visualizer.Data
{
	// TODO: Paths are YARP specific and should be move to Visualizer.Capturing.Yarp
	public class Path : IEnumerable<int>
	{
		readonly IEnumerable<int> nodes;

		public static string XElementName { get { return "Path"; } }

		public XElement XElement { get { return new XElement(XElementName, ToString()); } }
		public IEnumerable<int> Head { get { return nodes.Take(nodes.Count() - 1); } }
		public int Tail { get { return nodes.Last(); } }

		public Path(XElement path)
		{
			this.nodes = (from node in ((string)path).Split('.') select int.Parse(node)).ToArray();
		}
		public Path(string path)
		{
			try { this.nodes = (from node in path.Split('.') select int.Parse(node)).ToArray(); }
			catch (FormatException) { throw new ArgumentOutOfRangeException("path"); }
		}
		public Path(IEnumerable<int> nodes)
		{
			if (!nodes.Any()) throw new ArgumentOutOfRangeException("nodes");

			this.nodes = nodes.ToArray();
		}

		public override string ToString()
		{
			return nodes.ToStrings().Separate(".").AggregateString();
		}
		public IEnumerator<int> GetEnumerator()
		{
			return nodes.GetEnumerator();
		}
		IEnumerator IEnumerable.GetEnumerator()
		{
			return GetEnumerator();
		}

		public static IEnumerable<Path> Range(Path start, Path end)
		{
			if (!Enumerable.SequenceEqual(start.Head, end.Head))
				throw new InvalidOperationException("Heads of range delimiter paths do not match (" + start + ", " + end + ").");

			for (int i = start.Tail; i <= end.Tail; i++) yield return new Path(start.Head.Concat(i));
		}
	}
}
