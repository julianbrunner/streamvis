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
using System.Linq;
using Krach.Extensions;

namespace Data
{
	public abstract class Packet
	{
		public IEnumerable<Path> ValidPaths { get { return GetValidPaths(Enumerable.Empty<int>(), this); } }

		public Packet GetPacket(Path path)
		{
			if (path == null) throw new ArgumentNullException("path");

			if (path.IsEmpty) return this;

			List list = GetPacket(path.Head) as List;
			int index = path.Tail;

			if (list == null || index < 0 || index >= list.Length) return new InvalidPacket();

			return list[index];
		}
		public double GetValue(Path path)
		{
			if (path == null) throw new ArgumentNullException("path");

			Value value = GetPacket(path) as Value;

			if (value == null) throw new InvalidOperationException(string.Format("Packet \"{0}\" does not have a valid value at path \"{1}\".", this, path));

			return value;
		}

		static IEnumerable<Path> GetValidPaths(IEnumerable<int> root, Packet packet)
		{
			if (packet is Value) yield return new Path(root);
			if (packet is List)
			{
				int index = 0;

				foreach (Packet subPacket in (List)packet)
					foreach (Path subPath in GetValidPaths(root.Append(index++), subPacket))
						yield return subPath;
			}
		}
	}
}
