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

using System.Collections.Generic;
using Visualizer.Data;
using Krach.Basics;
using Krach.Maps.Scalar;
using Krach.Maps;

namespace Visualizer.Drawing.Timing
{
	public class ContinuousTimeManager : TimeManager
	{
		SymmetricRangeMap mapping;

		public override SymmetricRangeMap Mapping { get { return mapping; } }
		public override IEnumerable<SymmetricRangeMap> GraphMappings { get { yield return mapping; } }

		public ContinuousTimeManager(Timer timer) : base(timer) { }

		public override void Update()
		{
			base.Update();

			mapping = new SymmetricRangeMap(new Range<double>(Time - Width, Time), Mappers.Linear);
		}
	}
}
