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

using System.Diagnostics;

namespace Visualizer.Data
{
	public class Timer
	{
		readonly Stopwatch stopwatch = new Stopwatch();

		double time;

		public bool IsUpdated { get; set; }
		public double Time
		{
			get
			{
				if (IsUpdated)
					lock (stopwatch)
						time = stopwatch.Elapsed.TotalSeconds;

				return time;
			}
			set
			{
				time = value;
			}
		}

		public Timer()
		{
			IsUpdated = true;
			
			Reset();
		}

		public void Reset()
		{
			time = 0;

			lock (stopwatch)
			{
				stopwatch.Reset();
				stopwatch.Start();
			}
		}
	}
}
