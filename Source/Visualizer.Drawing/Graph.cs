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
using Utility.Extensions;
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
		readonly EntryData entryData;
		readonly StreamManager streamManager;

		public EntryData EntryData { get { return entryData; } }
		public StreamManager StreamManager { get { return streamManager; } }
		public bool IsDrawn { get; set; }
		public Color Color { get; set; }

		public Graph(Drawer drawer, Diagram diagram, EntryData entryData)
		{
			this.drawer = drawer;
			this.diagram = diagram;
			this.entryData = entryData;

			streamManager = new StreamManager(diagram, entryData);

			IsDrawn = true;
		}

		public void Update()
		{
			streamManager.Update();
		}
		public void Draw()
		{
			TimeRange timeRange = diagram.TimeManager.Range;
			ValueRange valueRange = diagram.ValueManager.Range;

			if (IsDrawn && !streamManager.EntryCache.IsEmpty && !timeRange.Range.IsEmpty() && !valueRange.Range.IsEmpty())
			{
				Entry firstEntry = streamManager.EntryCache.FirstEntry;
				Entry lastEntry = streamManager.EntryCache.LastEntry;

				// TODO: Maybe screw the segment manager and do it the old way, would also be more symmetric with the timeManager
				foreach (DataSegment segment in streamManager.Segments)
				{
					// TODO: Inline this?
					TimeRange segmentTimeRange = segment.TimeRange;

					Entry? startEntry = null;
					Entry? endEntry = null;

					if (diagram.GraphSettings.ExtendGraphs)
					{
						if (firstEntry.Time - segmentTimeRange.Range.Start > 1.5 * streamManager.EntryResampler.SampleDistance) startEntry = new Entry(segmentTimeRange.Range.Start, firstEntry.Value);
						if (segmentTimeRange.Range.End - lastEntry.Time > 1.5 * streamManager.EntryResampler.SampleDistance) endEntry = new Entry(segmentTimeRange.Range.End, lastEntry.Value);
						if (segment.Entries.Length == 0 && (startEntry != null || endEntry != null))
						{
							if (startEntry == null) startEntry = new Entry(segmentTimeRange.Range.Start, endEntry.Value.Value);
							if (endEntry == null) endEntry = new Entry(segmentTimeRange.Range.End, startEntry.Value.Value);
						}
					}

					int vertexCount = segment.Entries.Length;

					if (startEntry != null) vertexCount++;
					if (endEntry != null) vertexCount++;

					float[] vertices = new float[vertexCount * 2];
					int position = 0;

					if (startEntry != null)
					{
						vertices[position++] = (float)segmentTimeRange.Mapping.ForwardMap(startEntry.Value.Time.Seconds);
						vertices[position++] = (float)valueRange.Mapping.ForwardMap(startEntry.Value.Value);
					}

					foreach (Entry entry in segment.Entries)
					{
						vertices[position++] = (float)segmentTimeRange.Mapping.ForwardMap(entry.Time.Seconds);
						vertices[position++] = (float)valueRange.Mapping.ForwardMap(entry.Value);
					}

					if (endEntry != null)
					{
						vertices[position++] = (float)segmentTimeRange.Mapping.ForwardMap(endEntry.Value.Time.Seconds);
						vertices[position++] = (float)valueRange.Mapping.ForwardMap(endEntry.Value.Value);
					}

					drawer.DrawLineStrip(vertices, diagram.Layouter.Transformation, Color, (float)diagram.GraphSettings.LineWidth);
				}
			}
		}
	}
}
