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

namespace Visualizer.Drawing
{
	public class GraphSettings
	{
		double lineWidth = 0;
		
		public bool ExtendGraphs { get; set; }
		public double LineWidth
		{
			get { return lineWidth; }
			set
			{
				if (value <= 0) throw new ArgumentOutOfRangeException("value");
				
				lineWidth = value;
			}
		}
		
		public GraphSettings()
		{
			ExtendGraphs = true;
			LineWidth = 1;
		}
	}
}