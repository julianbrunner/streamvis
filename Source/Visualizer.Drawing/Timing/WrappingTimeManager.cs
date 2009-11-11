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
	public class WrappingTimeManager : TimeManager
	{
		LinearMapping timeMapping;
		IEnumerable<LinearMapping> graphTimeMappings;

		public double GapLength { get; set; }
		public override LinearMapping TimeMapping { get { return timeMapping; } }
		public override IEnumerable<LinearMapping> GraphTimeMappings { get { return graphTimeMappings; } }

		public WrappingTimeManager(Timer timer)
			: base(timer)
		{
			GapLength = 0.2;
		}

		public override void Update()
		{
			base.Update();

			double intervals = Time / Width;
			int wholeIntervals = (int)intervals;
			double fractionalIntervals = intervals - wholeIntervals;

			Time startTime = Time - (1 - GapLength) * Width;
			double startPosition = (fractionalIntervals + GapLength) % 1;
			Time endTime = Time;
			double endPosition = startPosition + (1 - GapLength);

			timeMapping = new LinearMapping(new Range<double>((wholeIntervals + 0) * Width.Seconds, (wholeIntervals + 1) * Width.Seconds));

			if (startTime >= wholeIntervals * Width)
				graphTimeMappings = new LinearMapping[]
				{
					new LinearMapping(new Range<double>(startTime.Seconds, endTime.Seconds), new Range<double>(startPosition,endPosition))
				};
			else
				graphTimeMappings = new LinearMapping[]
				{
					new LinearMapping(new Range<double>(startTime.Seconds, wholeIntervals * Width.Seconds), new Range<double>(startPosition, 1)),
					new LinearMapping(new Range<double>(wholeIntervals * Width.Seconds, endTime.Seconds), new Range<double>(0, endPosition - 1))
				};
		}
	}
}
