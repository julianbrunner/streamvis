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
	public class BufferedPortProperty : Contactable
	{
		private HandleRef swigCPtr;

		internal BufferedPortProperty(IntPtr cPtr, bool cMemoryOwn)
			: base(yarpPINVOKE.BufferedPortPropertyUpcast(cPtr), cMemoryOwn)
		{
			swigCPtr = new HandleRef(this, cPtr);
		}

		internal static HandleRef getCPtr(BufferedPortProperty obj)
		{
			return (obj == null) ? new HandleRef(null, IntPtr.Zero) : obj.swigCPtr;
		}

		~BufferedPortProperty()
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
					yarpPINVOKE.delete_BufferedPortProperty(swigCPtr);
				}
				swigCPtr = new HandleRef(null, IntPtr.Zero);
				GC.SuppressFinalize(this);
				base.Dispose();
			}
		}

		public BufferedPortProperty()
			: this(yarpPINVOKE.new_BufferedPortProperty(), true)
		{
		}

		public override bool addOutput(string name)
		{
			bool ret = yarpPINVOKE.BufferedPortProperty_addOutput__SWIG_0(swigCPtr, name);
			return ret;
		}

		public override bool addOutput(string name, string carrier)
		{
			bool ret = yarpPINVOKE.BufferedPortProperty_addOutput__SWIG_1(swigCPtr, name, carrier);
			return ret;
		}

		public override bool addOutput(Contact contact)
		{
			bool ret = yarpPINVOKE.BufferedPortProperty_addOutput__SWIG_2(swigCPtr, Contact.getCPtr(contact));
			if (yarpPINVOKE.SWIGPendingException.Pending) throw yarpPINVOKE.SWIGPendingException.Retrieve();
			return ret;
		}

		public override void close()
		{
			yarpPINVOKE.BufferedPortProperty_close(swigCPtr);
		}

		public override void interrupt()
		{
			yarpPINVOKE.BufferedPortProperty_interrupt(swigCPtr);
		}

		public int getPendingReads()
		{
			int ret = yarpPINVOKE.BufferedPortProperty_getPendingReads(swigCPtr);
			return ret;
		}

		public override Contact where()
		{
			Contact ret = new Contact(yarpPINVOKE.BufferedPortProperty_where(swigCPtr), true);
			return ret;
		}

		public override ConstString getName()
		{
			ConstString ret = new ConstString(yarpPINVOKE.BufferedPortProperty_getName(swigCPtr), true);
			return ret;
		}

		public Property prepare()
		{
			Property ret = new Property(yarpPINVOKE.BufferedPortProperty_prepare(swigCPtr), false);
			return ret;
		}

		public void write(bool forceStrict)
		{
			yarpPINVOKE.BufferedPortProperty_write__SWIG_0(swigCPtr, forceStrict);
		}

		public void write()
		{
			yarpPINVOKE.BufferedPortProperty_write__SWIG_1(swigCPtr);
		}

		public void writeStrict()
		{
			yarpPINVOKE.BufferedPortProperty_writeStrict(swigCPtr);
		}

		public void setStrict(bool strict)
		{
			yarpPINVOKE.BufferedPortProperty_setStrict__SWIG_0(swigCPtr, strict);
		}

		public void setStrict()
		{
			yarpPINVOKE.BufferedPortProperty_setStrict__SWIG_1(swigCPtr);
		}

		public Property read(bool shouldWait)
		{
			IntPtr cPtr = yarpPINVOKE.BufferedPortProperty_read__SWIG_0(swigCPtr, shouldWait);
			Property ret = (cPtr == IntPtr.Zero) ? null : new Property(cPtr, false);
			return ret;
		}

		public Property read()
		{
			IntPtr cPtr = yarpPINVOKE.BufferedPortProperty_read__SWIG_1(swigCPtr);
			Property ret = (cPtr == IntPtr.Zero) ? null : new Property(cPtr, false);
			return ret;
		}

		public Property lastRead()
		{
			IntPtr cPtr = yarpPINVOKE.BufferedPortProperty_lastRead(swigCPtr);
			Property ret = (cPtr == IntPtr.Zero) ? null : new Property(cPtr, false);
			return ret;
		}

		public bool isClosed()
		{
			bool ret = yarpPINVOKE.BufferedPortProperty_isClosed(swigCPtr);
			return ret;
		}

		public void setReplier(PortReader reader)
		{
			yarpPINVOKE.BufferedPortProperty_setReplier(swigCPtr, PortReader.getCPtr(reader));
			if (yarpPINVOKE.SWIGPendingException.Pending) throw yarpPINVOKE.SWIGPendingException.Retrieve();
		}

		public void onRead(Property datum)
		{
			yarpPINVOKE.BufferedPortProperty_onRead(swigCPtr, Property.getCPtr(datum));
			if (yarpPINVOKE.SWIGPendingException.Pending) throw yarpPINVOKE.SWIGPendingException.Retrieve();
		}

		public void useCallback(TypedReaderCallbackProperty callback)
		{
			yarpPINVOKE.BufferedPortProperty_useCallback__SWIG_0(swigCPtr, TypedReaderCallbackProperty.getCPtr(callback));
			if (yarpPINVOKE.SWIGPendingException.Pending) throw yarpPINVOKE.SWIGPendingException.Retrieve();
		}

		public void useCallback()
		{
			yarpPINVOKE.BufferedPortProperty_useCallback__SWIG_1(swigCPtr);
		}

		public void disableCallback()
		{
			yarpPINVOKE.BufferedPortProperty_disableCallback(swigCPtr);
		}

		public override bool setEnvelope(PortWriter envelope)
		{
			bool ret = yarpPINVOKE.BufferedPortProperty_setEnvelope(swigCPtr, PortWriter.getCPtr(envelope));
			if (yarpPINVOKE.SWIGPendingException.Pending) throw yarpPINVOKE.SWIGPendingException.Retrieve();
			return ret;
		}

		public override bool getEnvelope(PortReader envelope)
		{
			bool ret = yarpPINVOKE.BufferedPortProperty_getEnvelope(swigCPtr, PortReader.getCPtr(envelope));
			if (yarpPINVOKE.SWIGPendingException.Pending) throw yarpPINVOKE.SWIGPendingException.Retrieve();
			return ret;
		}

		public override int getInputCount()
		{
			int ret = yarpPINVOKE.BufferedPortProperty_getInputCount(swigCPtr);
			return ret;
		}

		public override int getOutputCount()
		{
			int ret = yarpPINVOKE.BufferedPortProperty_getOutputCount(swigCPtr);
			return ret;
		}

		public override void getReport(PortReport reporter)
		{
			yarpPINVOKE.BufferedPortProperty_getReport(swigCPtr, PortReport.getCPtr(reporter));
			if (yarpPINVOKE.SWIGPendingException.Pending) throw yarpPINVOKE.SWIGPendingException.Retrieve();
		}

		public override void setReporter(PortReport reporter)
		{
			yarpPINVOKE.BufferedPortProperty_setReporter(swigCPtr, PortReport.getCPtr(reporter));
			if (yarpPINVOKE.SWIGPendingException.Pending) throw yarpPINVOKE.SWIGPendingException.Retrieve();
		}

		public SWIGTYPE_p_void acquire()
		{
			IntPtr cPtr = yarpPINVOKE.BufferedPortProperty_acquire(swigCPtr);
			SWIGTYPE_p_void ret = (cPtr == IntPtr.Zero) ? null : new SWIGTYPE_p_void(cPtr, false);
			return ret;
		}

		public void release(SWIGTYPE_p_void handle)
		{
			yarpPINVOKE.BufferedPortProperty_release(swigCPtr, SWIGTYPE_p_void.getCPtr(handle));
		}

	}
}