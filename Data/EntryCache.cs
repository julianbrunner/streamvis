using System;
using System.Linq;
using System.Collections.Generic;
using Extensions;

namespace Data
{
	public class EntryCache : IRanged<Entry, Time>
	{
		readonly IRanged<Entry, Time> source;
		readonly List<Entry> buffer = new List<Entry>();

		public IEnumerable<Entry> this[Time startTime, Time endTime]
		{
			get
			{
				throw new System.NotImplementedException ();
			}
		}

		
		public EntryCache(IRanged<Entry, Time> source)
		{
			this.source = source;
		}
		
//		public void Refresh()
//		{
//			while (true)
//			{
//				Time startTime = buffer.Count == 0 ? Time.Zero : buffer[buffer.Count - 1].Time + 0.5 * sampleDistance;
//				
//				if (startTime + sampleDistance > source[source.Count - 1].Time) break;
//				
//				buffer.Add(Aggregate(source, startTime, startTime + sampleDistance));
//			}
//		}
	}
}
