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
using Krach.Maps.Scalar;
using Krach.Basics;
using Krach.Maps;

namespace Visualizer.Drawing.Timing
{
	public class WrappingTimeManager : TimeManager
	{
		SymmetricRangeMap mapping;
		IEnumerable<SymmetricRangeMap> graphMappings;
		double gapLength = 0;

		public double GapLength
		{
			get { return gapLength; }
			set
			{
				if (value < 0 || value >= 1) throw new ArgumentOutOfRangeException("value");

				gapLength = value;
			}
		}
		public override SymmetricRangeMap Mapping { get { return mapping; } }
		public override IEnumerable<SymmetricRangeMap> GraphMappings { get { return graphMappings; } }

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

			double startTime = Time - (1 - GapLength) * Width;
			double startPosition = (fractionalIntervals + GapLength) % 1;
			double endTime = Time;
			double endPosition = startPosition + (1 - GapLength);

			mapping = new SymmetricRangeMap(new Range<double>((wholeIntervals + 0) * Width, (wholeIntervals + 1) * Width), Mappers.Linear);

			if (startTime >= wholeIntervals * Width)
				graphMappings = new SymmetricRangeMap[]
				{
					new SymmetricRangeMap(new Range<double>(startTime, endTime), new Range<double>(startPosition,endPosition), Mappers.Linear)
				};
			else
				graphMappings = new SymmetricRangeMap[]
				{
					new SymmetricRangeMap(new Range<double>(startTime, wholeIntervals * Width), new Range<double>(startPosition, 1), Mappers.Linear),
					new SymmetricRangeMap(new Range<double>(wholeIntervals * Width, endTime), new Range<double>(0, endPosition - 1), Mappers.Linear)
				};
		}
	}
}
