using System.Collections.Generic;
using Extensions;

namespace Visualizer.Data.Transformations
{
	public class EntryCache
	{
		readonly EntryResampler resampler;
		readonly SearchList<Range<Time>, Time> ranges = new SearchList<Range<Time>, Time>(range => range.Start);
		readonly SearchList<Entry, Time> entries = new SearchList<Entry, Time>(entry => entry.Time);

		public IEnumerable<Entry> this[Time startTime, Time endTime]
		{
			get
			{
				Range<Time> requestRange = new Range<Time>(startTime, endTime);

				foreach (Range<Time> missingRange in Exclude(requestRange.Single(), ranges))
				{
					Fragment fragment = resampler[missingRange.Start, missingRange.End];

					if (!fragment.IsEmpty)
					{
						Time start = fragment.Range.Start;
						Time end = fragment.Range.End;

						// TODO: Is this slow?
						// TODO: Can this be implemented in a more readable way?
						int index = ranges.FindIndex(start);

						if (index > 0 && ranges[index - 1].End == start)
						{
							start = ranges[index - 1].Start;
							ranges.Remove(ranges[index - 1]);
						}
						if (index < ranges.Count && ranges[index].Start == end)
						{
							end = ranges[index].End;
							ranges.Remove(ranges[index]);
						}

						ranges.Insert(new Range<Time>(start, end));

						entries.Insert(fragment.Entries);
					}
				}

				return entries[requestRange];
			}
		}

		public EntryCache(EntryResampler source)
		{
			this.resampler = source;
		}

		static IEnumerable<Range<Time>> Exclude(IEnumerable<Range<Time>> ranges, IEnumerable<Range<Time>> exclusions)
		{
			List<Range<Time>> rangeList = new List<Range<Time>>(ranges);

			foreach (Range<Time> exclusion in exclusions)
			{
				IEnumerable<Range<Time>> currentRanges = rangeList.ToArray();

				rangeList.Clear();
				foreach (Range<Time> range in currentRanges) rangeList.AddRange(Exclude(range, exclusion));
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
