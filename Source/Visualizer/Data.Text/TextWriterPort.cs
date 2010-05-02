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
using System.Text.RegularExpressions;

namespace Data.Text
{
	public class TextWriterPort : TextPort, IDisposable
	{
		readonly TextWriter textWriter;

		bool disposed = false;

		public TextWriterPort()
		{
			this.textWriter = Console.Out;
		}
		public TextWriterPort(string path) : base(path)
		{
			this.textWriter = new StreamWriter(Path, true);
		}
		~TextWriterPort()
		{
			Dispose();
		}

		public virtual void Dispose()
		{
			if (!disposed)
			{
				if (textWriter != null) textWriter.Dispose();

				disposed = true;
			}
		}
		public override Packet Read()
		{
			throw new NotSupportedException();
		}
		public override void Write(Packet packet)
		{
			if (packet is Value) throw new ArgumentException("Cannot directly write a value to a text port.");
			if (packet is List) textWriter.WriteLine(Regex.Match(PacketToText(packet), @"^\((.*)\)$").Groups[1].Value);
		}
		public override void AbortWait()
		{
			throw new NotSupportedException();
		}

		static string PacketToText(Packet packet)
		{
			if (packet is Value) return ((double)(Value)packet).ToString(CultureInfo.InvariantCulture);
			if (packet is List)
			{
				IEnumerable<string> list =
				(
					from subPacket in (List)packet
					select PacketToText(subPacket)
				);

				return "(" + list.Separate(" ").AggregateString() + ")";
			}

			return string.Empty;
		}
	}
}
