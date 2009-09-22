// Copyright © Julian Brunner 2009

// This file is part of Stream Visualizer (streamvis).
// 
// Stream Visualizer is free software: you can redistribute it and/or modify
// it under the terms of the GNU General Public License as published by
// the Free Software Foundation, either version 3 of the License, or
// (at your option) any later version.
// 
// Stream Visualizer is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU General Public License for more details.
// 
// You should have received a copy of the GNU General Public License
// along with Stream Visualizer.  If not, see <http://www.gnu.org/licenses/>.

using System;

namespace Visualizer.Data
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
		public override int GetHashCode()
		{
			return ticks.GetHashCode();
		}
		public override string ToString()
		{
			return Seconds.ToString();
		}
		public Time Floor(Time interval, Time offset)
		{
			Time remainder = (this - offset) % interval;
			return remainder == Zero ? this : this - remainder + 0 * interval;
		}
		public Time Ceiling(Time interval, Time offset)
		{
			Time remainder = (this - offset) % interval;
			return remainder == Zero ? this : this - remainder + 1 * interval;
		}

		public static Time Min(Time a, Time b)
		{
			return a > b ? b : a;
		}
		public static Time Max(Time a, Time b)
		{
			return a < b ? b : a;
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
			return a.Seconds / b.Seconds;
		}

		public static Time operator %(Time a, Time b)
		{
			long remainder = a.ticks % b.ticks;
			if (remainder < 0) remainder += b.ticks;
			return new Time(remainder);
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
