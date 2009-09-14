using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Yarp
{
	public class List : Packet, IEnumerable<Packet>
	{
		readonly Packet[] packets;
		
		public Packet this[int index] { get { return packets[index]; } }

		public List()
		{
			this.packets = new Packet[0];
		}
		public List(IEnumerable<Packet> packets)
		{
			this.packets = packets.ToArray();
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
