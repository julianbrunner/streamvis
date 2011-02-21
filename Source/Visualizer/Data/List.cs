// Copyright © Julian Brunner 2009 - 2011

// This file is part of Stream Visualizer (streamvis).
// 
// Stream Visualizer is free software: you can redistribute it and/or modify it
// under the terms of the GNU General Public License as published by the Free
// Software Foundation, either version 3 of the License, or (at your option) any
// later version.
// 
// Stream Visualizer is distributed in the hope that it will be useful, but
// WITHOUT ANY WARRANTY; without even the implied warranty of MERCHANTABILITY or
// FITNESS FOR A PARTICULAR PURPOSE. See the GNU General Public License for more
// details.
// 
// You should have received a copy of the GNU General Public License along with
// Stream Visualizer. If not, see <http://www.gnu.org/licenses/>.

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Krach.Extensions;

namespace Data
{
	public class List : Packet, IEnumerable<Packet>
	{
		readonly Packet[] packets;

		public Packet this[int index] { get { return packets[index]; } }
		public int Length { get { return packets.Length; } }

		public List()
		{
			this.packets = new Packet[0];
		}
		public List(IEnumerable<Packet> packets)
		{
			if (packets == null) throw new ArgumentNullException("packets");

			this.packets = packets.ToArray();
		}

		public override string ToString()
		{
			return packets.ToStrings().Separate(" ").AggregateString();
		}
		public IEnumerator<Packet> GetEnumerator()
		{
			return ((IEnumerable<Packet>)packets).GetEnumerator();
		}

		IEnumerator IEnumerable.GetEnumerator()
		{
			return GetEnumerator();
		}
	}
}
