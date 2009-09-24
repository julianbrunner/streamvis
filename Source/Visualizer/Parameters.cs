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
using System.Drawing;
using System.Globalization;
using Visualizer.Data;

namespace Visualizer
{
	class Parameters
	{
		readonly List<string> ports = new List<string>();

		public IEnumerable<string> Ports { get { return ports; } }
		public bool MinimalMode { get; private set; }
		public bool ExtendGraphs { get; private set; }
		public bool LineSmoothing { get; private set; }
		public bool AlphaBlending { get; private set; }
		public bool VerticalSynchronization { get; private set; }
		public bool DataLogging { get; private set; }
		public Time DiagramWidth { get; private set; }
		public double LineWidth { get; private set; }
		public DiagramType DiagramType { get; private set; }
		public double DiagramTypeParameter { get; private set; }
		public double RangeLow { get; private set; }
		public double RangeHigh { get; private set; }
		public SamplerType SamplerType { get; private set; }
		public double SamplerFrequency { get; private set; }
		public int MarkersX { get; private set; }
		public int MarkersY { get; private set; }
		public Color DiagramColor { get; private set; }
		public Color BackgroundColor { get; private set; }

		public Parameters(IEnumerable<string> parameters)
		{
			MinimalMode = false;
			DiagramWidth = new Time(10.0);
			LineWidth = 1;
			ExtendGraphs = true;
			LineSmoothing = true;
			AlphaBlending = true;
			VerticalSynchronization = true;
			DataLogging = true;
			DiagramType = DiagramType.Continuous;
			DiagramTypeParameter = 0;
			RangeLow = 0;
			RangeHigh = 0;
			SamplerType = SamplerType.PerPixel;
			SamplerFrequency = 1;
			MarkersX = 5;
			MarkersY = 5;
			DiagramColor = Color.White;
			BackgroundColor = Color.Black;

			foreach (string parameter in parameters)
			{
				string[] details = parameter.Split(':');
				switch (details[0][0])
				{
					case '/': ports.Add(parameter); break;
					case '+':
						if (details.Length != 1) InvalidParameter(parameter);
						ParseBooleanOption(details[0].Substring(1), true);
						break;
					case '-':
						if (details.Length != 1) InvalidParameter(parameter);
						ParseBooleanOption(details[0].Substring(1), false);
						break;
					default: ParseOption(parameter); break;
				}
			}
		}

		void ParseOption(string option)
		{
			string[] details = option.Split(':');

			switch (details[0])
			{
				case "w":
					if (details.Length != 2) InvalidParameter(option);
					try { DiagramWidth = new Time(double.Parse(details[1])); }
					catch (FormatException) { InvalidParameter(option); }
					break;
				case "l":
					if (details.Length != 2) InvalidParameter(option);
					try { LineWidth = double.Parse(details[1]); }
					catch (FormatException) { InvalidParameter(option); }
					break;
				case "t":
					if (details.Length < 2) InvalidParameter(option);
					switch (details[1])
					{
						case "c":
							if (details.Length > 2) InvalidParameter(option);
							DiagramType = DiagramType.Continuous;
							break;
						case "s":
							if (details.Length > 3) InvalidParameter(option);
							DiagramType = DiagramType.Shiftting;
							try { DiagramTypeParameter = details.Length > 2 ? double.Parse(details[2]) : 0.8; }
							catch (FormatException) { InvalidParameter(option); }
							break;
						case "w":
							if (details.Length > 3) InvalidParameter(option);
							DiagramType = DiagramType.Wrapping;
							try { DiagramTypeParameter = details.Length > 2 ? double.Parse(details[2]) : 0.2; }
							catch (FormatException) { InvalidParameter(option); }
							break;
						default: throw new InvalidOperationException("Invalid diagram type: " + details[1]);
					}
					break;
				case "r":
					if (details.Length != 3) InvalidParameter(option);
					try
					{
						RangeLow = double.Parse(details[1]);
						RangeHigh = double.Parse(details[2]);
					}
					catch (FormatException) { InvalidParameter(option); }
					break;
				case "s":
					if (details.Length < 2) InvalidParameter(option);
					switch (details[1])
					{
						case "s":
							if (details.Length > 3) InvalidParameter(option);
							SamplerType = SamplerType.PerSecond;
							try { SamplerFrequency = details.Length > 2 ? double.Parse(details[2]) : 10; }
							catch (FormatException) { InvalidParameter(option); }
							break;
						case "p":
							if (details.Length > 3) InvalidParameter(option);
							SamplerType = SamplerType.PerPixel;
							try { SamplerFrequency = details.Length > 2 ? double.Parse(details[2]) : 0.1; }
							catch (FormatException) { InvalidParameter(option); }
							break;
						default: throw new InvalidOperationException("Invalid sampler type: " + details[1]);
					}
					break;
				case "mx":
					if (details.Length != 2) InvalidParameter(option);
					try { MarkersX = int.Parse(details[1]); }
					catch (FormatException) { InvalidParameter(option); }
					break;
				case "my":
					if (details.Length != 2) InvalidParameter(option);
					try { MarkersY = int.Parse(details[1]); }
					catch (FormatException) { InvalidParameter(option); }
					break;
				case "pc":
					if (details.Length != 2) InvalidParameter(option);
					try { DiagramColor = HtmlStringToColor(details[1]); }
					catch (FormatException) { InvalidParameter(option); }
					catch (ArgumentOutOfRangeException) { InvalidParameter(option); }
					break;
				case "bc":
					if (details.Length != 2) InvalidParameter(option);
					try { BackgroundColor = HtmlStringToColor(details[1]); }
					catch (FormatException) { InvalidParameter(option); }
					catch (ArgumentOutOfRangeException) { InvalidParameter(option); }
					break;
				default: InvalidParameter(option); break;
			}
		}
		void ParseBooleanOption(string name, bool value)
		{
			switch (name)
			{
				case "m": MinimalMode = value; break;
				case "e": ExtendGraphs = value; break;
				case "a": LineSmoothing = value; break;
				case "b": AlphaBlending = value; break;
				case "v": VerticalSynchronization = value; break;
				case "l": DataLogging = value; break;
				default: InvalidParameter((value ? "+" : "-") + name); break;
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
