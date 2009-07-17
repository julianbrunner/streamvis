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
	public class TypedReaderCallbackProperty : IDisposable
	{
		private HandleRef swigCPtr;
		protected bool swigCMemOwn;

		internal TypedReaderCallbackProperty(IntPtr cPtr, bool cMemoryOwn)
		{
			swigCMemOwn = cMemoryOwn;
			swigCPtr = new HandleRef(this, cPtr);
		}

		internal static HandleRef getCPtr(TypedReaderCallbackProperty obj)
		{
			return (obj == null) ? new HandleRef(null, IntPtr.Zero) : obj.swigCPtr;
		}

		~TypedReaderCallbackProperty()
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
					yarpPINVOKE.delete_TypedReaderCallbackProperty(swigCPtr);
				}
				swigCPtr = new HandleRef(null, IntPtr.Zero);
				GC.SuppressFinalize(this);
			}
		}

		public new void onRead(Property datum)
		{
			yarpPINVOKE.TypedReaderCallbackProperty_onRead__SWIG_0(swigCPtr, Property.getCPtr(datum));
			if (yarpPINVOKE.SWIGPendingException.Pending) throw yarpPINVOKE.SWIGPendingException.Retrieve();
		}

		public new void onRead(Property datum, TypedReaderProperty reader)
		{
			yarpPINVOKE.TypedReaderCallbackProperty_onRead__SWIG_1(swigCPtr, Property.getCPtr(datum), TypedReaderProperty.getCPtr(reader));
			if (yarpPINVOKE.SWIGPendingException.Pending) throw yarpPINVOKE.SWIGPendingException.Retrieve();
		}

		public TypedReaderCallbackProperty()
			: this(yarpPINVOKE.new_TypedReaderCallbackProperty(), true)
		{
		}

	}
}