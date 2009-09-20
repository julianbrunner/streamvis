using Utility;
using Visualizer.Data;
using System;

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
		// TODO: Move these to EntryCache?
		public bool IsEmpty { get { return entryCache.Entries.Count == 0; } }
		public Entry FirstEntry
		{
			get
			{
				if (entryCache.Entries.Count == 0) throw new InvalidOperationException();

				return entryCache.Entries[0];
			}
		}
		public Entry LastEntry
		{
			get
			{
				if (entryCache.Entries.Count == 0) throw new InvalidOperationException();

				return entryCache.Entries[entryCache.Entries.Count - 1];
			}
		}

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
