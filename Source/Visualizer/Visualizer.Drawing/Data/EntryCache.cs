// Copyright © Julian Brunner 2009 - 2011

// This file is part of Stream Visualizer (streamvis).
// 
// Stream Visualizer is free software: you can redistribute it and/or modify it
// under the terms of the GNU General Public License as published by the Free
// Software Foundation, either version 3 of the License, or (at your option) any
// later version.
// 
// Stream Visualizer is distributed in the hope that it will be useful, but
// WITHOUT ANY WARRANTY; without even the implied warranty of MERCHANTABILITY or
// FITNESS FOR A PARTICULAR PURPOSE. See the GNU General Public License for more
// details.
// 
// You should have received a copy of the GNU General Public License along with
// Stream Visualizer. If not, see <http://www.gnu.org/licenses/>.

using System;
using System.Collections.Generic;
using Visualizer.Data;
using Krach.Collections;
using Krach.Basics;
using Krach.Extensions;

namespace Visualizer.Drawing.Data
{
	public class EntryCache
	{
		readonly EntryResampler entryResampler;
		readonly SearchList<Range<double>, double> ranges = new SearchList<Range<double>, double>(range => range.Start);
		readonly SearchList<Entry, double> entries = new SearchList<Entry, double>(entry => entry.Time);

		public Entry[] this[Range<double> range]
		{
			get
			{
				foreach (Range<double> missingRange in Exclude(Enumerables.Single(range), ranges))
				{
					CacheFragment fragment = entryResampler
					[
						new Range<double>
						(
							missingRange.Start - 0.5 * entryResampler.SampleDistance,
							missingRange.End + 0.5 * entryResampler.SampleDistance
						)
					];

					if (!fragment.IsEmpty)
					{
						double start = fragment.Range.Start;
						double end = fragment.Range.End;

						int indexAfter = ranges.FindIndex(start);
						int indexBefore = indexAfter - 1;

						// The fragment can be merged with a range that comes after the fragment
						if (indexAfter >= 0 && indexAfter < ranges.Count && ranges[indexAfter].Start == end)
						{
							end = ranges[indexAfter].End;
							ranges.Remove(indexAfter);
						}
						// The fragment can be merged with a range that comes before the fragment
						if (indexBefore >= 0 && indexBefore < ranges.Count && ranges[indexBefore].End == start)
						{
							start = ranges[indexBefore].Start;
							ranges.Remove(indexBefore);
						}

						ranges.Insert(new Range<double>(start, end));
						entries.Insert(fragment.Entries);
					}
				}

				return entries[range.Start, range.End];
			}
		}

		public bool IsEmpty { get { return entries.Count == 0; } }
		public Entry FirstEntry
		{
			get
			{
				if (entries.IsEmpty) throw new InvalidOperationException();

				return entries[0];
			}
		}
		public Entry LastEntry
		{
			get
			{
				if (entries.IsEmpty) throw new InvalidOperationException();

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

		static IEnumerable<Range<double>> Exclude(IEnumerable<Range<double>> ranges, IEnumerable<Range<double>> exclusions)
		{
			List<Range<double>> result = new List<Range<double>>(ranges);

			foreach (Range<double> exclusion in exclusions)
			{
				IEnumerable<Range<double>> oldRanges = result.ToArray();

				result.Clear();

				foreach (Range<double> range in oldRanges)
					if (Intersect(range, exclusion)) 
					{
						if (range.Start <= exclusion.Start) result.Add(new Range<double>(range.Start, exclusion.Start));
						if (exclusion.End <= range.End) result.Add(new Range<double>(exclusion.End, range.End));
					}
					else result.Add(range);
			}

			return result;
		}
		static bool Intersect(Range<double> range1, Range<double> range2)
		{
			return Math.Max(range1.Start, range2.Start) <= Math.Min(range1.End, range2.End);
		}
	}
}
