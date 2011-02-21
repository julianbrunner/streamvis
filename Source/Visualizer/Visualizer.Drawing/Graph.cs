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

using System.Drawing;
using Graphics;
using Visualizer.Data;
using Visualizer.Drawing.Data;
using Krach.Maps.Scalar;

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
			SymmetricRangeMap timeMapping = diagram.TimeManager.Mapping;
			SymmetricRangeMap valueMapping = diagram.ValueManager.Mapping;

			if (IsDrawn && !streamManager.EntryCache.IsEmpty && !timeMapping.Source.IsEmpty && !valueMapping.Source.IsEmpty)
			{
				Entry firstEntry = streamManager.EntryCache.FirstEntry;
				Entry lastEntry = streamManager.EntryCache.LastEntry;

				foreach (DataSegment segment in streamManager.Segments)
				{
					SymmetricRangeMap segmentTimeMapping = segment.TimeMapping;

					Entry? startEntry = null;
					Entry? endEntry = null;

					if (diagram.GraphSettings.ExtendGraphs)
					{
						if (firstEntry.Time - segmentTimeMapping.Source.Start > 1.5 * streamManager.EntryResampler.SampleDistance) startEntry = new Entry(segmentTimeMapping.Source.Start, firstEntry.Value);
						if (segmentTimeMapping.Source.End - lastEntry.Time > 1.5 * streamManager.EntryResampler.SampleDistance) endEntry = new Entry(segmentTimeMapping.Source.End, lastEntry.Value);
						if (segment.Entries.Length == 0 && (startEntry != null || endEntry != null))
						{
							if (startEntry == null) startEntry = new Entry(segmentTimeMapping.Source.Start, endEntry.Value.Value);
							if (endEntry == null) endEntry = new Entry(segmentTimeMapping.Source.End, startEntry.Value.Value);
						}
					}

					int vertexCount = segment.Entries.Length;

					if (startEntry != null) vertexCount++;
					if (endEntry != null) vertexCount++;

					float[] vertices = new float[vertexCount * 2];
					int position = 0;

					if (startEntry != null)
					{
						vertices[position++] = (float)segmentTimeMapping.Forward.Map(startEntry.Value.Time);
						vertices[position++] = (float)valueMapping.Forward.Map(startEntry.Value.Value);
					}

					foreach (Entry entry in segment.Entries)
					{
						vertices[position++] = (float)segmentTimeMapping.Forward.Map(entry.Time);
						vertices[position++] = (float)valueMapping.Forward.Map(entry.Value);
					}

					if (endEntry != null)
					{
						vertices[position++] = (float)segmentTimeMapping.Forward.Map(endEntry.Value.Time);
						vertices[position++] = (float)valueMapping.Forward.Map(endEntry.Value.Value);
					}

					drawer.DrawLineStrip(vertices, diagram.Layouter.Transformation, Color, (float)diagram.GraphSettings.LineWidth);
				}
			}
		}
	}
}
