using System.Collections.Generic;
using System.Linq;
using Visualizer.Data;

namespace Visualizer.Plotting
{
	public class SimpleDataManager : DataManager
	{
		Dictionary<Stream, IEnumerable<DataSegment>> segments = new Dictionary<Stream, IEnumerable<DataSegment>>();

		public override IEnumerable<DataSegment> this[Stream stream] { get { return segments[stream]; } }

		public SimpleDataManager(TimeManager timeManager, IEnumerable<Stream> streams) : base(timeManager, streams) { }

		public override void Update()
		{
			foreach (Stream stream in Streams)
			{
				segments[stream] =
				(
				    from timeRange in TimeManager.GraphRanges
				    select new DataSegment(timeRange, stream.Container[timeRange.Start.Value, timeRange.End.Value])
				)
				.ToArray();
			}
		}
	}
}
