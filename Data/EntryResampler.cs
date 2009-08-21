using System.Collections.Generic;

namespace Data
{
	public class EntryResampler : Transformation<Entry, Time>
	{
		public override IEnumerable<Entry> this[Time startTime, Time endTime]
		{
			get
			{
				throw new System.NotImplementedException ();
			}
		}

	}
}
