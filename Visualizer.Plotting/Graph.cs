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
			if (IsDrawn)
			{
				ValueRange valueRange = valueManager.Range;

				foreach (DataSegment segment in segmentManager[this])
					if (segment.Entries.Length > 0)
					{
						TimeRange timeRange = segment.TimeRange;

						int vertexCount = segment.Entries.Length;

						if (segmentManager.ExtendGraphs) vertexCount += 2;

						float[] vertices = new float[vertexCount * 2];
						int position = 0;

						if (segmentManager.ExtendGraphs)
						{
							vertices[position++] = (float)timeRange.Range.Start.Seconds;
							vertices[position++] = (float)segment.Entries[0].Value;
						}

						foreach (Entry entry in segment.Entries)
						{
							vertices[position++] = (float)entry.Time.Seconds;
							vertices[position++] = (float)entry.Value;
						}

						if (segmentManager.ExtendGraphs)
						{
							vertices[position++] = (float)timeRange.Range.End.Seconds;
							vertices[position++] = (float)segment.Entries[segment.Entries.Length - 1].Value;
						}

						Matrix4 transformation = valueRange.Transformation * timeRange.Transformation * layouter.Transformation;

						drawer.DrawLineStrip(vertices, transformation, Color, 1);
					}
			}
		}
	}
}
