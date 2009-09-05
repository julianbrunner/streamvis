using System.Collections.Generic;
using System.Linq;
using Extensions;
using Extensions.Searching;

namespace Visualizer.Data.Transformations
{
	// TODO:
	// Deferred execution (iterators and LINQ) make it extremely difficult to spot performance bottlenecks.
	// Consider removing those constructs in performance critical areas.
	public class EntryCache
	{
		readonly EntryResampler source;
		readonly List<Range<Time>> cachedRanges = new List<Range<Time>>();
		readonly SearchList<Entry, Time> entries = new SearchList<Entry, Time>();

		public IEnumerable<Entry> this[Time startTime, Time endTime]
		{
			get
			{
				Range<Time> requestRange = new Range<Time>(startTime, endTime);

				IEnumerable<Range<Time>> missingRanges = Exclude(requestRange.Single(), cachedRanges);

				foreach (Range<Time> missingRange in missingRanges)
				{
					Fragment fragment = source[missingRange.Start, missingRange.End];

					// TODO: Defragment on insertion
					if (!fragment.IsEmpty)
					{
						cachedRanges.Add(fragment.Range);
						entries.Insert(fragment.Entries);
					}
				}

				return entries[requestRange].ToArray();
			}
		}

		public EntryCache(EntryResampler source)
		{
			this.source = source;
		}

		static IEnumerable<Range<Time>> Exclude(IEnumerable<Range<Time>> ranges, IEnumerable<Range<Time>> exclusions)
		{
			List<Range<Time>> rangeList = new List<Range<Time>>(ranges);

			foreach (Range<Time> exclusion in exclusions)
				foreach (Range<Time> range in rangeList.ToArray())
				{
					rangeList.Remove(range);
					rangeList.AddRange(Exclude(range, exclusion));
				}

			return rangeList;
		}
		static IEnumerable<Range<Time>> Exclude(Range<Time> range, Range<Time> exclusion)
		{
			exclusion = Intersect(range, exclusion);

			if (exclusion.IsEmpty()) yield return range;
			else
			{
				Range<Time> range1 = new Range<Time>(range.Start, exclusion.Start);
				Range<Time> range2 = new Range<Time>(exclusion.End, range.End);

				if (!range1.IsEmpty()) yield return range1;
				if (!range2.IsEmpty()) yield return range2;
			} 
		}
		static Range<Time> Intersect(Range<Time> a, Range<Time> b)
		{
			return new Range<Time>(Time.Max(a.Start, b.Start), Time.Min(a.End, b.End));
		}
	}
}
