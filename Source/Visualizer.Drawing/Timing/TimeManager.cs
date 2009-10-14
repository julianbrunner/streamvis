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
using Visualizer.Data;

namespace Visualizer.Drawing.Timing
{
	public abstract class TimeManager
	{
		readonly Timer timer;

		// TODO: Create and document visibility policy
		public bool IsUpdated { get; set; }
		public Time Time { get; set; }
		public Time Width { get; set; }
		/// <summary>
		/// Gets the overall Range, in which graphs are drawn.
		/// </summary>
		public abstract TimeRange Range { get; }
		/// <summary>
		/// Gets the specific sub Ranges, in which the graphs are drawn.
		/// </summary>
		public abstract IEnumerable<TimeRange> GraphRanges { get; }

		protected TimeManager(Timer timer)
		{
			this.timer = timer;
			
			// TODO: Create and document policy on property initialization (initialize all properties for other components, too)
			Time = Time.Zero;
			Width = new Time(10.0);
			IsUpdated = true;
		}

		public virtual void Update()
		{
			if (IsUpdated) Time = timer.Time;
		}
	}
}
