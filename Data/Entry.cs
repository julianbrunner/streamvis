using System;
using Data.Searching;

namespace Data
{
	public class Entry : IPositioned<Time>
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
		
		Time IPositioned<Time>.Position { get { return Time; } }
	}
}