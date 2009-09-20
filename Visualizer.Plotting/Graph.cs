using System.Drawing;
using Graphics;
using OpenTK.Math;
using Visualizer.Data;
using Visualizer.Plotting.Data;
using Visualizer.Plotting.Timing;
using Visualizer.Plotting.Values;

namespace Visualizer.Plotting
{
	public class Graph
	{
		readonly Layouter layouter;
		readonly ValueManager valueManager;
		readonly SegmentManager segmentManager;
		readonly Drawer drawer;
		readonly DataManager dataManager;

		public bool IsDrawn { get; set; }
		public Color Color { get; set; }
		public DataManager DataManager { get { return dataManager; } }

		public Graph(Layouter layouter, ValueManager valueManager, SegmentManager segmentManager, Drawer drawer, DataManager dataManager)
		{
			this.layouter = layouter;
			this.valueManager = valueManager;
			this.segmentManager = segmentManager;
			this.drawer = drawer;
			this.dataManager = dataManager;

			IsDrawn = true;
		}

		public void Update()
		{
			dataManager.Update();
		}
		public void Draw()
		{
			if (IsDrawn && !dataManager.IsEmpty)
			{
				ValueRange valueRange = valueManager.Range;

				Entry firstEntry = dataManager.FirstEntry;
				Entry lastEntry = dataManager.LastEntry;

				foreach (DataSegment segment in segmentManager[this])
				{
					TimeRange timeRange = segment.TimeRange;

					Entry? startEntry = null;
					Entry? endEntry = null;

					if (segmentManager.ExtendGraphs)
					{
						if (firstEntry.Time - timeRange.Range.Start > 1.5 * dataManager.SampleDistance) startEntry = new Entry(timeRange.Range.Start, firstEntry.Value);
						if (timeRange.Range.End - lastEntry.Time > 1.5 * dataManager.SampleDistance) endEntry = new Entry(timeRange.Range.End, lastEntry.Value);
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

					drawer.DrawLineStrip(vertices, transformation, Color, 1);
				}
			}
		}
	}
}
