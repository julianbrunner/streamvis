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

using System.Collections.Generic;
using Utility;
using Visualizer.Data;

namespace Visualizer.Drawing.Timing
{
	public class ShiftingTimeManager : TimeManager
	{
		LinearMapping timeMapping;
		LinearMapping graphTimeMappings;

		public double ShiftLength { get; set; }
		public override LinearMapping TimeMapping { get { return timeMapping; } }
		public override IEnumerable<LinearMapping> GraphTimeMappings { get { yield return graphTimeMappings; } }

		public ShiftingTimeManager(Timer timer)
			: base(timer)
		{
			ShiftLength = 0.8;
		}

		public override void Update()
		{
			base.Update();

			Time interval = Width * ShiftLength;

			double intervals = Time / interval;
			int wholeIntervals = (int)intervals;
			double fractionalIntervals = intervals - wholeIntervals;

			Time startTime = interval * (wholeIntervals + 1) - Width;
			double startPosition = 0;
			Time endTime = Time;
			double endPosition = (1 - ShiftLength) + ShiftLength * fractionalIntervals;

			timeMapping = new LinearMapping(new Range<double>(startTime.Seconds, startTime.Seconds + Width.Seconds));
			graphTimeMappings = new LinearMapping(new Range<double>(startTime.Seconds, endTime.Seconds), new Range<double>(startPosition, endPosition));
		}
	}
}
