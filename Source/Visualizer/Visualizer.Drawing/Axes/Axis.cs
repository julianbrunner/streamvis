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
using System.Drawing;
using System.Linq;
using Krach.Basics;
using Krach.Extensions;
using Graphics;

namespace Visualizer.Drawing.Axes
{
	public abstract class Axis
	{
		readonly Drawer drawer;
		readonly Diagram diagram;
		
		int markerCount;

		protected Drawer Drawer { get { return drawer; } }
		protected Diagram Diagram { get { return diagram; } }
		protected abstract Range<double> MarkersRange { get; }
		protected IEnumerable<double> Markers
		{
			get
			{
				if (MarkersRange.IsEmpty) return Enumerable.Empty<double>();

				return Scalars.GetMarkers(MarkersRange.Start, MarkersRange.End, MarkerCount);
			}
		}

		public int MarkerCount
		{
			get { return markerCount; }
			set
			{
				if (value <= 0) throw new ArgumentOutOfRangeException("value");
				
				markerCount = value;
			}
		}
		public Color Color { get; set; }
		public Size MaximumCaptionSize
		{
			get
			{
				IEnumerable<double> markers = Markers;

				int maximumWidth = markers.Any() ? markers.Max(time => Drawer.GetTextSize(time).Width) : 0;
				int maximumHeight = markers.Any() ? markers.Max(time => Drawer.GetTextSize(time).Height) : 0;

				return new Size(maximumWidth, maximumHeight);
			}
		}

		protected Axis(Drawer drawer, Diagram diagram)
		{
			this.drawer = drawer;
			this.diagram = diagram;
			
			MarkerCount = 5;
			Color = Color.White;
		}

		public abstract void Draw();
	}
}
