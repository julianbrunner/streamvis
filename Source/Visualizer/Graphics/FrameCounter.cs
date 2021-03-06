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
using System.Diagnostics;
using Graphics.Components;

namespace Graphics
{
	public class FrameCounter : IComponent, IUpdateable
	{
		readonly Stopwatch stopwatch;

		TimeSpan lastOverflow = TimeSpan.Zero;
		int frames = 0;
		int cycleLength = 0;

		public bool IsUpdated { get; set; }
		public int CycleLength
		{
			get { return cycleLength; }
			set
			{
				if (value <= 0) throw new ArgumentOutOfRangeException("value");

				cycleLength = value;
			}
		}
		public double FramesPerSecond { get; private set; }

		public FrameCounter()
		{
			stopwatch = new Stopwatch();
			stopwatch.Reset();
			stopwatch.Start();

			IsUpdated = true;
			CycleLength = 20;
		}

		public void Update()
		{
			if (++frames == CycleLength)
			{
				TimeSpan time = stopwatch.Elapsed;
				FramesPerSecond = CycleLength / (time - lastOverflow).TotalSeconds;
				lastOverflow = time;
				frames = 0;
			}
		}
	}
}
