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
	public class PixelHsvFloat : IDisposable
	{
		private HandleRef swigCPtr;
		protected bool swigCMemOwn;

		internal PixelHsvFloat(IntPtr cPtr, bool cMemoryOwn)
		{
			swigCMemOwn = cMemoryOwn;
			swigCPtr = new HandleRef(this, cPtr);
		}

		internal static HandleRef getCPtr(PixelHsvFloat obj)
		{
			return (obj == null) ? new HandleRef(null, IntPtr.Zero) : obj.swigCPtr;
		}

		~PixelHsvFloat()
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
					yarpPINVOKE.delete_PixelHsvFloat(swigCPtr);
				}
				swigCPtr = new HandleRef(null, IntPtr.Zero);
				GC.SuppressFinalize(this);
			}
		}

		public float h
		{
			set
			{
				yarpPINVOKE.PixelHsvFloat_h_set(swigCPtr, value);
			}
			get
			{
				float ret = yarpPINVOKE.PixelHsvFloat_h_get(swigCPtr);
				return ret;
			}
		}

		public float s
		{
			set
			{
				yarpPINVOKE.PixelHsvFloat_s_set(swigCPtr, value);
			}
			get
			{
				float ret = yarpPINVOKE.PixelHsvFloat_s_get(swigCPtr);
				return ret;
			}
		}

		public float v
		{
			set
			{
				yarpPINVOKE.PixelHsvFloat_v_set(swigCPtr, value);
			}
			get
			{
				float ret = yarpPINVOKE.PixelHsvFloat_v_get(swigCPtr);
				return ret;
			}
		}

		public PixelHsvFloat()
			: this(yarpPINVOKE.new_PixelHsvFloat(), true)
		{
		}

	}
}