using System;
using System.Runtime.InteropServices;

namespace Yarp
{
	public class Bottle
	{
		readonly IntPtr bottle;
		
		public double this[int index] { get { return Bottle_GetDouble(bottle, index); } }
		
		internal Bottle(IntPtr bottle)
		{
			this.bottle = bottle;
		}
		
		[DllImport("Yarp.Wrapper")]
		static extern double Bottle_GetDouble(IntPtr bottle, int index);
	}
}
