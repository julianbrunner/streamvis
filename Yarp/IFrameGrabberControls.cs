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
	public class IFrameGrabberControls : IDisposable
	{
		private HandleRef swigCPtr;
		protected bool swigCMemOwn;

		internal IFrameGrabberControls(IntPtr cPtr, bool cMemoryOwn)
		{
			swigCMemOwn = cMemoryOwn;
			swigCPtr = new HandleRef(this, cPtr);
		}

		internal static HandleRef getCPtr(IFrameGrabberControls obj)
		{
			return (obj == null) ? new HandleRef(null, IntPtr.Zero) : obj.swigCPtr;
		}

		~IFrameGrabberControls()
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
					yarpPINVOKE.delete_IFrameGrabberControls(swigCPtr);
				}
				swigCPtr = new HandleRef(null, IntPtr.Zero);
				GC.SuppressFinalize(this);
			}
		}

		public virtual bool setBrightness(double v)
		{
			bool ret = yarpPINVOKE.IFrameGrabberControls_setBrightness(swigCPtr, v);
			return ret;
		}

		public virtual bool setExposure(double v)
		{
			bool ret = yarpPINVOKE.IFrameGrabberControls_setExposure(swigCPtr, v);
			return ret;
		}

		public virtual bool setSharpness(double v)
		{
			bool ret = yarpPINVOKE.IFrameGrabberControls_setSharpness(swigCPtr, v);
			return ret;
		}

		public virtual bool setWhiteBalance(double blue, double red)
		{
			bool ret = yarpPINVOKE.IFrameGrabberControls_setWhiteBalance(swigCPtr, blue, red);
			return ret;
		}

		public virtual bool setHue(double v)
		{
			bool ret = yarpPINVOKE.IFrameGrabberControls_setHue(swigCPtr, v);
			return ret;
		}

		public virtual bool setSaturation(double v)
		{
			bool ret = yarpPINVOKE.IFrameGrabberControls_setSaturation(swigCPtr, v);
			return ret;
		}

		public virtual bool setGamma(double v)
		{
			bool ret = yarpPINVOKE.IFrameGrabberControls_setGamma(swigCPtr, v);
			return ret;
		}

		public virtual bool setShutter(double v)
		{
			bool ret = yarpPINVOKE.IFrameGrabberControls_setShutter(swigCPtr, v);
			return ret;
		}

		public virtual bool setGain(double v)
		{
			bool ret = yarpPINVOKE.IFrameGrabberControls_setGain(swigCPtr, v);
			return ret;
		}

		public virtual bool setIris(double v)
		{
			bool ret = yarpPINVOKE.IFrameGrabberControls_setIris(swigCPtr, v);
			return ret;
		}

		public virtual double getBrightness()
		{
			double ret = yarpPINVOKE.IFrameGrabberControls_getBrightness(swigCPtr);
			return ret;
		}

		public virtual double getExposure()
		{
			double ret = yarpPINVOKE.IFrameGrabberControls_getExposure(swigCPtr);
			return ret;
		}

		public virtual double getSharpness()
		{
			double ret = yarpPINVOKE.IFrameGrabberControls_getSharpness(swigCPtr);
			return ret;
		}

		public virtual bool getWhiteBalance(SWIGTYPE_p_double blue, SWIGTYPE_p_double red)
		{
			bool ret = yarpPINVOKE.IFrameGrabberControls_getWhiteBalance(swigCPtr, SWIGTYPE_p_double.getCPtr(blue), SWIGTYPE_p_double.getCPtr(red));
			if (yarpPINVOKE.SWIGPendingException.Pending) throw yarpPINVOKE.SWIGPendingException.Retrieve();
			return ret;
		}

		public virtual double getHue()
		{
			double ret = yarpPINVOKE.IFrameGrabberControls_getHue(swigCPtr);
			return ret;
		}

		public virtual double getSaturation()
		{
			double ret = yarpPINVOKE.IFrameGrabberControls_getSaturation(swigCPtr);
			return ret;
		}

		public virtual double getGamma()
		{
			double ret = yarpPINVOKE.IFrameGrabberControls_getGamma(swigCPtr);
			return ret;
		}

		public virtual double getShutter()
		{
			double ret = yarpPINVOKE.IFrameGrabberControls_getShutter(swigCPtr);
			return ret;
		}

		public virtual double getGain()
		{
			double ret = yarpPINVOKE.IFrameGrabberControls_getGain(swigCPtr);
			return ret;
		}

		public virtual double getIris()
		{
			double ret = yarpPINVOKE.IFrameGrabberControls_getIris(swigCPtr);
			return ret;
		}

	}
}