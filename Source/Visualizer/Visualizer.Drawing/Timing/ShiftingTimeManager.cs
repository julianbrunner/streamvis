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
using Krach.Basics;
using Krach.Maps.Scalar;
using Krach.Maps;

namespace Visualizer.Drawing.Timing
{
	public class ShiftingTimeManager : TimeManager
	{
		SymmetricRangeMap mapping;
		SymmetricRangeMap graphMappings;

		double shiftLength = 0;

		public double ShiftLength
		{
			get { return shiftLength; }
			set
			{
				if (value <= 0 || value > 1) throw new ArgumentOutOfRangeException("value");

				shiftLength = value;
			}
		}
		public override SymmetricRangeMap Mapping { get { return mapping; } }
		public override IEnumerable<SymmetricRangeMap> GraphMappings { get { yield return graphMappings; } }

		public ShiftingTimeManager(Timer timer)
			: base(timer)
		{
			ShiftLength = 0.8;
		}

		public override void Update()
		{
			base.Update();

			double interval = Width * ShiftLength;

			double intervals = Time / interval;
			int wholeIntervals = (int)intervals;
			double fractionalIntervals = intervals - wholeIntervals;

			double startTime = interval * (wholeIntervals + 1) - Width;
			double startPosition = 0;
			double endTime = Time;
			double endPosition = (1 - ShiftLength) + ShiftLength * fractionalIntervals;

			mapping = new SymmetricRangeMap(new Range<double>(startTime, startTime + Width), Mappers.Linear);
			graphMappings = new SymmetricRangeMap(new Range<double>(startTime, endTime), new Range<double>(startPosition, endPosition), Mappers.Linear);
		}
	}
}
