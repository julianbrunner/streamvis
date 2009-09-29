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

using System.Drawing;
using Graphics;
using OpenTK.Math;
using Visualizer.Data;
using Visualizer.Drawing.Data;
using Visualizer.Drawing.Timing;
using Visualizer.Drawing.Values;

namespace Visualizer.Drawing
{
	public class Graph
	{
		readonly Drawer drawer;
		readonly Diagram diagram;
		readonly Layouter layouter;
		readonly ValueManager valueManager;
		readonly StreamManager streamManager;

		public bool IsDrawn { get; set; }
		public Color Color { get; set; }
		public StreamManager StreamManager { get { return streamManager; } }

		public Graph(Drawer drawer, Diagram diagram, Layouter layouter, ValueManager valueManager, StreamManager streamManager)
		{
			this.drawer = drawer;
			this.diagram = diagram;
			this.layouter = layouter;
			this.valueManager = valueManager;
			this.streamManager = streamManager;

			IsDrawn = true;
		}

		public void Update()
		{
			streamManager.Update();
		}
		public void Draw()
		{
			if (IsDrawn && !streamManager.IsEmpty)
			{
				ValueRange valueRange = valueManager.Range;

				Entry firstEntry = streamManager.FirstEntry;
				Entry lastEntry = streamManager.LastEntry;

				foreach (DataSegment segment in streamManager.Segments)
				{
					TimeRange timeRange = segment.TimeRange;

					Entry? startEntry = null;
					Entry? endEntry = null;

					// TODO: Move the ExtendGraphs property so Graph doesn't need a DataManager
					if (diagram.ExtendGraphs)
					{
						if (firstEntry.Time - timeRange.Range.Start > 1.5 * streamManager.SampleDistance) startEntry = new Entry(timeRange.Range.Start, firstEntry.Value);
						if (timeRange.Range.End - lastEntry.Time > 1.5 * streamManager.SampleDistance) endEntry = new Entry(timeRange.Range.End, lastEntry.Value);
						if (segment.Entries.Length == 0)
						{
							if (startEntry == null) startEntry = new Entry(timeRange.Range.Start, endEntry.Value.Value);
							if (endEntry == null) endEntry = new Entry(timeRange.Range.End, startEntry.Value.Value);
						}
					}

					int vertexCount = segment.Entries.Length;

					if (startEntry.HasValue) vertexCount++;
					if (endEntry.HasValue) vertexCount++;

					float[] vertices = new float[vertexCount * 2];
					int position = 0;

					if (startEntry.HasValue)
					{
						vertices[position++] = (float)startEntry.Value.Time.Seconds;
						vertices[position++] = (float)startEntry.Value.Value;
					}

					foreach (Entry entry in segment.Entries)
					{
						vertices[position++] = (float)entry.Time.Seconds;
						vertices[position++] = (float)entry.Value;
					}

					if (endEntry.HasValue)
					{
						vertices[position++] = (float)endEntry.Value.Time.Seconds;
						vertices[position++] = (float)endEntry.Value.Value;
					}

					Matrix4 transformation = valueRange.Transformation * timeRange.Transformation * layouter.Transformation;

					drawer.DrawLineStrip(vertices, transformation, Color, (float)diagram.LineWidth);
				}
			}
		}
	}
}
