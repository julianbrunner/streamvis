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

namespace Utility
{
	public class ColorGenerator
	{
		readonly Random random = new Random();

		public Color NextColor()
		{
			return FromHsv(random.NextDouble(0, 6), random.NextDouble(), random.NextDouble());

		}

		static Color FromHsv(double hue, double saturation, double value)
		{
			if (hue < 0 || hue >= 6) throw new ArgumentOutOfRangeException("hue");
			if (saturation < 0 || saturation >= 1) throw new ArgumentOutOfRangeException("saturation");
			if (value < 0 || value >= 1) throw new ArgumentOutOfRangeException("value");

			int hueIndex = (int)hue;
			double hueFraction = hue - hueIndex;

			double bottom = value * (1 - saturation);
			double top = value;
			double rising = value * (1 - (1 - hueFraction) * saturation);
			double falling = value * (1 - hueFraction * saturation);

			double r, g, b;

			switch (hueIndex)
			{
				case 0: r = top; g = rising; b = bottom; break;
				case 1: r = falling; g = top; b = bottom; break;
				case 2: r = bottom; g = top; b = rising; break;
				case 3: r = bottom; g = falling; b = top; break;
				case 4: r = rising; g = bottom; b = top; break;
				case 5: r = top; g = bottom; b = falling; break;
				default: throw new InvalidOperationException();
			}

			return Color.FromArgb((int)(r * 0x100), (int)(g * 0x100), (int)(b * 0x100));
		}
	}
}
