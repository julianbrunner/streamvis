// Copyright © Julian Brunner 2009 - 2010

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
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using Utility.Extensions;

namespace Data.Text
{
	public class TextWriterPort : TextPort, IDisposable
	{
		TextWriter textWriter;
		bool disposed = false;

		public TextWriterPort()
		{
			textWriter = Console.Out;
		}
		public TextWriterPort(string path)
			: base(path)
		{
			textWriter = new StreamWriter(Path, true);
		}
		~TextWriterPort()
		{
			Dispose();
		}

		public virtual void Dispose()
		{
			if (!disposed)
			{
				textWriter.Dispose();
				
				disposed = true;
			}
		}
		public override List Read()
		{
			throw new NotSupportedException();
		}
		public override void Write(List list)
		{
			IEnumerable<string> tuple =
			(
				 from subPacket in list
				 select PacketToString(subPacket)
			);

			textWriter.WriteLine(tuple.Separate(" ").AggregateString());
		}
		public override void AbortWait()
		{
			throw new NotSupportedException();
		}

		static string PacketToString(Packet packet)
		{
			if (packet is List)
			{
				IEnumerable<string> tuple =
				(
					from subPacket in (List)packet
					select PacketToString(subPacket)
				);

				return "(" + tuple.Separate(" ").AggregateString() + ")";
			}
			if (packet is Value)
			{
				double value = (Value)packet;

				return value.ToString(CultureInfo.InvariantCulture);
			}

			throw new ArgumentException("packet");
		}
	}
}
