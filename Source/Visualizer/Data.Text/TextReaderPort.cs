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
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using Krach.Threading;

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

			Initialize();
		}
		public TextReaderPort(string path)
			: base(path)
		{
			this.textReader = new StreamReader(Path);
			this.lines = new Breaker<string>(textReader.ReadLine);

			Initialize();
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
				if (lines != null) lines.Dispose();

				disposed = true;
			}
		}
		public override Packet Read()
		{
			string line = lines.Read();

			if (line == null) return new InvalidPacket();
			if (line.Contains('#')) line = line.Substring(0, line.IndexOf('#'));
			line = line.Trim();
			if (line == string.Empty) return new InvalidPacket();

			return TextToPacket("(" + line + ")");
		}
		public override void AbortWait()
		{
			lines.Break();
		}
		public override string GetName(Path path)
		{
			return null;
		}

		static Packet TextToPacket(string text)
		{
			// Is it a list?
			if (Regex.IsMatch(text, @"^\(.*\)$"))
			{
				// Extract the content
				text = Regex.Match(text, @"^\((.*)\)$").Groups[1].Value;

				return new List
				(
					from part in text.Split(' ', '\t')
					where part != string.Empty
					select TextToPacket(part)
				);
			}

			// Then it must be a value
			try { return new Value(double.Parse(text)); }
			catch (FormatException)
			{
				Console.WriteLine("Text \"{0}\" could not be parsed.", text);
				return new InvalidPacket();
			}
		}
	}
}
