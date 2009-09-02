using System.Collections.Generic;
using Extensions;
using Extensions.Searching;

namespace Visualizer.Data.Transformations
{
	public class EntryCache
	{
		readonly EntryResampler source;
		readonly SearchList<Entry, Time> entries = new SearchList<Entry, Time>();
		readonly List<Range<Time>> fragmentRanges = new List<Range<Time>>();

		public IEnumerable<Entry> this[Time startTime, Time endTime]
		{
			get
			{
				Range<Time> requestRange = new Range<Time>(startTime, endTime);
				IEnumerable<Range<Time>> missingRanges = Exclude(requestRange.Single(), fragmentRanges);

				// TODO: Defragment on insertion
				fragmentRanges.AddRange(missingRanges);

				return source[startTime, endTime];
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
		static Range<Time> Intersect(Range<Time> a, Range<Time> b)
		{
			return new Range<Time>(Time.Max(a.Start, b.Start), Time.Min(a.End, b.End));
		}
	}
}
