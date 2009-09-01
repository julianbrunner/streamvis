using System;
using System.Linq;
using System.Collections.Generic;
using Extensions;

namespace Data
{
	public class EntryCache
	{
		readonly EntryResampler source;
		//readonly List<Entry> buffer = new List<Entry>();

		public IEnumerable<Entry> this[Time startTime, Time endTime]
		{
			get
			{
				return source[startTime, endTime];
			}
		}
		
		public EntryCache(EntryResampler source)
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
