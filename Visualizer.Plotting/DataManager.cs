using System.Collections.Generic;
using Visualizer.Data;

namespace Visualizer.Plotting
{
	public abstract class DataManager
	{
		readonly TimeManager timeManager;
		readonly IEnumerable<Stream> streams;

		protected TimeManager TimeManager { get { return timeManager; } }
		protected IEnumerable<Stream> Streams { get { return streams; } }

		public abstract IEnumerable<DataSegment> this[Stream stream] { get; }

		protected DataManager(TimeManager timeManager, IEnumerable<Stream> streams)
		{
			this.timeManager = timeManager;
			this.streams = streams;
		}

		public abstract void Update();
	}
}
