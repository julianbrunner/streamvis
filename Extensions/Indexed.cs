using System.Collections.Generic;

namespace Extensions
{
	public static class Indexed
	{
		public static IEnumerable<T> Range<T>(this IIndexed<T, int> source, int startIndex, int endIndex)
		{
			for (int i = startIndex; i < endIndex; i++) yield return source[i];
		}
	}
}
