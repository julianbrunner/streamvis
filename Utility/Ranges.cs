using System;

namespace Utility
{
	public static class Ranges
	{
		public static bool IsEmpty<T>(this Range<T> range) where T : IComparable<T>
		{
			return range.End.CompareTo(range.Start) <= 0;
		}
	}
}
