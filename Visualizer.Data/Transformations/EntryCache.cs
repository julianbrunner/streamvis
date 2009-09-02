using System.Collections.Generic;
using Extensions;
using Extensions.Searching;

namespace Visualizer.Data.Transformations
{
	public class EntryCache
	{
		readonly EntryResampler source;
		readonly List<CacheFragment> fragments = new List<CacheFragment>();

		public IEnumerable<Entry> this[Time startTime, Time endTime]
		{
			get
			{
				List<Pair<Time>> requestRanges = new List<Pair<Time>>();
				requestRanges.Add(new Pair<Time>(startTime, endTime));

				List<IEnumerable<Entry>> entries = new List<IEnumerable<Entry>>();

				foreach (CacheFragment fragment in fragments)
					foreach (Pair<Time> requestRange in requestRanges.ToArray())
					{
						Pair<Time> intersection = Intersect(requestRange, fragment.Range);

						if (intersection.B - intersection.A > Time.Zero)
						{
							entries.Add(fragment.FindRange(intersection));
							requestRanges.Remove(requestRange);
							requestRanges.AddRange(Subtract(requestRange, intersection));
						}
					}

				return source[startTime, endTime];
			}
		}

		public EntryCache(EntryResampler source)
		{
			this.source = source;
		}

		static Pair<Time> Intersect(Pair<Time> a, Pair<Time> b)
		{
			return new Pair<Time>(Time.Max(a.A, b.A), Time.Min(a.B, b.B));
		}
		static IEnumerable<Pair<Time>> Subtract(Pair<Time> a, Pair<Time> b)
		{
			Pair<Time> range1 = new Pair<Time>(a.A, b.A);
			Pair<Time> range2 = new Pair<Time>(b.B, a.B);

			if (range1.B - range1.A > Time.Zero) yield return range1;
			if (range2.B - range2.A > Time.Zero) yield return range2;
		}
	}
}
