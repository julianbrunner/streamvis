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

using Visualizer.Data;
using Visualizer.Drawing.Timing;

namespace Visualizer.Drawing.Data
{
	public class PerPixelDataManager : DataManager
	{
		readonly double sampleFrequency;
		readonly Layouter layouter;
		readonly TimeManager timeManager;

		public PerPixelDataManager(EntryData entryData, double sampleFrequency, Layouter layouter, TimeManager timeManager)
			: base(entryData)
		{
			this.sampleFrequency = sampleFrequency;
			this.layouter = layouter;
			this.timeManager = timeManager;
		}

		public override void Update()
		{
			base.Update();

			int width = layouter.Area.Width;
			Time time = timeManager.Range.Range.End - timeManager.Range.Range.Start;
			double pixelsPerSecond = width / time.Seconds;

			EntryResampler.SampleDistance = new Time(1.0) / (sampleFrequency * pixelsPerSecond);
		}
	}
}