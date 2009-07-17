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
	public class Property : Searchable
	{
		private HandleRef swigCPtr;

		internal Property(IntPtr cPtr, bool cMemoryOwn)
			: base(yarpPINVOKE.PropertyUpcast(cPtr), cMemoryOwn)
		{
			swigCPtr = new HandleRef(this, cPtr);
		}

		internal static HandleRef getCPtr(Property obj)
		{
			return (obj == null) ? new HandleRef(null, IntPtr.Zero) : obj.swigCPtr;
		}

		~Property()
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
					yarpPINVOKE.delete_Property(swigCPtr);
				}
				swigCPtr = new HandleRef(null, IntPtr.Zero);
				GC.SuppressFinalize(this);
				base.Dispose();
			}
		}

		public new bool check(string key, string comment)
		{
			bool ret = yarpPINVOKE.Property_check__SWIG_0_0(swigCPtr, key, comment);
			return ret;
		}

		public new Value check(string key, Value fallback, string comment)
		{
			Value ret = new Value(yarpPINVOKE.Property_check__SWIG_0_1(swigCPtr, key, Value.getCPtr(fallback), comment), true);
			if (yarpPINVOKE.SWIGPendingException.Pending) throw yarpPINVOKE.SWIGPendingException.Retrieve();
			return ret;
		}

		public new Value check(string key, Value fallback)
		{
			Value ret = new Value(yarpPINVOKE.Property_check__SWIG_0_2(swigCPtr, key, Value.getCPtr(fallback)), true);
			if (yarpPINVOKE.SWIGPendingException.Pending) throw yarpPINVOKE.SWIGPendingException.Retrieve();
			return ret;
		}

		public new Bottle findGroup(string key, string comment)
		{
			Bottle ret = new Bottle(yarpPINVOKE.Property_findGroup__SWIG_0_0(swigCPtr, key, comment), false);
			return ret;
		}

		public Property()
			: this(yarpPINVOKE.new_Property__SWIG_0(), true)
		{
		}

		public Property(string str)
			: this(yarpPINVOKE.new_Property__SWIG_1(str), true)
		{
		}

		public Property(Property prop)
			: this(yarpPINVOKE.new_Property__SWIG_2(Property.getCPtr(prop)), true)
		{
			if (yarpPINVOKE.SWIGPendingException.Pending) throw yarpPINVOKE.SWIGPendingException.Retrieve();
		}

		public new bool check(string key)
		{
			bool ret = yarpPINVOKE.Property_check__SWIG_1(swigCPtr, key);
			return ret;
		}

		public void put(string key, string value)
		{
			yarpPINVOKE.Property_put__SWIG_0(swigCPtr, key, value);
		}

		public void put(string key, Value value)
		{
			yarpPINVOKE.Property_put__SWIG_1(swigCPtr, key, Value.getCPtr(value));
			if (yarpPINVOKE.SWIGPendingException.Pending) throw yarpPINVOKE.SWIGPendingException.Retrieve();
		}

		public void put(string key, int v)
		{
			yarpPINVOKE.Property_put__SWIG_2(swigCPtr, key, v);
		}

		public void put(string key, double v)
		{
			yarpPINVOKE.Property_put__SWIG_3(swigCPtr, key, v);
		}

		public void unput(string key)
		{
			yarpPINVOKE.Property_unput(swigCPtr, key);
		}

		public new Value find(string key)
		{
			Value ret = new Value(yarpPINVOKE.Property_find(swigCPtr, key), false);
			return ret;
		}

		public new Bottle findGroup(string key)
		{
			Bottle ret = new Bottle(yarpPINVOKE.Property_findGroup__SWIG_1(swigCPtr, key), false);
			return ret;
		}

		public void clear()
		{
			yarpPINVOKE.Property_clear(swigCPtr);
		}

		public void fromString(string txt, bool wipe)
		{
			yarpPINVOKE.Property_fromString__SWIG_0(swigCPtr, txt, wipe);
		}

		public void fromString(string txt)
		{
			yarpPINVOKE.Property_fromString__SWIG_1(swigCPtr, txt);
		}

		public void fromCommand(int argc, SWIGTYPE_p_p_char argv, bool skipFirst, bool wipe)
		{
			yarpPINVOKE.Property_fromCommand__SWIG_0(swigCPtr, argc, SWIGTYPE_p_p_char.getCPtr(argv), skipFirst, wipe);
		}

		public void fromCommand(int argc, SWIGTYPE_p_p_char argv, bool skipFirst)
		{
			yarpPINVOKE.Property_fromCommand__SWIG_1(swigCPtr, argc, SWIGTYPE_p_p_char.getCPtr(argv), skipFirst);
		}

		public void fromCommand(int argc, SWIGTYPE_p_p_char argv)
		{
			yarpPINVOKE.Property_fromCommand__SWIG_2(swigCPtr, argc, SWIGTYPE_p_p_char.getCPtr(argv));
		}

		public bool fromConfigFile(string fname, bool wipe)
		{
			bool ret = yarpPINVOKE.Property_fromConfigFile__SWIG_0(swigCPtr, fname, wipe);
			return ret;
		}

		public bool fromConfigFile(string fname)
		{
			bool ret = yarpPINVOKE.Property_fromConfigFile__SWIG_1(swigCPtr, fname);
			return ret;
		}

		public bool fromConfigFile(string fname, Searchable env, bool wipe)
		{
			bool ret = yarpPINVOKE.Property_fromConfigFile__SWIG_2(swigCPtr, fname, Searchable.getCPtr(env), wipe);
			if (yarpPINVOKE.SWIGPendingException.Pending) throw yarpPINVOKE.SWIGPendingException.Retrieve();
			return ret;
		}

		public bool fromConfigFile(string fname, Searchable env)
		{
			bool ret = yarpPINVOKE.Property_fromConfigFile__SWIG_3(swigCPtr, fname, Searchable.getCPtr(env));
			if (yarpPINVOKE.SWIGPendingException.Pending) throw yarpPINVOKE.SWIGPendingException.Retrieve();
			return ret;
		}

		public void fromConfig(string txt, bool wipe)
		{
			yarpPINVOKE.Property_fromConfig__SWIG_0(swigCPtr, txt, wipe);
		}

		public void fromConfig(string txt)
		{
			yarpPINVOKE.Property_fromConfig__SWIG_1(swigCPtr, txt);
		}

		public void fromConfig(string txt, Searchable env, bool wipe)
		{
			yarpPINVOKE.Property_fromConfig__SWIG_2(swigCPtr, txt, Searchable.getCPtr(env), wipe);
			if (yarpPINVOKE.SWIGPendingException.Pending) throw yarpPINVOKE.SWIGPendingException.Retrieve();
		}

		public void fromConfig(string txt, Searchable env)
		{
			yarpPINVOKE.Property_fromConfig__SWIG_3(swigCPtr, txt, Searchable.getCPtr(env));
			if (yarpPINVOKE.SWIGPendingException.Pending) throw yarpPINVOKE.SWIGPendingException.Retrieve();
		}

		public void fromQuery(string url, bool wipe)
		{
			yarpPINVOKE.Property_fromQuery__SWIG_0(swigCPtr, url, wipe);
		}

		public void fromQuery(string url)
		{
			yarpPINVOKE.Property_fromQuery__SWIG_1(swigCPtr, url);
		}

		public new ConstString toString_c()
		{
			ConstString ret = new ConstString(yarpPINVOKE.Property_toString_c(swigCPtr), true);
			return ret;
		}

		public new bool write(ConnectionWriter connection)
		{
			bool ret = yarpPINVOKE.Property_write(swigCPtr, ConnectionWriter.getCPtr(connection));
			if (yarpPINVOKE.SWIGPendingException.Pending) throw yarpPINVOKE.SWIGPendingException.Retrieve();
			return ret;
		}

		public new string toString()
		{
			string ret = yarpPINVOKE.Property_toString(swigCPtr);
			return ret;
		}

	}
}