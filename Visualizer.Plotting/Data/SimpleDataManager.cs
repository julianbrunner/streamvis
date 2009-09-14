using System.Collections.Generic;
using System.Linq;
using Visualizer.Data;

namespace Visualizer.Plotting
{
	public class SimpleDataManager : DataManager
	{
		Dictionary<EntryData, IEnumerable<DataSegment>> segments = new Dictionary<EntryData, IEnumerable<DataSegment>>();

		public override IEnumerable<DataSegment> this[EntryData entryData] { get { return segments[entryData]; } }

		public SimpleDataManager(TimeManager timeManager, IEnumerable<EntryData> entryData) : base(timeManager, entryData) { }

		public override void Update()
		{
			foreach (EntryData entryData in EntryData)
			{
				segments[entryData] =
				(
					from timeRange in TimeManager.GraphRanges
					select new DataSegment(timeRange, entryData[timeRange.Range.Start, timeRange.Range.End])
				)
				.ToArray();
			}
		}
	}
}
