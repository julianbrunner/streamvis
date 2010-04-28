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
	public class RosNode : IDisposable
	{
		readonly IntPtr node;
		
		bool disposed = false;
		
		public IntPtr Node { get { return node; } }
		public bool IsRunning { get { return RosOk(); } }
		
		public RosNode()
		{
			//this.node = InitializeNode();
			
			string text = @"dynamic_reconfigure/ParamDescription[] parameters
  string name
  string type
  uint32 level
  string description
  string edit_method
dynamic_reconfigure/Config max
  dynamic_reconfigure/BoolParameter[] bools
    string name
    bool value
  dynamic_reconfigure/IntParameter[] ints
    string name
    int32 value
  dynamic_reconfigure/StrParameter[] strs
    string name
    string value
  dynamic_reconfigure/DoubleParameter[] doubles
    string name
    float64 value
dynamic_reconfigure/Config min
  dynamic_reconfigure/BoolParameter[] bools
    string name
    bool value
  dynamic_reconfigure/IntParameter[] ints
    string name
    int32 value
  dynamic_reconfigure/StrParameter[] strs
    string name
    string value
  dynamic_reconfigure/DoubleParameter[] doubles
    string name
    float64 value
dynamic_reconfigure/Config dflt
  dynamic_reconfigure/BoolParameter[] bools
    string name
    bool value
  dynamic_reconfigure/IntParameter[] ints
    string name
    int32 value
  dynamic_reconfigure/StrParameter[] strs
    string name
    string value
  dynamic_reconfigure/DoubleParameter[] doubles
    string name
    float64 value";
			text = text.Replace("\r\n", "\n");
			
			RosType a = RosType.Parse("TestType Test", text);
			Console.WriteLine(a);
		}
		~RosNode()
		{
			Dispose();
		}
		
		public void Dispose()
		{
			if (!disposed)
			{
				DisposeNode(node);
				
				disposed = true;
			}
		}
		public void SpinOnce()
		{
			RosSpinOnce();
		}
		
		[DllImport("streamvis-wrappers-ros")]
		static extern IntPtr InitializeNode();
		[DllImport("streamvis-wrappers-ros")]
		static extern void DisposeNode(IntPtr node);
		[DllImport("streamvis-wrappers-ros")]
		static extern bool RosOk();
		[DllImport("streamvis-wrappers-ros")]
		static extern void RosSpinOnce();
	}
}