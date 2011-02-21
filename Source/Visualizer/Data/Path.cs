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
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using Krach.Extensions;

namespace Data
{
	public class Path : IEnumerable<int>
	{
		readonly IEnumerable<int> nodes;

		public static string XElementName { get { return "Path"; } }
		public static Path Empty { get { return new Path(); } }

		public Path Head { get { return new Path(nodes.SkipLast(1)); } }
		public int Tail { get { return nodes.Last(); } }
		public bool IsEmpty { get { return !nodes.Any(); } }
		public XElement XElement { get { return new XElement(XElementName, this); } }

		public Path()
		{
			this.nodes = Enumerable.Empty<int>();
		}
		public Path(IEnumerable<int> nodes)
		{
			if (nodes == null) throw new ArgumentNullException("nodes");

			this.nodes = nodes.ToArray();
		}
		public Path(string pathString) : this(Parse(pathString)) { }
		public Path(XElement path) : this((string)path) { }

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
			if (start == null) throw new ArgumentNullException("start");
			if (end == null) throw new ArgumentNullException("end");

			Path head = Items.Equal(start.Head, end.Head, Enumerable.SequenceEqual);

			for (int i = start.Tail; i <= end.Tail; i++) yield return new Path(head.Append(i));
		}

		static IEnumerable<int> Parse(string pathString)
		{
			if (pathString == null) throw new ArgumentNullException("pathString");

			try
			{
				return
				(
					from node in pathString.Split('.')
					select int.Parse(node)
				)
				.ToArray();
			}
			catch (FormatException)
			{
				throw new ArgumentException(string.Format("Parameter 'pathString' ({0}) is not a valid path string.", pathString));
			}
		}
	}
}
