// Copyright � Julian Brunner 2009

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
using System.Drawing;
using System.Globalization;
using Visualizer.Data;

namespace Visualizer
{
	class Parameters
	{
		readonly List<string> ports = new List<string>();
		readonly bool minimalMode = false;
		readonly Time diagramWidth = new Time(10.0);
		readonly double lineWidth = 1;
		readonly bool extendGraphs = true;
		readonly bool lineSmoothing = true;
		readonly DiagramType diagramType = DiagramType.Continuous;
		readonly double diagramTypeParameter = 0;
		readonly double rangeLow = 0;
		readonly double rangeHigh = 0;
		readonly SamplerType samplerType = SamplerType.PerPixel;
		readonly double samplerFrequency = 1;
		readonly int intervalsX = 5;
		readonly int intervalsY = 5;
		readonly Color diagramColor = Color.White;
		readonly Color backgroundColor = Color.Black;

		public IEnumerable<string> Ports { get { return ports; } }
		public bool MinimalMode { get { return minimalMode; } }
		public Time DiagramWidth { get { return diagramWidth; } }
		public double LineWidth { get { return lineWidth; } }
		public bool ExtendGraphs { get { return extendGraphs; } }
		public bool LineSmoothing { get { return lineSmoothing; } }
		public DiagramType DiagramType { get { return diagramType; } }
		public double DiagramTypeParameter { get { return diagramTypeParameter; } }
		public double RangeLow { get { return rangeLow; } }
		public double RangeHigh { get { return rangeHigh; } }
		public SamplerType SamplerType { get { return samplerType; } }
		public double SamplerFrequency { get { return samplerFrequency; } }
		public int IntervalsX { get { return intervalsX; } }
		public int IntervalsY { get { return intervalsY; } }
		public Color DiagramColor { get { return diagramColor; } }
		public Color BackgroundColor { get { return backgroundColor; } }

		public Parameters(IEnumerable<string> parameters)
		{
			foreach (string parameter in parameters)
			{
				string[] details = parameter.Split(':');
				switch (details[0])
				{
					case "-m":
						if (details.Length != 1) InvalidParameter(parameter);
						minimalMode = true;
						break;
					case "-w":
						if (details.Length != 2) InvalidParameter(parameter);
						try { diagramWidth = new Time(double.Parse(details[1])); }
						catch (FormatException) { InvalidParameter(parameter); }
						break;
					case "-l":
						if (details.Length != 2) InvalidParameter(parameter);
						try { lineWidth = double.Parse(details[1]); }
						catch (FormatException) { InvalidParameter(parameter); }
						break;
					case "-noe":
						if (details.Length != 1) InvalidParameter(parameter);
						extendGraphs = false;
						break;
					case "-noa":
						if (details.Length != 1) InvalidParameter(parameter);
						lineSmoothing = false;
						break;
					case "-t":
						if (details.Length < 2) InvalidParameter(parameter);
						switch (details[1])
						{
							case "c":
								if (details.Length > 2) InvalidParameter(parameter);
								diagramType = DiagramType.Continuous;
								break;
							case "s":
								if (details.Length > 3) InvalidParameter(parameter);
								diagramType = DiagramType.Shiftting;
								try { diagramTypeParameter = details.Length > 2 ? double.Parse(details[2]) : 0.8; }
								catch (FormatException) { InvalidParameter(parameter); }
								break;
							case "w":
								if (details.Length > 3) InvalidParameter(parameter);
								diagramType = DiagramType.Wrapping;
								try { diagramTypeParameter = details.Length > 2 ? double.Parse(details[2]) : 0.2; }
								catch (FormatException) { InvalidParameter(parameter); }
								break;
							default: throw new InvalidOperationException("Invalid diagram type: " + details[1]);
						}
						break;
					case "-r":
						if (details.Length != 3) InvalidParameter(parameter);
						try
						{
							rangeLow = double.Parse(details[1]);
							rangeHigh = double.Parse(details[2]);
						}
						catch (FormatException) { InvalidParameter(parameter); }
						break;
					case "-s":
						if (details.Length < 2) InvalidParameter(parameter);
						switch (details[1])
						{
							case "s":
								if (details.Length > 3) InvalidParameter(parameter);
								samplerType = SamplerType.PerSecond;
								try { samplerFrequency = details.Length > 2 ? double.Parse(details[2]) : 10; }
								catch (FormatException) { InvalidParameter(parameter); }
								break;
							case "p":
								if (details.Length > 3) InvalidParameter(parameter);
								samplerType = SamplerType.PerPixel;
								try { samplerFrequency = details.Length > 2 ? double.Parse(details[2]) : 0.1; }
								catch (FormatException) { InvalidParameter(parameter); }
								break;
							default: throw new InvalidOperationException("Invalid sampler type: " + details[1]);
						}
						break;
					case "-ix":
						if (details.Length != 2) InvalidParameter(parameter);
						try { intervalsX = int.Parse(details[1]); }
						catch (FormatException) { InvalidParameter(parameter); }
						break;
					case "-iy":
						if (details.Length != 2) InvalidParameter(parameter);
						try { intervalsY = int.Parse(details[1]); }
						catch (FormatException) { InvalidParameter(parameter); }
						break;
					case "-pc":
						if (details.Length != 2) InvalidParameter(parameter);
						try { diagramColor = HtmlStringToColor(details[1]); }
						catch (FormatException) { InvalidParameter(parameter); }
						catch (ArgumentOutOfRangeException) { InvalidParameter(parameter); }
						break;
					case "-bc":
						if (details.Length != 2) InvalidParameter(parameter);
						try { backgroundColor = HtmlStringToColor(details[1]); }
						catch (FormatException) { InvalidParameter(parameter); }
						catch (ArgumentOutOfRangeException) { InvalidParameter(parameter); }
						break;
					default: ports.Add(parameter); break;
				}
			}
		}

		static Color HtmlStringToColor(string htmlString)
		{
			if (htmlString.Length != 6) throw new ArgumentOutOfRangeException("htmlString");

			try
			{
				byte red = byte.Parse(htmlString.Substring(0, 2), NumberStyles.HexNumber);
				byte green = byte.Parse(htmlString.Substring(2, 2), NumberStyles.HexNumber);
				byte blue = byte.Parse(htmlString.Substring(4, 2), NumberStyles.HexNumber);
				return Color.FromArgb(red, green, blue);
			}
			catch (FormatException) { throw new ArgumentOutOfRangeException("htmlString"); }
		}
		static void InvalidParameter(string parameter)
		{
			throw new InvalidOperationException("Invalid parameter: \"" + parameter + "\"");
		}
	}
}
