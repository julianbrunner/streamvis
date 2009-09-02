using System;
using System.Collections.Generic;

namespace Extensions
{
	public static class Indexed
	{
		public static IEnumerable<T> GetRange<T>(this IIndexed<T, int> source, int startIndex, int endIndex)
		{
			if (source == null) throw new ArgumentNullException("source");
			if (startIndex < 0 || startIndex >= source.Count) throw new ArgumentOutOfRangeException("startIndex");
			if (endIndex < 0 || endIndex >= source.Count) throw new ArgumentOutOfRangeException("endIndex");
			
			for (int i = startIndex; i < endIndex; i++) yield return source[i];
		}
	}
}
