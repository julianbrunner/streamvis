using System;
using System.Collections.Generic;
using System.Linq;

namespace Extensions
{
	public static class EnumerablePlus
	{
		public static IEnumerable<T> Separate<T>(this IEnumerable<T> source, T separator)
		{
			if (source == null) throw new ArgumentNullException("source");
			
			bool first = true;
			
			foreach (T item in source)
			{
				if (first) first = false;
				else yield return separator;
				yield return item;
			}
		}
		public static IEnumerable<T> Concat<T>(this IEnumerable<T> source, T item)
		{
			if (source == null) throw new ArgumentNullException("source");
			
			return source.Concat(item.Single());
		}
		public static IEnumerable<Range<T>> GetRanges<T>(this IEnumerable<T> source)
		{
			if (source == null) throw new ArgumentNullException("source");
			
			IEnumerator<T> enumerator = source.GetEnumerator();
			
			if (!enumerator.MoveNext()) yield break;
			
			T start = enumerator.Current;
			T end = default(T);
			
			while (enumerator.MoveNext())
			{
				end = enumerator.Current;
				yield return new Range<T>(start, end);
				start = enumerator.Current;
			}
		}
		public static IEnumerable<string> ToStrings<T>(this IEnumerable<T> source)
		{
			if (source == null) throw new ArgumentNullException("source");
			
			foreach (T item in source) yield return item.ToString();
		}
		public static string AggregateString<T>(this IEnumerable<T> source)
		{
			if (source == null) throw new ArgumentNullException("source");
			
			return source.Aggregate(string.Empty, (seed, current) => seed + current);
		}
		public static IEnumerable<T> Construct<T>(params IEnumerable<T>[] sources)
		{
			if (sources == null) throw new ArgumentNullException("sources");
			
			foreach (IEnumerable<T> source in sources)
			{
				if (source == null) throw new ArgumentException("sources");
				
				foreach (T item in source)
					yield return item;
			}
		}
		public static IEnumerable<T> Single<T>(this T item)
		{
			yield return item;
		}
	}
}
