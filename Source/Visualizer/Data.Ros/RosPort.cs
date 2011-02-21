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
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Text.RegularExpressions;
using System.Threading;
using Data.Ros.Types;

namespace Data.Ros
{
	public class RosPort : Port, IDisposable
	{
		readonly AutoResetEvent packetAvailable;
		readonly Action<IntPtr> messageReceived;
		readonly IntPtr subscriber;

		bool disposed = false;
		RosField sampleDefinition;
		Packet currentPacket;

		public RosPort(string topicName, RosNetwork network)
			: base(topicName)
		{
			if (network == null) throw new ArgumentNullException("network");

			this.packetAvailable = new AutoResetEvent(false);
			this.messageReceived = MessageReceived;
			this.subscriber = CreateSubscriber(network.Node, topicName, 0x100, messageReceived);

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
			currentPacket = null;

			packetAvailable.Set();
		}
		public override string GetName(Path path)
		{
			return sampleDefinition.GetName(path);
		}

		void MessageReceived(IntPtr message)
		{
			if (disposed) return;

			if (sampleDefinition == null)
			{
				string dataType = ShapeShifterGetDataType(message);
				string definition = null;

				using (Process rosmsg = new Process())
				{
					rosmsg.StartInfo.FileName = "rosmsg";
					rosmsg.StartInfo.Arguments = string.Format("show {0}", dataType);
					rosmsg.StartInfo.UseShellExecute = false;
					rosmsg.StartInfo.RedirectStandardOutput = true;

					rosmsg.Start();
					definition = rosmsg.StandardOutput.ReadToEnd();
					rosmsg.WaitForExit();
				}

				// Add indentation to each line
				definition = Regex.Replace(definition, @"^(.*?)$", @"  $0", RegexOptions.Multiline);
				// Remove whitespaces at the end of a line
				definition = Regex.Replace(definition, @"[ \n]*$", string.Empty);
				// Add header
				definition = string.Format("Message {0}\n{1}", dataType, definition);

				sampleDefinition = RosField.Parse(definition);
			}

			byte[] data = new byte[ShapeShifterGetDataLength(message)];
			Marshal.Copy(ShapeShifterGetData(message), data, 0, data.Length);

			currentPacket = sampleDefinition.BinaryToPacket(data);

			packetAvailable.Set();
		}

		[DllImport("streamvis-wrappers-ros")]
		static extern IntPtr CreateSubscriber(IntPtr node, string topicName, uint queueLength, Action<IntPtr> callback);
		[DllImport("streamvis-wrappers-ros")]
		static extern void DisposeSubscriber(IntPtr subscriber);

		[DllImport("streamvis-wrappers-ros")]
		static extern string ShapeShifterGetDataType(IntPtr message);
		[DllImport("streamvis-wrappers-ros")]
		static extern IntPtr ShapeShifterGetData(IntPtr message);
		[DllImport("streamvis-wrappers-ros")]
		static extern int ShapeShifterGetDataLength(IntPtr message);
	}
}
