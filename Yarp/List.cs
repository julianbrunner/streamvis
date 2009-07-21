using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;

namespace Yarp
{
	public class List : Packet, IEnumerable<Packet>
	{
		readonly Packet[] packets;
		
		public Packet this[int index] { get { return packets[index]; } }
		
		public List(params Packet[] packets) : this((IEnumerable<Packet>)packets) { }
		public List(IEnumerable<Packet> packets)
		{
			this.packets = packets.ToArray();
		}
		
		public IEnumerator<Packet> GetEnumerator()
		{
			return (IEnumerator<Packet>)packets.GetEnumerator();
		}
		IEnumerator IEnumerable.GetEnumerator()
		{
			return GetEnumerator();
		}
	}
}
