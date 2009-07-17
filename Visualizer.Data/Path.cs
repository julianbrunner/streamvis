using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;

namespace Visualizer.Data
{
	public struct Path
	{
		readonly IEnumerable<int> parts;
		readonly IEnumerable<int> head;
		readonly int tail;

		public static string XElementName { get { return "path"; } }

		public XElement XElement { get { return new XElement(XElementName, ToString()); } }
		public IEnumerable<int> Head { get { return head; } }
		public int Tail { get { return tail; } }

		public Path(XElement path)
		{
			this.parts = (from part in ((string)path).Split('.') select int.Parse(part)).ToArray();
			this.head = this.parts.Take(this.parts.Count() - 1).ToArray();
			this.tail = this.parts.Last();
		}
		public Path(string parts)
		{
			try { this.parts = (from part in parts.Split('.') select int.Parse(part)).ToArray(); }
			catch (FormatException) { throw new ArgumentOutOfRangeException("parts"); }
			this.head = this.parts.Take(this.parts.Count() - 1).ToArray();
			this.tail = this.parts.Last();
		}
		public Path(IEnumerable<int> parts)
		{
			if (!parts.Any()) throw new ArgumentOutOfRangeException("parts");

			this.parts = parts.ToArray();
			this.head = this.parts.Take(this.parts.Count() - 1).ToArray();
			this.tail = this.parts.Last();
		}

		public override string ToString()
		{
			return parts.Aggregate(string.Empty, (pathString, current) => pathString + (pathString == string.Empty ? string.Empty : ".") + current);
		}

		public static IEnumerable<Path> Range(Path start, Path end)
		{
			if (!Enumerable.SequenceEqual(start.Head, end.Head))
				throw new InvalidOperationException("Heads of range delimiter paths do not match (" + start + ", " + end + ").");

			for (int i = start.Tail; i <= end.Tail; i++) yield return new Path(start.Head.Concat(new int[] { i }));
		}
	}
}
