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
using Utility.Extensions;
using Visualizer.Data;

namespace Visualizer.Drawing.Data
{
	public class EntryResampler
	{
		readonly SearchList<Entry, double> entries;

		double sampleDistance;

		public event EventHandler SampleDistanceChanged;

		public double SampleDistance
		{
			get { return sampleDistance; }
			set
			{
				if (value <= 0) throw new ArgumentOutOfRangeException("value");

				if (sampleDistance != value)
				{
					sampleDistance = value;
					OnSampleDistanceChanged();
				}
			}
		}

		public CacheFragment this[Range<double> range]
		{
			get
			{
				if (sampleDistance == 0) return CacheFragment.Empty;
				if (entries.IsEmpty) return CacheFragment.Empty;

				double startTime = range.Start;
				double endTime = range.End;

				startTime = Math.Max(startTime, entries[0].Time);
				endTime = Math.Min(endTime, entries[entries.Count - 1].Time);

				startTime = startTime.Ceiling(sampleDistance);
				endTime = endTime.Floor(sampleDistance);

				if (startTime >= endTime) return CacheFragment.Empty;

				List<Entry> samples = new List<Entry>();

				for (double time = startTime; time + sampleDistance <= endTime; time += sampleDistance)
					samples.Add(Aggregate(entries, time, time + sampleDistance));

				return new CacheFragment(new Range<double>(startTime, endTime), samples);
			}
		}

		public EntryResampler(SearchList<Entry, double> source)
		{
			this.entries = source;
		}

		protected virtual void OnSampleDistanceChanged()
		{
			if (SampleDistanceChanged != null) SampleDistanceChanged(this, EventArgs.Empty);
		}

		static Entry Aggregate(SearchList<Entry, double> source, double startTime, double endTime)
		{
			int startIndex = source.FindIndex(startTime);
			int endIndex = source.FindIndex(endTime);

			Entry beforeStart = source[startIndex].Time > startTime ? source[startIndex - 1] : source[startIndex];
			Entry afterStart = source[startIndex];
			Entry beforeEnd = source[endIndex].Time > endTime ? source[endIndex - 1] : source[endIndex];
			Entry afterEnd = source[endIndex];

			double startFraction = beforeStart.Time == afterStart.Time ? 0 : (startTime - beforeStart.Time) / (afterStart.Time - beforeStart.Time);
			double startValue = (1 - startFraction) * beforeStart.Value + startFraction * afterStart.Value;
			Entry start = new Entry(startTime, startValue);
			double endFraction = beforeEnd.Time == afterEnd.Time ? 0 : (endTime - beforeEnd.Time) / (afterEnd.Time - beforeEnd.Time);
			double endValue = (1 - endFraction) * beforeEnd.Value + endFraction * afterEnd.Value;
			Entry end = new Entry(endTime, endValue);

			double area = 0;
			Entry last = start;
			foreach (Entry entry in source[startIndex, endIndex]) area += GetArea(last, last = entry);
			area += GetArea(last, end);

			return new Entry(0.5 * (start.Time + end.Time), area / (end.Time - start.Time));
		}
		static double GetArea(Entry start, Entry end)
		{
			return 0.5 * (start.Value + end.Value) * (end.Time - start.Time);
		}
	}
}
