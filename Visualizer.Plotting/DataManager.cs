using System.Collections.Generic;
using Visualizer.Data;

namespace Visualizer.Plotting
{
	public abstract class DataManager
	{
		readonly TimeManager timeManager;
		readonly IEnumerable<EntryData> entryData;

		protected TimeManager TimeManager { get { return timeManager; } }
		protected IEnumerable<EntryData> EntryData { get { return entryData; } }

		public abstract IEnumerable<DataSegment> this[EntryData entryData] { get; }

		protected DataManager(TimeManager timeManager, IEnumerable<EntryData> entryData)
		{
			this.timeManager = timeManager;
			this.entryData = entryData;
		}

		public abstract void Update();
	}
}
