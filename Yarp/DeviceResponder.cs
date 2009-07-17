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
	public class DeviceResponder : PortReader
	{
		private HandleRef swigCPtr;

		internal DeviceResponder(IntPtr cPtr, bool cMemoryOwn)
			: base(yarpPINVOKE.DeviceResponderUpcast(cPtr), cMemoryOwn)
		{
			swigCPtr = new HandleRef(this, cPtr);
		}

		internal static HandleRef getCPtr(DeviceResponder obj)
		{
			return (obj == null) ? new HandleRef(null, IntPtr.Zero) : obj.swigCPtr;
		}

		~DeviceResponder()
		{
			Dispose();
		}

		public override void Dispose()
		{
			lock (this)
			{
				if (swigCPtr.Handle != IntPtr.Zero && swigCMemOwn)
				{
					swigCMemOwn = false;
					yarpPINVOKE.delete_DeviceResponder(swigCPtr);
				}
				swigCPtr = new HandleRef(null, IntPtr.Zero);
				GC.SuppressFinalize(this);
				base.Dispose();
			}
		}

		public DeviceResponder()
			: this(yarpPINVOKE.new_DeviceResponder(), true)
		{
		}

		public void addUsage(string txt, string explain)
		{
			yarpPINVOKE.DeviceResponder_addUsage__SWIG_0(swigCPtr, txt, explain);
		}

		public void addUsage(string txt)
		{
			yarpPINVOKE.DeviceResponder_addUsage__SWIG_1(swigCPtr, txt);
		}

		public void addUsage(Bottle bot, string explain)
		{
			yarpPINVOKE.DeviceResponder_addUsage__SWIG_2(swigCPtr, Bottle.getCPtr(bot), explain);
			if (yarpPINVOKE.SWIGPendingException.Pending) throw yarpPINVOKE.SWIGPendingException.Retrieve();
		}

		public void addUsage(Bottle bot)
		{
			yarpPINVOKE.DeviceResponder_addUsage__SWIG_3(swigCPtr, Bottle.getCPtr(bot));
			if (yarpPINVOKE.SWIGPendingException.Pending) throw yarpPINVOKE.SWIGPendingException.Retrieve();
		}

		public virtual bool respond(Bottle command, Bottle reply)
		{
			bool ret = yarpPINVOKE.DeviceResponder_respond(swigCPtr, Bottle.getCPtr(command), Bottle.getCPtr(reply));
			if (yarpPINVOKE.SWIGPendingException.Pending) throw yarpPINVOKE.SWIGPendingException.Retrieve();
			return ret;
		}

		public void onRead(Bottle v)
		{
			yarpPINVOKE.DeviceResponder_onRead(swigCPtr, Bottle.getCPtr(v));
			if (yarpPINVOKE.SWIGPendingException.Pending) throw yarpPINVOKE.SWIGPendingException.Retrieve();
		}

		public void makeUsage()
		{
			yarpPINVOKE.DeviceResponder_makeUsage(swigCPtr);
		}

		public void attach(TypedReaderBottle source)
		{
			yarpPINVOKE.DeviceResponder_attach(swigCPtr, TypedReaderBottle.getCPtr(source));
			if (yarpPINVOKE.SWIGPendingException.Pending) throw yarpPINVOKE.SWIGPendingException.Retrieve();
		}

	}
}