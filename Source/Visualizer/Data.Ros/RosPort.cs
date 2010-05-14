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
using System.Runtime.InteropServices;
using System.Linq;
using Data.Ros.Types;
using System.Text.RegularExpressions;
using Utility.Extensions;

namespace Data.Ros
{
	public class RosPort : Port, IDisposable
	{
		readonly RosNode node;
		readonly IntPtr subscriber;

		bool disposed = false;
		Packet currentPacket;

		public RosPort(string topicName, RosNode node) : base(topicName)
		{
			if (node == null) throw new ArgumentNullException("node");
			
			this.node = node;
			this.subscriber = Subscribe(node.Node, topicName, 0x100, MessageReceived);

			Initialize();
		}
		~RosPort()
		{
			Dispose();
		}

		public void Dispose()
		{
			if (!disposed)
			{
				DisposeSubscriber(subscriber);
				
				disposed = true;
			}
		}
		public override Packet Read()
		{
			if (!node.IsRunning) return new InvalidPacket();

			while (currentPacket == null) node.SpinOnce();

			Packet packet = currentPacket;

			currentPacket = null;

			return packet;
		}
		public override void AbortWait()
		{
			throw new NotSupportedException();
		}
		public override string GetName(Path path)
		{
			throw new NotImplementedException();
		}

		void MessageReceived(IntPtr message)
		{
			string dataType = ShapeShifterGetDataType(message);

			string definition = ShapeShifterGetDefinition(message);
			definition = Regex.Replace(definition, @"^(.*?)$", @"  $0", RegexOptions.Multiline);
			definition = Regex.Replace(definition, @"[ \n]*$", string.Empty);

			RosField messageDefinition = RosField.Parse(string.Format("Message {0}\n{1}", dataType, definition));

			byte[] data = new byte[ShapeShifterGetDataLength(message)];
			Marshal.Copy(ShapeShifterGetData(message), data, 0, data.Length);

			currentPacket = messageDefinition.ToPacket(data);
		}

		[DllImport("streamvis-wrappers-ros")]
		static extern IntPtr Subscribe(IntPtr node, string topicName, uint queueLength, Action<IntPtr> callback);
		[DllImport("streamvis-wrappers-ros")]
		static extern void DisposeSubscriber(IntPtr subscriber);
		[DllImport("streamvis-wrappers-ros")]
		static extern string ShapeShifterGetDataType(IntPtr message);
		[DllImport("streamvis-wrappers-ros")]
		static extern string ShapeShifterGetDefinition(IntPtr message);
		[DllImport("streamvis-wrappers-ros")]
		static extern IntPtr ShapeShifterGetData(IntPtr message);
		[DllImport("streamvis-wrappers-ros")]
		static extern int ShapeShifterGetDataLength(IntPtr message);
	}
}
