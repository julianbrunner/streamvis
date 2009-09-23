// Copyright © Julian Brunner 2009

// This file is part of Stream Visualizer (streamvis).
// 
// Stream Visualizer is free software: you can redistribute it and/or modify
// it under the terms of the GNU General Public License as published by
// the Free Software Foundation, either version 3 of the License, or
// (at your option) any later version.
// 
// Stream Visualizer is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU General Public License for more details.
// 
// You should have received a copy of the GNU General Public License
// along with Stream Visualizer.  If not, see <http://www.gnu.org/licenses/>.

using System;
using System.Collections.Generic;
using Utility;
using Visualizer.Data;

namespace Visualizer.Drawing.Data
{
	public class EntryCache
	{
		readonly EntryResampler entryResampler;
		readonly SearchList<Range<Time>, Time> ranges = new SearchList<Range<Time>, Time>(range => range.Start);
		readonly SearchList<Entry, Time> entries = new SearchList<Entry, Time>(entry => entry.Time);

		public Entry[] this[Range<Time> range]
		{
			get
			{
				foreach (Range<Time> missingRange in Exclude(range.Single(), ranges))
				{
					CacheFragment fragment = entryResampler[missingRange];

					if (!fragment.IsEmpty)
					{
						Time start = fragment.Range.Start;
						Time end = fragment.Range.End;

						int indexAfter = ranges.FindIndex(start);
						int indexBefore = indexAfter - 1;

						if (indexAfter >= 0 && indexAfter < ranges.Count && ranges[indexAfter].Start == end)
						{
							end = ranges[indexAfter].End;
							ranges.Remove(indexAfter);
						}
						if (indexBefore >= 0 && indexBefore < ranges.Count && ranges[indexBefore].End == start)
						{
							start = ranges[indexBefore].Start;
							ranges.Remove(indexBefore);
						}

						ranges.Insert(new Range<Time>(start, end));
						entries.Insert(fragment.Entries);
					}
				}

				return entries[range];
			}
		}

		public bool IsEmpty { get { return entries.Count == 0; } }
		public Entry FirstEntry
		{
			get
			{
				if (entries.Count == 0) throw new InvalidOperationException();

				return entries[0];
			}
		}
		public Entry LastEntry
		{
			get
			{
				if (entries.Count == 0) throw new InvalidOperationException();

				return entries[entries.Count - 1];
			}
		}

		public EntryCache(EntryResampler source)
		{
			this.entryResampler = source;

			this.entryResampler.SampleDistanceChanged += entryResampler_SampleDistanceChanged;
		}

		public void Clear()
		{
			ranges.Clear();
			entries.Clear();
		}

		void entryResampler_SampleDistanceChanged(object sender, EventArgs e)
		{
			Clear();
		}

		static IEnumerable<Range<Time>> Exclude(IEnumerable<Range<Time>> ranges, IEnumerable<Range<Time>> exclusions)
		{
			List<Range<Time>> rangeList = new List<Range<Time>>(ranges);

			foreach (Range<Time> exclusion in exclusions)
			{
				Range<Time>[] oldRanges = rangeList.ToArray();

				rangeList.Clear();

				foreach (Range<Time> range in oldRanges)
				{
					Range<Time> intersection = Intersect(range, exclusion);

					if (intersection.IsEmpty()) rangeList.Add(range);
					else
					{
						Range<Time> range1 = new Range<Time>(range.Start, exclusion.Start);
						Range<Time> range2 = new Range<Time>(exclusion.End, range.End);

						if (!range1.IsEmpty()) rangeList.Add(range1);
						if (!range2.IsEmpty()) rangeList.Add(range2);
					}
				}
			}

			return rangeList;
		}
		static Range<Time> Intersect(Range<Time> a, Range<Time> b)
		{
			return new Range<Time>(Time.Max(a.Start, b.Start), Time.Min(a.End, b.End));
		}
	}
}
