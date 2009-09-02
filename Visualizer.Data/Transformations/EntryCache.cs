using System.Collections.Generic;
using Extensions;

namespace Visualizer.Data.Transformations
{
	public class EntryCache
	{
		readonly EntryResampler source;
		readonly IndexedList<Entry> entries = new IndexedList<Entry>();
		readonly List<Pair<Time>> fragmentRanges = new List<Pair<Time>>();

		public IEnumerable<Entry> this[Time startTime, Time endTime]
		{
			get
			{
				Pair<Time> requestRange = new Pair<Time>(startTime, endTime);

				// TODO: Defragment on insertion
				fragmentRanges.AddRange(Remove(requestRange.Single(), fragmentRanges));

				return source[startTime, endTime];
			}
		}

		public EntryCache(EntryResampler source)
		{
			this.source = source;
		}

		static IEnumerable<Pair<Time>> Remove(IEnumerable<Pair<Time>> ranges, IEnumerable<Pair<Time>> exclusions)
		{
			List<Pair<Time>> rangeList = new List<Pair<Time>>(ranges);

			foreach (Pair<Time> exclusion in exclusions)
				foreach (Pair<Time> range in rangeList.ToArray())
				{
					rangeList.Remove(range);
					rangeList.AddRange(Remove(range, exclusion));
				}

			return rangeList;
		}
		static IEnumerable<Pair<Time>> Remove(Pair<Time> range, Pair<Time> exclusion)
		{
			Pair<Time> range1 = new Pair<Time>(range.A, exclusion.A);
			Pair<Time> range2 = new Pair<Time>(exclusion.B, range.B);

			if (range1.B - range1.A > Time.Zero) yield return range1;
			if (range2.B - range2.A > Time.Zero) yield return range2;
		}
		static Pair<Time> Intersect(Pair<Time> a, Pair<Time> b)
		{
			return new Pair<Time>(Time.Max(a.A, b.A), Time.Min(a.B, b.B));
		}
	}
}
