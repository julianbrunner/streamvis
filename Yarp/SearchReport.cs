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
	public class SearchReport : IDisposable
	{
		private HandleRef swigCPtr;
		protected bool swigCMemOwn;

		internal SearchReport(IntPtr cPtr, bool cMemoryOwn)
		{
			swigCMemOwn = cMemoryOwn;
			swigCPtr = new HandleRef(this, cPtr);
		}

		internal static HandleRef getCPtr(SearchReport obj)
		{
			return (obj == null) ? new HandleRef(null, IntPtr.Zero) : obj.swigCPtr;
		}

		~SearchReport()
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
					yarpPINVOKE.delete_SearchReport(swigCPtr);
				}
				swigCPtr = new HandleRef(null, IntPtr.Zero);
				GC.SuppressFinalize(this);
			}
		}

		public ConstString key
		{
			set
			{
				yarpPINVOKE.SearchReport_key_set(swigCPtr, ConstString.getCPtr(value));
			}
			get
			{
				IntPtr cPtr = yarpPINVOKE.SearchReport_key_get(swigCPtr);
				ConstString ret = (cPtr == IntPtr.Zero) ? null : new ConstString(cPtr, false);
				return ret;
			}
		}

		public ConstString value
		{
			set
			{
				yarpPINVOKE.SearchReport_value_set(swigCPtr, ConstString.getCPtr(value));
			}
			get
			{
				IntPtr cPtr = yarpPINVOKE.SearchReport_value_get(swigCPtr);
				ConstString ret = (cPtr == IntPtr.Zero) ? null : new ConstString(cPtr, false);
				return ret;
			}
		}

		public bool isFound
		{
			set
			{
				yarpPINVOKE.SearchReport_isFound_set(swigCPtr, value);
			}
			get
			{
				bool ret = yarpPINVOKE.SearchReport_isFound_get(swigCPtr);
				return ret;
			}
		}

		public bool isGroup
		{
			set
			{
				yarpPINVOKE.SearchReport_isGroup_set(swigCPtr, value);
			}
			get
			{
				bool ret = yarpPINVOKE.SearchReport_isGroup_get(swigCPtr);
				return ret;
			}
		}

		public bool isComment
		{
			set
			{
				yarpPINVOKE.SearchReport_isComment_set(swigCPtr, value);
			}
			get
			{
				bool ret = yarpPINVOKE.SearchReport_isComment_get(swigCPtr);
				return ret;
			}
		}

		public bool isDefault
		{
			set
			{
				yarpPINVOKE.SearchReport_isDefault_set(swigCPtr, value);
			}
			get
			{
				bool ret = yarpPINVOKE.SearchReport_isDefault_get(swigCPtr);
				return ret;
			}
		}

		public SearchReport()
			: this(yarpPINVOKE.new_SearchReport(), true)
		{
		}

	}
}