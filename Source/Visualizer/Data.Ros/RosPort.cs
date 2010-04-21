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
using System.Runtime.InteropServices;

namespace Data.Ros
{
	public class RosPort : Port, IDisposable
	{
		readonly RosNode node;
		readonly IntPtr subscriber;
		
		bool disposed = false;
		
		public RosPort(string topicName, RosNode node) : base(topicName)
		{
			if (node == null) throw new ArgumentNullException("node");
			
			this.node = node;
			this.subscriber = Subscribe(node.Node, topicName, 0x100, MessageReceived);
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
		public override List Read()
		{
			throw new System.NotImplementedException();
		}
		public override void Write(List list)
		{
			throw new System.NotImplementedException();
		}
		public override void AbortWait()
		{
			throw new System.NotImplementedException();
		}
		
		void MessageReceived(IntPtr message)
		{
		}	

		[DllImport("streamvis-wrappers-ros")]
		static extern IntPtr Subscribe(IntPtr node, string topicName, uint queueLength, Action<IntPtr> callback);
		[DllImport("streamvis-wrappers-ros")]
		static extern void DisposeSubscriber(IntPtr subscriber);
	}
}