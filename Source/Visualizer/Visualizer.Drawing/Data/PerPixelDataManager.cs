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

namespace Visualizer.Drawing.Data
{
	public class PerPixelDataManager : DataManager
	{
		double samplesPerPixel = 0;
		
		public double SamplesPerPixel
		{
			get { return samplesPerPixel; }
			set
			{
				if (value <= 0) throw new ArgumentOutOfRangeException("value");
				
				samplesPerPixel = value;
			}
		}

		public PerPixelDataManager(Diagram diagram)
			: base(diagram)
		{
			SamplesPerPixel = 1;
		}

		public override void Update()
		{
			base.Update();

			if (Diagram.Layouter.Area.Width > 0)
			{
				double pixelsPerSecond = Diagram.Layouter.Area.Width / Diagram.TimeManager.Width;
				double samplesPerSecond = SamplesPerPixel * pixelsPerSecond;

				foreach (Graph graph in Diagram.Graphs)
					SetSampleDistance(graph.StreamManager.EntryResampler, 1.0 / samplesPerSecond);
			}
		}
	}
}