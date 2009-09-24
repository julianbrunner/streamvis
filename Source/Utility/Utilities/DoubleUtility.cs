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
using System.Collections.Generic;
using Utility.Extensions;

namespace Utility.Utilities
{
	public static class DoubleUtility
	{
		public static IEnumerable<double> GetMarkers(double start, double end, int count)
		{
			double difference = end - start;
			int magnitude = (int)Math.Floor(Math.Log10(difference));
			double rawIntervalLength = difference * Math.Pow(10, -magnitude) / count;
			double intervalLength = rawIntervalLength.FractionRound(1, 2, 2.5, 5) * Math.Pow(10, magnitude);

			start = start.Ceiling(intervalLength);
			end = end.Floor(intervalLength);

			for (double value = start; value <= end; value += intervalLength) yield return value;
		}
		public static double Modulo(double a, double b)
		{
			if (b == 0) throw new DivideByZeroException();

			double remainder = a % b;
			if (remainder < 0) remainder += b;
			return remainder;
		}
	}
}
