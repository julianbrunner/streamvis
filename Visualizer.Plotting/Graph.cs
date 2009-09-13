using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using Graphics;
using Graphics.Transformations;
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

//					IEnumerable<PointF> points = from entry in segment.Entries
//												 select layouter.TransformGraph
//												 (
//													timeRange.Map((float)((entry.Time - startTime) / width)),
//													valueRange.Map((float)((entry.Value - startValue) / height))
//												 );
//					
//					drawer.DrawLineStrip(points, Color, 1f);
					
					IEnumerable<PointF> points = from entry in segment.Entries
												 select new PointF((float)entry.Time.Seconds, (float)entry.Value);
					
					Transformation entryTranslation = new Translation(new PointF((float)-startTime.Seconds, (float)-startValue));
					Transformation entryScaling = new Scaling(new PointF((float)(1.0 / width.Seconds), (float)(1.0 / height)));
					
					Transformation rangeScaling = new Scaling(new PointF(timeRange.End.Position - timeRange.Start.Position, valueRange.End.Position - valueRange.Start.Position));
					Transformation rangeTranslation = new Translation(new PointF(timeRange.Start.Position, valueRange.Start.Position));
					
					Transformation layouterScaling = new Scaling(new PointF(layouter.GraphsArea.Width, -layouter.GraphsArea.Height));
					Transformation layouterTranslation = new Translation(new PointF(layouter.GraphsArea.Left, layouter.GraphsArea.Bottom));
					
					IEnumerable<Transformation> transformations = new[]
					{
						entryTranslation,
						entryScaling,
						rangeScaling,
						rangeTranslation,
						layouterScaling,
						layouterTranslation
					};
					
					drawer.DrawLineStrip(points, transformations, Color, 1f);
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
