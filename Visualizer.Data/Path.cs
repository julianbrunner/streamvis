using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using Data;

namespace Visualizer.Data
{
	public struct Path : IEnumerable<int>
	{
		readonly IEnumerable<int> nodes;

		public static string XElementName { get { return "path"; } }

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
			return (from node in nodes select node.ToString()).Separate(".").Aggregate((path, current) => path + current);
		}		
		public IEnumerator<int> GetEnumerator ()
		{
			return nodes.GetEnumerator();
		}
		IEnumerator IEnumerable.GetEnumerator ()
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
