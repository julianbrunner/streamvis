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
	public class PixelHsv : IDisposable
	{
		private HandleRef swigCPtr;
		protected bool swigCMemOwn;

		internal PixelHsv(IntPtr cPtr, bool cMemoryOwn)
		{
			swigCMemOwn = cMemoryOwn;
			swigCPtr = new HandleRef(this, cPtr);
		}

		internal static HandleRef getCPtr(PixelHsv obj)
		{
			return (obj == null) ? new HandleRef(null, IntPtr.Zero) : obj.swigCPtr;
		}

		~PixelHsv()
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
					yarpPINVOKE.delete_PixelHsv(swigCPtr);
				}
				swigCPtr = new HandleRef(null, IntPtr.Zero);
				GC.SuppressFinalize(this);
			}
		}

		public byte h
		{
			set
			{
				yarpPINVOKE.PixelHsv_h_set(swigCPtr, value);
			}
			get
			{
				byte ret = yarpPINVOKE.PixelHsv_h_get(swigCPtr);
				return ret;
			}
		}

		public byte s
		{
			set
			{
				yarpPINVOKE.PixelHsv_s_set(swigCPtr, value);
			}
			get
			{
				byte ret = yarpPINVOKE.PixelHsv_s_get(swigCPtr);
				return ret;
			}
		}

		public byte v
		{
			set
			{
				yarpPINVOKE.PixelHsv_v_set(swigCPtr, value);
			}
			get
			{
				byte ret = yarpPINVOKE.PixelHsv_v_get(swigCPtr);
				return ret;
			}
		}

		public PixelHsv()
			: this(yarpPINVOKE.new_PixelHsv(), true)
		{
		}

	}
}