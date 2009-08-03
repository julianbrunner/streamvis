using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using Visualizer.Data;

namespace Visualizer.Plotting
{
	public class Graph
	{
		readonly Plotter plotter;
		readonly Stream stream;

		public bool IsUpdated { get; set; }
		public bool IsDrawn { get; set; }
		public IEnumerable<double> Values
		{
			get
			{
				return from timeRange in plotter.TimeManager.GraphRanges
					   from entry in GetEntries(timeRange.Start.Value, timeRange.End.Value)
					   select entry.Value;
			}
		}

		public Graph(Plotter plotter, Stream stream)
		{
			this.plotter = plotter;
			this.stream = stream;

			IsUpdated = true;
			IsDrawn = true;
		}

		public virtual void Update()
		{
			if (IsUpdated)
			{

			}
		}
		public virtual void Draw()
		{
			if (IsDrawn && !stream.Container.IsEmpty)
			{
				Range<double> valueRange = plotter.ValueManager.Range;
				double height = valueRange.End.Value - valueRange.Start.Value;

				foreach (Range<long> timeRange in plotter.TimeManager.GraphRanges)
				{
					double width = timeRange.End.Value - timeRange.Start.Value;

					IEnumerable<PointF> points =
					(
						from entry in GetEntries(timeRange.Start.Value, timeRange.End.Value)
						select plotter.Layouter.TransformGraph
						(
							timeRange.Map((float)((entry.Time - timeRange.Start.Value) / width)),
							valueRange.Map((float)((entry.Value - valueRange.Start.Value) / height))
						)
					);

					plotter.Viewport.DrawLineStrip(points, stream.Color, 1.5f);
				}
			}
		}

		// TODO: Change longs back to DateTime if possible
		IEnumerable<Entry> GetEntries(long start, long end)
		{
			if (stream.Container.IsEmpty) yield break;

			int startIndex = stream.Container.GetIndex(start);
			int endIndex = stream.Container.GetIndex(end);

			if (plotter.ExtendGraphs)
			{
				double head;
				double tail;

				if (startIndex == endIndex)
				{
					if (endIndex == stream.Container.Count) 
						head = tail = stream.Container[endIndex - 1].Value;
					else
						head = tail = stream.Container[startIndex].Value;
				}
				else
				{
					head = stream.Container[startIndex].Value;
					tail = stream.Container[endIndex - 1].Value;
				}

				yield return new Entry(start, head);
				foreach (Entry entry in stream.Container[startIndex, endIndex]) yield return entry;
				yield return new Entry(end, tail);
			}
			else
				foreach (Entry entry in stream.Container[startIndex, endIndex])
					yield return entry;
		}
	}
}
