using System;

namespace Data
{
	public struct Time : IEquatable<Time>, IComparable, IComparable<Time>
	{
		readonly long ticks;
		
		public static Time Zero { get { return new Time(); } }
		
		public long Ticks { get { return ticks; } }
		public double Seconds { get { return ticks / 10000000.0; } }
		
		public Time(long ticks)
		{
			this.ticks = ticks;
		}
		public Time(double seconds) : this((long)(seconds * 10000000)) { }
		
		public override bool Equals(object obj)
		{
			return obj is Time ? Equals((Time)obj) : false;
		}
		public bool Equals(Time other)
		{
			return Equals(this, other);
		}
		public int CompareTo(object obj)
		{
			return obj is Time ? CompareTo((Time)obj) : 0;
		}
		public int CompareTo(Time other)
		{
			return Compare(this, other);
		}
		public override int GetHashCode ()
		{
			return ticks.GetHashCode();
		}
		public override string ToString()
		{
			return Seconds.ToString();
		}
		
		public static bool operator ==(Time a, Time b)
		{
			return Equals(a, b);
		}
		public static bool operator !=(Time a, Time b)
		{
			return !Equals(a, b);
		}
		
		public static bool operator <(Time a, Time b)
		{
			return Compare(a, b) < 0;
		}
		public static bool operator <=(Time a, Time b)
		{
			return Compare(a, b) <= 0;
		}
		public static bool operator >(Time a, Time b)
		{
			return Compare(a, b) > 0;
		}
		public static bool operator >=(Time a, Time b)
		{
			return Compare(a, b) >= 0;
		}
		
		public static Time operator +(Time a, Time b)
		{
			return new Time(a.ticks + b.ticks);
		}
		public static Time operator -(Time a, Time b)
		{
			return new Time(a.ticks - b.ticks);
		}
		public static Time operator *(Time a, double d)
		{
			return new Time((long)(a.ticks * d));
		}
		public static Time operator *(double d, Time a)
		{
			return new Time((long)(d * a.ticks));
		}
		public static Time operator /(Time a, double d)
		{
			return new Time((long)(a.ticks / d));
		}
		public static double operator /(Time a, Time b)
		{
			return (double)a.ticks / (double)b.ticks;
		}
		
		public static Time operator %(Time a, Time b)
		{
			long rest = a.ticks % b.ticks;
			if (rest < 0) rest += b.ticks;
			return new Time(rest);
		}
		
		static bool Equals(Time a, Time b)
		{
			return a.ticks == b.ticks;
		}
		static int Compare(Time a, Time b)
		{
			long difference = a.ticks - b.ticks;
			
			if (difference < int.MinValue) difference = int.MinValue;
			if (difference > int.MaxValue) difference = int.MaxValue;
			
			return (int)difference;
		}
	}
}
