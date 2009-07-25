using System;
using System.Collections.Generic;
using System.Linq;

namespace Data
{
	public static class Enumeraable
	{
		public static IEnumerable<T> Separate<T>(this IEnumerable<T> source, T separator)
		{
			bool first = true;
			
			foreach (T item in source)
			{
				if (first) first = false;
				else yield return separator;
				yield return item;
			}
		}
		public static IEnumerable<T> Single<T>(this T item)
		{
			yield return item;
		}
		public static IEnumerable<T> Concat<T>(this IEnumerable<T> source, T item)
		{
			return source.Concat(item.Single());
		}
	}
}
