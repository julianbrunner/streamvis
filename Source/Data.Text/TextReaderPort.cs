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
using System.IO;
using System.Linq;
using Utility;

namespace Data.Text
{
	public class TextReaderPort : TextPort, IDisposable
	{
		readonly TextReader textReader;
		readonly Breaker<string> lines;

		bool disposed = false;

		public TextReaderPort()
		{
			this.textReader = Console.In;
			this.lines = new Breaker<string>(textReader.ReadLine);
		}
		public TextReaderPort(string path)
			: base(path)
		{
			this.textReader = new StreamReader(Path);
			this.lines = new Breaker<string>(textReader.ReadLine);
		}
		~TextReaderPort()
		{
			Dispose();
		}

		public virtual void Dispose()
		{
			if (!disposed)
			{
				if (textReader != null) textReader.Dispose();
				if (textReader != null) lines.Dispose();

				disposed = true;
			}
		}
		public override List Read()
		{
			string line = lines.Read();

			if (line == null) return null;

			if (line.Contains('#')) line = line.Substring(0, line.IndexOf('#'));
			line = line.Trim();

			if (line == string.Empty) return null;

			return new List
			(
				 from part in line.Split(' ', '\t')
				 where part != string.Empty
				 select StringToPacket(part)
			);
		}
		public override void Write(List list)
		{
			throw new NotSupportedException();
		}
		public override void AbortWait()
		{
			lines.Break();
		}

		static Packet StringToPacket(string item)
		{
			if (item[0] == '(' && item[item.Length - 1] == ')')
			{
				item = item.Substring(1, item.Length - 2);

				return new List(from part in item.Split(' ') select StringToPacket(part));
			}

			double value;
			if (double.TryParse(item, out value)) return new Value(value);

			throw new ArgumentException("item");
		}
	}
}
