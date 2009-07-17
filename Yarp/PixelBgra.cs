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
	public class PixelBgra : IDisposable
	{
		private HandleRef swigCPtr;
		protected bool swigCMemOwn;

		internal PixelBgra(IntPtr cPtr, bool cMemoryOwn)
		{
			swigCMemOwn = cMemoryOwn;
			swigCPtr = new HandleRef(this, cPtr);
		}

		internal static HandleRef getCPtr(PixelBgra obj)
		{
			return (obj == null) ? new HandleRef(null, IntPtr.Zero) : obj.swigCPtr;
		}

		~PixelBgra()
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
					yarpPINVOKE.delete_PixelBgra(swigCPtr);
				}
				swigCPtr = new HandleRef(null, IntPtr.Zero);
				GC.SuppressFinalize(this);
			}
		}

		public byte b
		{
			set
			{
				yarpPINVOKE.PixelBgra_b_set(swigCPtr, value);
			}
			get
			{
				byte ret = yarpPINVOKE.PixelBgra_b_get(swigCPtr);
				return ret;
			}
		}

		public byte g
		{
			set
			{
				yarpPINVOKE.PixelBgra_g_set(swigCPtr, value);
			}
			get
			{
				byte ret = yarpPINVOKE.PixelBgra_g_get(swigCPtr);
				return ret;
			}
		}

		public byte r
		{
			set
			{
				yarpPINVOKE.PixelBgra_r_set(swigCPtr, value);
			}
			get
			{
				byte ret = yarpPINVOKE.PixelBgra_r_get(swigCPtr);
				return ret;
			}
		}

		public byte a
		{
			set
			{
				yarpPINVOKE.PixelBgra_a_set(swigCPtr, value);
			}
			get
			{
				byte ret = yarpPINVOKE.PixelBgra_a_get(swigCPtr);
				return ret;
			}
		}

		public PixelBgra()
			: this(yarpPINVOKE.new_PixelBgra__SWIG_0(), true)
		{
		}

		public PixelBgra(byte n_r, byte n_g, byte n_b, byte n_a)
			: this(yarpPINVOKE.new_PixelBgra__SWIG_1(n_r, n_g, n_b, n_a), true)
		{
		}

	}
}