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
		readonly Time plotterWidth = new Time(10.0);
		readonly bool extendGraphs = true;
		readonly PlotterType plotterType = PlotterType.Continuous;
		readonly double plotterTypeParameter = 0;
		readonly double rangeLow = 0;
		readonly double rangeHigh = 0;
		readonly SamplerType samplerType = SamplerType.PerSecond;
		readonly double samplerFrequency = 10;
		readonly int intervalsX = 5;
		readonly int intervalsY = 5;
		readonly Color plotterColor = Color.White;
		readonly Color backgroundColor = Color.Black;

		public IEnumerable<string> Ports { get { return ports; } }
		public bool MinimalMode { get { return minimalMode; } }
		public Time PlotterWidth { get { return plotterWidth; } }
		public bool ExtendGraphs { get { return extendGraphs; } }
		public PlotterType PlotterType { get { return plotterType; } }
		public double PlotterTypeParameter { get { return plotterTypeParameter; } }
		public double RangeLow { get { return rangeLow; } }
		public double RangeHigh { get { return rangeHigh; } }
		public SamplerType SamplerType { get { return samplerType; } }
		public double SamplerFrequency { get { return samplerFrequency; } }
		public int IntervalsX { get { return intervalsX; } }
		public int IntervalsY { get { return intervalsY; } }
		public Color PlotterColor { get { return plotterColor; } }
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
						try { plotterWidth = new Time(double.Parse(details[1])); }
						catch (FormatException) { InvalidParameter(parameter); }
						break;
					case "-noe":
						if (details.Length != 1) InvalidParameter(parameter);
						extendGraphs = false;
						break;
					case "-t":
						if (details.Length < 2) InvalidParameter(parameter);
						switch (details[1])
						{
							case "c":
								if (details.Length > 2) InvalidParameter(parameter);
								plotterType = PlotterType.Continuous;
								break;
							case "s":
								if (details.Length > 3) InvalidParameter(parameter);
								plotterType = PlotterType.Shiftting;
								try { plotterTypeParameter = details.Length > 2 ? double.Parse(details[2]) : 0.8; }
								catch (FormatException) { InvalidParameter(parameter); }
								break;
							case "w":
								if (details.Length > 3) InvalidParameter(parameter);
								plotterType = PlotterType.Wrapping;
								try { plotterTypeParameter = details.Length > 2 ? double.Parse(details[2]) : 0.2; }
								catch (FormatException) { InvalidParameter(parameter); }
								break;
							default: throw new InvalidOperationException("Invalid plotter type: " + details[1]);
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
						try { plotterColor = HtmlStringToColor(details[1]); }
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
