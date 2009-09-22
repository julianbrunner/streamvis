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
using System.Diagnostics;

namespace Graphics
{
	public class FrameCounter : IComponent, IUpdateable
	{
		const int frameWindow = 20;
		
		readonly Stopwatch stopwatch;
		
		TimeSpan lastOverflow = TimeSpan.Zero;
		int frames = 0;
		
		public bool IsUpdated { get; set; }
		public double FramesPerSecond { get; private set; }
		
		public FrameCounter()
		{			
			stopwatch = new Stopwatch();
			stopwatch.Reset();
			stopwatch.Start();
			
			IsUpdated = true;
		}
		
		public void Update()
		{
			if (IsUpdated)
			{
				if (++frames == frameWindow)
				{
					TimeSpan time =  stopwatch.Elapsed;
					FramesPerSecond = frameWindow / (time - lastOverflow).TotalSeconds;
					lastOverflow = time;
					frames = 0;
				}
			}
		}
	}
}
