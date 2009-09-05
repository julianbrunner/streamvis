using System.Collections.Generic;
using System.Linq;
using Extensions;
using Extensions.Searching;

namespace Visualizer.Data.Transformations
{
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

				IEnumerable<Fragment> fragments = from missingRange in Exclude(requestRange.Single(), cachedRanges)
												  let fragment = source[missingRange.Start, missingRange.End]
												  where !fragment.IsEmpty
												  select fragment;

				// TODO: Defragment on insertion
				foreach (Fragment fragment in fragments)
				{
					cachedRanges.Add(fragment.Range);
					entries.Insert(fragment.Entries);
				}

				return entries[requestRange];
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
			Range<Time> range1 = new Range<Time>(range.Start, exclusion.Start);
			Range<Time> range2 = new Range<Time>(exclusion.End, range.End);

			if (range1.End - range1.Start > Time.Zero) yield return range1;
			if (range2.End - range2.Start > Time.Zero) yield return range2;
		}
	}
}
