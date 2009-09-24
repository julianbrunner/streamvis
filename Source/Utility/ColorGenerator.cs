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
using System.Drawing;
using Utility.Extensions;
using System.Linq;
using System.Collections.Generic;
using Utility.Utilities;

namespace Utility
{
	public class ColorGenerator
	{
		readonly Random random = new Random();
		readonly List<double> hues = new List<double>();

		int position = 0;

		public ColorGenerator()
		{
			for (int i = 0; i < 38; i++) hues.Add(GetHue());
			hues.Sort();
		}

		public Color NextColor()
		{
			//double hue = GetHue();

			//hues.Add(hue);

			//return FromHsv(hue, random.NextDouble(1, 1), random.NextDouble(1, 1));

			return FromHsv(hues[position++], random.NextDouble(0.8, 1), random.NextDouble(0.8, 1));
		}

		double GetHue()
		{
			if (!hues.Any()) 
				return random.NextDouble(0, 6);

			return
			(
				from randomHue in GetRandomHues(100)
				let distance = hues.Min(hue => HueDifference(hue, randomHue))
				orderby distance descending
				select randomHue
			)
			.First();
		}
		IEnumerable<double> GetRandomHues(int count)
		{
			for (int i = 0; i < count; i++) yield return random.NextDouble(0, 6);
		}

		static double HueDifference(double hue1, double hue2)
		{
			if (hue1 > hue2)
			{
				double temp = hue1;
				hue1 = hue2;
				hue2 = temp;
			}

			return Math.Min(hue2 - hue1, (hue1 + 6) - hue2);
		}
		static Color FromHsv(double hue, double saturation, double value)
		{
			if (hue < 0 || hue >= 6) throw new ArgumentOutOfRangeException("hue");
			if (saturation < 0 || saturation > 1) throw new ArgumentOutOfRangeException("saturation");
			if (value < 0 || value > 1) throw new ArgumentOutOfRangeException("value");

			int hueIndex = (int)hue;
			double hueFraction = hue - hueIndex;

			double bottom = value * (1 - saturation);
			double top = value;
			double rising = value * (1 - (1 - hueFraction) * saturation);
			double falling = value * (1 - hueFraction * saturation);

			double red, green, blue;

			switch (hueIndex)
			{
				case 0: red = top; green = rising; blue = bottom; break;
				case 1: red = falling; green = top; blue = bottom; break;
				case 2: red = bottom; green = top; blue = rising; break;
				case 3: red = bottom; green = falling; blue = top; break;
				case 4: red = rising; green = bottom; blue = top; break;
				case 5: red = top; green = bottom; blue = falling; break;
				default: throw new InvalidOperationException();
			}

			return FromRgb(red, green, blue);
		}
		static Color FromRgb(double red, double green, double blue)
		{
			if (red < 0 || red > 1) throw new ArgumentOutOfRangeException("red");
			if (green < 0 || green > 1) throw new ArgumentOutOfRangeException("green");
			if (blue < 0 || blue > 1) throw new ArgumentOutOfRangeException("blue");

			return Color.FromArgb(ToByte(red), ToByte(green), ToByte(blue));
		}
		static byte ToByte(double value)
		{
			if (value < 0 || value > 1) throw new ArgumentOutOfRangeException("value");

			return value == 1 ? (byte)0xFF : (byte)(value * 0x100);
		}
	}
}
