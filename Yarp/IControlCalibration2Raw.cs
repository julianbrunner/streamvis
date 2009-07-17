/* ----------------------------------------------------------------------------
 * This file was automatically generated by SWIG (http://www.swig.org).
 * Version 1.3.39
 *
 * Do not make changes to this file unless you know what you are doing--modify
 * the SWIG interface file instead.
 * ----------------------------------------------------------------------------- */


using System;
using System.Runtime.InteropServices;

namespace Yarp
{
	public class IControlCalibration2Raw : IDisposable
	{
		private HandleRef swigCPtr;
		protected bool swigCMemOwn;

		internal IControlCalibration2Raw(IntPtr cPtr, bool cMemoryOwn)
		{
			swigCMemOwn = cMemoryOwn;
			swigCPtr = new HandleRef(this, cPtr);
		}

		internal static HandleRef getCPtr(IControlCalibration2Raw obj)
		{
			return (obj == null) ? new HandleRef(null, IntPtr.Zero) : obj.swigCPtr;
		}

		~IControlCalibration2Raw()
		{
			Dispose();
		}

		public virtual void Dispose()
		{
			lock (this)
			{
				if (swigCPtr.Handle != IntPtr.Zero && swigCMemOwn)
				{
					swigCMemOwn = false;
					yarpPINVOKE.delete_IControlCalibration2Raw(swigCPtr);
				}
				swigCPtr = new HandleRef(null, IntPtr.Zero);
				GC.SuppressFinalize(this);
			}
		}

		public virtual bool calibrate2Raw(int axis, uint type, double p1, double p2, double p3)
		{
			bool ret = yarpPINVOKE.IControlCalibration2Raw_calibrate2Raw(swigCPtr, axis, type, p1, p2, p3);
			return ret;
		}

		public virtual bool doneRaw(int j)
		{
			bool ret = yarpPINVOKE.IControlCalibration2Raw_doneRaw(swigCPtr, j);
			return ret;
		}

	}
}