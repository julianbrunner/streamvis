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
	public class IControlCalibration : IDisposable
	{
		private HandleRef swigCPtr;
		protected bool swigCMemOwn;

		internal IControlCalibration(IntPtr cPtr, bool cMemoryOwn)
		{
			swigCMemOwn = cMemoryOwn;
			swigCPtr = new HandleRef(this, cPtr);
		}

		internal static HandleRef getCPtr(IControlCalibration obj)
		{
			return (obj == null) ? new HandleRef(null, IntPtr.Zero) : obj.swigCPtr;
		}

		~IControlCalibration()
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
					yarpPINVOKE.delete_IControlCalibration(swigCPtr);
				}
				swigCPtr = new HandleRef(null, IntPtr.Zero);
				GC.SuppressFinalize(this);
			}
		}

		public virtual bool calibrate(int j, double p)
		{
			bool ret = yarpPINVOKE.IControlCalibration_calibrate__SWIG_0(swigCPtr, j, p);
			return ret;
		}

		public virtual bool done(int j)
		{
			bool ret = yarpPINVOKE.IControlCalibration_done(swigCPtr, j);
			return ret;
		}

		public virtual bool setCalibrator(SWIGTYPE_p_ICalibrator c)
		{
			bool ret = yarpPINVOKE.IControlCalibration_setCalibrator(swigCPtr, SWIGTYPE_p_ICalibrator.getCPtr(c));
			return ret;
		}

		public virtual bool calibrate()
		{
			bool ret = yarpPINVOKE.IControlCalibration_calibrate__SWIG_1(swigCPtr);
			return ret;
		}

		public virtual bool park(bool wait)
		{
			bool ret = yarpPINVOKE.IControlCalibration_park__SWIG_0(swigCPtr, wait);
			return ret;
		}

		public virtual bool park()
		{
			bool ret = yarpPINVOKE.IControlCalibration_park__SWIG_1(swigCPtr);
			return ret;
		}

	}
}