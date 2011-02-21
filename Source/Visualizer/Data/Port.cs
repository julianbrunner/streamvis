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
using System.Collections.Generic;
using System.Linq;
using Krach.Extensions;

namespace Data
{
	public abstract class Port
	{
		readonly string name;

		protected Packet Sample { get; private set; }

		public string Name { get { return name; } }
		public IEnumerable<Path> ValidPaths { get { return Sample.ValidPaths; } }

		protected Port(string name)
		{
			if (name == null) throw new ArgumentNullException("name");

			this.name = name;
		}

		public abstract Packet Read();
		public abstract void AbortWait();
		public abstract string GetName(Path path);

		protected void Initialize()
		{
			Sample = Enumerables.Consume<Packet>(Read).First(packet => packet != null && !(packet is InvalidPacket));
		}
	}
}
