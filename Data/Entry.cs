using System;

namespace Data
{
	public class Entry
	{
		readonly Time time;
		readonly double value;

		public Time Time { get { return time; } }
		public double Value { get { return value; } }
		
		public Entry(Time time, double value)
		{
			this.time = time;
			this.value = value;
		}
	}
}