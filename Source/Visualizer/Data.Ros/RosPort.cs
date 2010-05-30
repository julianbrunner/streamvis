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
using System.Threading;

namespace Data.Ros
{
	public class RosPort : Port, IDisposable
	{
		readonly AutoResetEvent packetAvailable;
		readonly IntPtr subscriber;

		bool disposed = false;
		RosField sampleDefinition;
		Packet currentPacket;

		public RosPort(string topicName, RosNetwork network) : base(topicName)
		{
			if (network == null) throw new ArgumentNullException("network");
			
			this.packetAvailable = new AutoResetEvent(false);
			this.subscriber = CreateSubscriber(network.Node, topicName, 0x100, MessageReceived);

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
				packetAvailable.Close();
				DisposeSubscriber(subscriber);
				
				disposed = true;
			}
		}
		public override Packet Read()
		{
			packetAvailable.WaitOne();
			
			if (currentPacket == null) return new InvalidPacket();

			return currentPacket;
		}
		public override void AbortWait()
		{
			throw new NotSupportedException();
		}
		public override string GetName(Path path)
		{
			return sampleDefinition.GetName(path);
		}

		void MessageReceived(IntPtr message)
		{
			if (sampleDefinition == null)
			{
				string dataType = ShapeShifterGetDataType(message);

				string definition = ShapeShifterGetDefinition(message);
				definition = Regex.Replace(definition, @"^(.*?)$", @"  $0", RegexOptions.Multiline);
				definition = Regex.Replace(definition, @"[ \n]*$", string.Empty);

				sampleDefinition = RosField.Parse(string.Format("Message {0}\n{1}", dataType, definition));
			}

			byte[] data = new byte[ShapeShifterGetDataLength(message)];
			Marshal.Copy(ShapeShifterGetData(message), data, 0, data.Length);

			currentPacket = sampleDefinition.ToPacket(data);
			packetAvailable.Set();
		}

		[DllImport("streamvis-wrappers-ros")]
		static extern IntPtr CreateSubscriber(IntPtr node, string topicName, uint queueLength, Action<IntPtr> callback);
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