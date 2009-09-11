using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using Graphics;
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
				Layouter layouter = plotter.Layouter;

				Range<double> valueRange = plotter.ValueManager.Range;

				double startValue = valueRange.Start.Value;
				double endValue = valueRange.End.Value;
				double height = endValue - startValue;

				foreach (DataSegment segment in plotter.DataManager[entryData])
				{
					Range<Time> timeRange = segment.TimeRange;

					Time startTime = segment.TimeRange.Start.Value;
					Time endTime = segment.TimeRange.End.Value;
					Time width = endTime - startTime;

					IEnumerable<PointF> points = from entry in segment.Entries
												 select layouter.TransformGraph
												 (
													timeRange.Map((float)((entry.Time - startTime) / width)),
													valueRange.Map((float)((entry.Value - startValue) / height))
												 );

					drawer.DrawLineStrip(points, Color, 1f);
				}
			}
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
