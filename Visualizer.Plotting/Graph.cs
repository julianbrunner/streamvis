using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using Graphics;
using OpenTK.Math;
using Visualizer.Data;

namespace Visualizer.Plotting
{
	public class Graph
	{
		readonly Plotter plotter;
		readonly Drawer drawer;
		readonly EntryData entryData;

		public bool IsDrawn { get; set; }
		public Color Color { get; set; }
		public EntryData EntryData { get { return entryData; } }

		public Graph(Plotter plotter, Drawer drawer, EntryData entryData)
		{
			this.plotter = plotter;
			this.drawer = drawer;
			this.entryData = entryData;

			IsDrawn = true;
		}

		public void Update() { }
		public void Draw()
		{
			if (IsDrawn)
			{
				ValueRange valueRange = plotter.ValueManager.Range;

				foreach (DataSegment segment in plotter.DataManager[entryData])
				{
					TimeRange timeRange = segment.TimeRange;

					Matrix4 transformation = valueRange.Transformation * timeRange.Transformation * plotter.Layouter.Transformation;

					drawer.DrawLineStrip(GetVertices(segment.Entries), transformation, Color, 1f);
				}
			}
		}

		static float[] GetVertices(IEnumerable<Entry> entries)
		{
			int position = 0;
			float[] vertices = new float[entries.Count() * 2];

			// TODO: Does an array foreach improve performance significantly?
			foreach (Entry entry in entries)
			{
				vertices[position++] = (float)entry.Time.Seconds;
				vertices[position++] = (float)entry.Value;
			}

			return vertices;
		}

		// TODO: Reenable graph extension
		//IEnumerable<Entry> GetEntries(Time startTime, Time endTime)
		//{
		//    if (stream.Container.IsEmpty) yield break;

		//    int startIndex = stream.Container.GetIndex(start);
		//    int endIndex = stream.Container.GetIndex(end);

		//    if (plotter.ExtendGraphs)
		//    {
		//        double head;
		//        double tail;

		//        if (startIndex == endIndex)
		//        {
		//            if (endIndex == stream.Container.Count) head = tail = stream.Container[endIndex - 1].Value;
		//            else head = tail = stream.Container[startIndex].Value;
		//        }
		//        else
		//        {
		//            head = stream.Container[startIndex].Value;
		//            tail = stream.Container[endIndex - 1].Value;
		//        }

		//        yield return new Entry(start, head);
		//        foreach (Entry entry in stream.Container[startIndex, endIndex]) yield return entry;
		//        yield return new Entry(end, tail);
		//    }
		//    else
		//        foreach (Entry entry in stream.Container[startIndex, endIndex])
		//            yield return entry;
		//}
	}
}
