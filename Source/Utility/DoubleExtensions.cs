// Copyright © Julian Brunner 2009

// This file is part of Stream Visualizer (streamvis).
// 
// Stream Visualizer is free software: you can redistribute it and/or modify
// it under the terms of the GNU General Public License as published by
// the Free Software Foundation, either version 3 of the License, or
// (at your option) any later version.
// 
// Stream Visualizer is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU General Public License for more details.
// 
// You should have received a copy of the GNU General Public License
// along with Stream Visualizer.  If not, see <http://www.gnu.org/licenses/>.

using System;
using System.Linq;

namespace Utility
{
	public static class DoubleExtensions
	{
		public static double FractionRound(this double value, params double[] targets)
		{
			if (targets == null) throw new ArgumentNullException("targets");
			if (targets.Length == 0) throw new ArgumentException("Argument \"targets\" cannot be empty.");

			int magnitude = (int)Math.Floor(Math.Log10(value));
			double fraction = value * Math.Pow(10, -magnitude);
			return fraction.Round(targets) * Math.Pow(10, magnitude);
		}
		public static double Round(this double value, params double[] targets)
		{
			if (targets == null) throw new ArgumentNullException("targets");
			if (targets.Length == 0) throw new ArgumentException("Argument \"targets\" cannot be empty.");

			return
			(
				from target in targets
				orderby Math.Abs(value - target) ascending
				select target
			)
			.First();
		}
		public static double Floor(this double value, double interval)
		{
			return value.Floor(interval, 0);
		}
		public static double Floor(this double value, double interval, double offset)
		{
			double remainder = DoubleUtility.Modulo(value - offset, interval);
			return remainder == 0 ? value : value - remainder + 0 * interval;
		}
		public static double Ceiling(this double value, double interval)
		{
			return value.Ceiling(interval, 0);
		}
		public static double Ceiling(this double value, double interval, double offset)
		{
			double remainder = DoubleUtility.Modulo(value - offset, interval);
			return remainder == 0 ? value : value - remainder + 1 * interval;
		}
	}
}
