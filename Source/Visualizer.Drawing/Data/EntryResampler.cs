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
	public class EntryResampler
	{
		readonly SearchList<Entry, Time> entries;

		Time sampleDistance;

		public event EventHandler SampleDistanceChanged;

		public Time SampleDistance
		{
			get { return sampleDistance; }
			set
			{
				if (value <= Time.Zero) throw new ArgumentOutOfRangeException("value");

				if (sampleDistance != value)
				{
					sampleDistance = value;
					OnSampleDistanceChanged();
				}
			}
		}

		public CacheFragment this[Range<Time> range]
		{
			get
			{
				if (entries.Count == 0) return CacheFragment.Empty;

				Time startTime = range.Start;
				Time endTime = range.End;

				startTime = Time.Max(startTime, entries[0].Time);
				endTime = Time.Min(endTime, entries[entries.Count - 1].Time);

				startTime = startTime.Ceiling(sampleDistance, Time.Zero);
				endTime = endTime.Floor(sampleDistance, Time.Zero);

				if (startTime >= endTime) return CacheFragment.Empty;

				List<Entry> samples = new List<Entry>();

				for (Time time = startTime; time < endTime; time += sampleDistance)
					samples.Add(Aggregate(entries, time, time + sampleDistance));

				return new CacheFragment(new Range<Time>(startTime, endTime), samples);
			}
		}

		public EntryResampler(SearchList<Entry, Time> source)
		{
			this.entries = source;
		}

		protected virtual void OnSampleDistanceChanged()
		{
			if (SampleDistanceChanged != null) SampleDistanceChanged(this, EventArgs.Empty);
		}

		static Entry Aggregate(SearchList<Entry, Time> source, Time startTime, Time endTime)
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

			return new Entry(0.5 * (start.Time + end.Time), area / (end.Time - start.Time).Seconds);
		}
		static double GetArea(Entry start, Entry end)
		{
			return 0.5 * (start.Value + end.Value) * (end.Time - start.Time).Seconds;
		}
	}
}
