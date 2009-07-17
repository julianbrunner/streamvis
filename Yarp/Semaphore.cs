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
	public class Semaphore : IDisposable
	{
		private HandleRef swigCPtr;
		protected bool swigCMemOwn;

		internal Semaphore(IntPtr cPtr, bool cMemoryOwn)
		{
			swigCMemOwn = cMemoryOwn;
			swigCPtr = new HandleRef(this, cPtr);
		}

		internal static HandleRef getCPtr(Semaphore obj)
		{
			return (obj == null) ? new HandleRef(null, IntPtr.Zero) : obj.swigCPtr;
		}

		~Semaphore()
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
					yarpPINVOKE.delete_Semaphore(swigCPtr);
				}
				swigCPtr = new HandleRef(null, IntPtr.Zero);
				GC.SuppressFinalize(this);
			}
		}

		public Semaphore(int initialCount)
			: this(yarpPINVOKE.new_Semaphore__SWIG_0(initialCount), true)
		{
		}

		public Semaphore()
			: this(yarpPINVOKE.new_Semaphore__SWIG_1(), true)
		{
		}

		public void wait()
		{
			yarpPINVOKE.Semaphore_wait(swigCPtr);
		}

		public new bool check()
		{
			bool ret = yarpPINVOKE.Semaphore_check(swigCPtr);
			return ret;
		}

		public void post()
		{
			yarpPINVOKE.Semaphore_post(swigCPtr);
		}

	}
}