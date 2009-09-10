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

		public Graph(Plotter plotter, Drawer drawer, EntryData entryData, Color color)
		{
			this.plotter = plotter;
			this.drawer = drawer;
			this.entryData = entryData;

			IsDrawn = true;
			Color = color;
		}

		public void Update() { }
		public void Draw()
		{
			if (IsDrawn)
			{
				Range<double> valueRange = plotter.ValueManager.Range;
				double height = valueRange.End.Value - valueRange.Start.Value;

				foreach (DataSegment segment in plotter.DataManager[entryData])
				{
					Time width = segment.TimeRange.End.Value - segment.TimeRange.Start.Value;

					IEnumerable<PointF> points = from entry in segment.Entries
												 select plotter.Layouter.TransformGraph
												 (
													segment.TimeRange.Map((float)((entry.Time - segment.TimeRange.Start.Value) / width)),
													valueRange.Map((float)((entry.Value - valueRange.Start.Value) / height))
												 );

					drawer.DrawLineStrip(points, Color, 1f);
				}
			}
		}

		//public PointF <Draw>b__0(Entry entry)
		//{
		//    return this.<>4__this.plotter.Layouter.TransformGraph
		//    (
		//        this.CS$<>8__locals4.segment.TimeRange.Map((float) ((entry.Time - this.CS$<>8__locals4.segment.TimeRange.Start.Value) / this.width)),
		//        this.CS$<>8__locals2.valueRange.Map((float) ((entry.Value - this.CS$<>8__locals2.valueRange.Start.Value) / this.CS$<>8__locals2.height))
		//    );
		//}



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
