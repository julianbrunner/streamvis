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
	public class yarp
	{
		public static void typedReaderMissingCallback()
		{
			yarpPINVOKE.typedReaderMissingCallback();
		}

		public static readonly int BOTTLE_TAG_INT = yarpPINVOKE.BOTTLE_TAG_INT_get();
		public static readonly int BOTTLE_TAG_VOCAB = yarpPINVOKE.BOTTLE_TAG_VOCAB_get();
		public static readonly int BOTTLE_TAG_DOUBLE = yarpPINVOKE.BOTTLE_TAG_DOUBLE_get();
		public static readonly int BOTTLE_TAG_STRING = yarpPINVOKE.BOTTLE_TAG_STRING_get();
		public static readonly int BOTTLE_TAG_BLOB = yarpPINVOKE.BOTTLE_TAG_BLOB_get();
		public static readonly int BOTTLE_TAG_LIST = yarpPINVOKE.BOTTLE_TAG_LIST_get();
		public static readonly int VOCAB_CALIBRATE_JOINT = yarpPINVOKE.VOCAB_CALIBRATE_JOINT_get();
		public static readonly int VOCAB_CALIBRATE = yarpPINVOKE.VOCAB_CALIBRATE_get();
		public static readonly int VOCAB_ABORTCALIB = yarpPINVOKE.VOCAB_ABORTCALIB_get();
		public static readonly int VOCAB_ABORTPARK = yarpPINVOKE.VOCAB_ABORTPARK_get();
		public static readonly int VOCAB_CALIBRATE_DONE = yarpPINVOKE.VOCAB_CALIBRATE_DONE_get();
		public static readonly int VOCAB_PARK = yarpPINVOKE.VOCAB_PARK_get();
		public static readonly int VOCAB_SET = yarpPINVOKE.VOCAB_SET_get();
		public static readonly int VOCAB_GET = yarpPINVOKE.VOCAB_GET_get();
		public static readonly int VOCAB_IS = yarpPINVOKE.VOCAB_IS_get();
		public static readonly int VOCAB_FAILED = yarpPINVOKE.VOCAB_FAILED_get();
		public static readonly int VOCAB_OK = yarpPINVOKE.VOCAB_OK_get();
		public static readonly int VOCAB_OFFSET = yarpPINVOKE.VOCAB_OFFSET_get();
		public static readonly int VOCAB_PID = yarpPINVOKE.VOCAB_PID_get();
		public static readonly int VOCAB_PIDS = yarpPINVOKE.VOCAB_PIDS_get();
		public static readonly int VOCAB_REF = yarpPINVOKE.VOCAB_REF_get();
		public static readonly int VOCAB_REFS = yarpPINVOKE.VOCAB_REFS_get();
		public static readonly int VOCAB_LIM = yarpPINVOKE.VOCAB_LIM_get();
		public static readonly int VOCAB_LIMS = yarpPINVOKE.VOCAB_LIMS_get();
		public static readonly int VOCAB_RESET = yarpPINVOKE.VOCAB_RESET_get();
		public static readonly int VOCAB_DISABLE = yarpPINVOKE.VOCAB_DISABLE_get();
		public static readonly int VOCAB_ENABLE = yarpPINVOKE.VOCAB_ENABLE_get();
		public static readonly int VOCAB_ERR = yarpPINVOKE.VOCAB_ERR_get();
		public static readonly int VOCAB_ERRS = yarpPINVOKE.VOCAB_ERRS_get();
		public static readonly int VOCAB_OUTPUT = yarpPINVOKE.VOCAB_OUTPUT_get();
		public static readonly int VOCAB_OUTPUTS = yarpPINVOKE.VOCAB_OUTPUTS_get();
		public static readonly int VOCAB_REFERENCE = yarpPINVOKE.VOCAB_REFERENCE_get();
		public static readonly int VOCAB_REFERENCES = yarpPINVOKE.VOCAB_REFERENCES_get();
		public static readonly int VOCAB_AXES = yarpPINVOKE.VOCAB_AXES_get();
		public static readonly int VOCAB_MOTION_DONE = yarpPINVOKE.VOCAB_MOTION_DONE_get();
		public static readonly int VOCAB_MOTION_DONES = yarpPINVOKE.VOCAB_MOTION_DONES_get();
		public static readonly int VOCAB_POSITION_MODE = yarpPINVOKE.VOCAB_POSITION_MODE_get();
		public static readonly int VOCAB_POSITION_MOVE = yarpPINVOKE.VOCAB_POSITION_MOVE_get();
		public static readonly int VOCAB_POSITION_MOVES = yarpPINVOKE.VOCAB_POSITION_MOVES_get();
		public static readonly int VOCAB_RELATIVE_MOVE = yarpPINVOKE.VOCAB_RELATIVE_MOVE_get();
		public static readonly int VOCAB_RELATIVE_MOVES = yarpPINVOKE.VOCAB_RELATIVE_MOVES_get();
		public static readonly int VOCAB_REF_SPEED = yarpPINVOKE.VOCAB_REF_SPEED_get();
		public static readonly int VOCAB_REF_SPEEDS = yarpPINVOKE.VOCAB_REF_SPEEDS_get();
		public static readonly int VOCAB_REF_ACCELERATION = yarpPINVOKE.VOCAB_REF_ACCELERATION_get();
		public static readonly int VOCAB_REF_ACCELERATIONS = yarpPINVOKE.VOCAB_REF_ACCELERATIONS_get();
		public static readonly int VOCAB_STOP = yarpPINVOKE.VOCAB_STOP_get();
		public static readonly int VOCAB_STOPS = yarpPINVOKE.VOCAB_STOPS_get();
		public static readonly int VOCAB_VELOCITY_MODE = yarpPINVOKE.VOCAB_VELOCITY_MODE_get();
		public static readonly int VOCAB_VELOCITY_MOVE = yarpPINVOKE.VOCAB_VELOCITY_MOVE_get();
		public static readonly int VOCAB_VELOCITY_MOVES = yarpPINVOKE.VOCAB_VELOCITY_MOVES_get();
		public static readonly int VOCAB_E_RESET = yarpPINVOKE.VOCAB_E_RESET_get();
		public static readonly int VOCAB_E_RESETS = yarpPINVOKE.VOCAB_E_RESETS_get();
		public static readonly int VOCAB_ENCODER = yarpPINVOKE.VOCAB_ENCODER_get();
		public static readonly int VOCAB_ENCODERS = yarpPINVOKE.VOCAB_ENCODERS_get();
		public static readonly int VOCAB_ENCODER_SPEED = yarpPINVOKE.VOCAB_ENCODER_SPEED_get();
		public static readonly int VOCAB_ENCODER_SPEEDS = yarpPINVOKE.VOCAB_ENCODER_SPEEDS_get();
		public static readonly int VOCAB_ENCODER_ACCELERATION = yarpPINVOKE.VOCAB_ENCODER_ACCELERATION_get();
		public static readonly int VOCAB_ENCODER_ACCELERATIONS = yarpPINVOKE.VOCAB_ENCODER_ACCELERATIONS_get();
		public static readonly int VOCAB_AMP_ENABLE = yarpPINVOKE.VOCAB_AMP_ENABLE_get();
		public static readonly int VOCAB_AMP_DISABLE = yarpPINVOKE.VOCAB_AMP_DISABLE_get();
		public static readonly int VOCAB_AMP_CURRENT = yarpPINVOKE.VOCAB_AMP_CURRENT_get();
		public static readonly int VOCAB_AMP_CURRENTS = yarpPINVOKE.VOCAB_AMP_CURRENTS_get();
		public static readonly int VOCAB_AMP_MAXCURRENT = yarpPINVOKE.VOCAB_AMP_MAXCURRENT_get();
		public static readonly int VOCAB_AMP_STATUS = yarpPINVOKE.VOCAB_AMP_STATUS_get();
		public static readonly int VOCAB_LIMITS = yarpPINVOKE.VOCAB_LIMITS_get();
		public static readonly int VOCAB_INFO_NAME = yarpPINVOKE.VOCAB_INFO_NAME_get();
		public static readonly int VOCAB_TIMESTAMP = yarpPINVOKE.VOCAB_TIMESTAMP_get();
	}
}