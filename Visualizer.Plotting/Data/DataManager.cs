using Utility;
using Visualizer.Data;

namespace Visualizer.Plotting.Data
{
	public abstract class DataManager
	{
		readonly EntryData entryData;
		readonly EntryResampler entryResampler;
		readonly EntryCache entryCache;

		public Entry[] this[Range<Time> range] { get { return entryCache[range]; } }

		protected EntryData EntryData { get { return entryData; } }
		protected EntryResampler EntryResampler { get { return entryResampler; } }
		protected EntryCache EntryCache { get { return entryCache; } }

		public Time SampleDistance { get { return entryResampler.SampleDistance; } }
		public bool IsEmpty { get { return entryCache.IsEmpty; } }
		public Entry FirstEntry { get { return entryCache.FirstEntry; } }
		public Entry LastEntry { get { return entryCache.LastEntry; } }

		protected DataManager(EntryData entryData)
		{
			this.entryData = entryData;
			this.entryResampler = new EntryResampler(entryData.Entries);
			this.entryCache = new EntryCache(entryResampler);
		}

		public virtual void Update()
		{
			entryData.Update();
		}
	}
}
