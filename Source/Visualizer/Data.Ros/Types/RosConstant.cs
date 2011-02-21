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

namespace Data.Ros.Types
{
	class RosConstant : RosType
	{
		readonly RosType type;

		public RosConstant(RosType type) : base(string.Format("constant<{0}>", type))
		{
			if (type == null) throw new ArgumentNullException("type");

			this.type = type;
		}

		public override Packet ToPacket(Queue<byte> data)
		{
			return new InvalidPacket();
		}

		public override string GetName(Path path)
		{
			if (path.Any()) throw new ArgumentException("Parameter 'path' cannot be non-empty.");

			return string.Empty;
		}
	}
}