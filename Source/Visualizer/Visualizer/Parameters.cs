// Copyright © Julian Brunner 2009 - 2010

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
using Utility;
using Utility.Utilities;

namespace Visualizer
{
	class Parameters
	{
		readonly List<string> portStrings = new List<string>();

		public IEnumerable<string> PortStrings { get { return portStrings; } }
		public bool? MinimalMode { get; private set; }
		public bool? ExtendGraphs { get; private set; }
		public bool? ClearData { get; private set; }
		public TimeManagerType TimeManagerType { get; private set; }
		public double? TimeManagerParameter { get; private set; }
		public double? DiagramWidth { get; private set; }
		public ValueManagerType ValueManagerType { get; private set; }
		public Range<double>? ValueRange { get; private set; }
		public double? LineWidth { get; private set; }
		public int? MarkerCountX { get; private set; }
		public int? MarkerCountY { get; private set; }
		public Color? DiagramColor { get; private set; }
		public Color? BackgroundColor { get; private set; }

		public Parameters(IEnumerable<string> parameters)
		{
			TimeManagerType = TimeManagerType.Continuous;
			ValueManagerType = ValueManagerType.Fitting;

			foreach (string parameter in parameters)
			{
				switch (parameter.Substring(0, 1))
				{
					case ":": portStrings.Add(parameter.Substring(1)); break;
					case "+": ParseBooleanOption(parameter.Substring(1), true); break;
					case "-": ParseBooleanOption(parameter.Substring(1), false); break;
					default: ParseOption(parameter); break;
				}
			}
		}

		void ParseBooleanOption(string name, bool value)
		{
			switch (name)
			{
				case "m": MinimalMode = value; break;
				case "e": ExtendGraphs = value; break;
				case "c": ClearData = value; break;
				default: InvalidParameter((value ? "+" : "-") + name); break;
			}
		}
		void ParseOption(string option)
		{
			string[] details = option.Split(':');

			switch (details[0])
			{
				case "t":
					if (details.Length < 2) InvalidParameter(option);
					switch (details[1])
					{
						case "c":
							if (details.Length > 2) InvalidParameter(option);
							TimeManagerType = TimeManagerType.Continuous;
							break;
						case "s":
							if (details.Length > 3) InvalidParameter(option);
							TimeManagerType = TimeManagerType.Shiftting;
							if (details.Length > 2)
								try { TimeManagerParameter = double.Parse(details[2]); }
								catch (FormatException) { InvalidParameter(option); }
							break;
						case "w":
							if (details.Length > 3) InvalidParameter(option);
							TimeManagerType = TimeManagerType.Wrapping;
							if (details.Length > 2)
								try { TimeManagerParameter = double.Parse(details[2]); }
								catch (FormatException) { InvalidParameter(option); }
							break;
						default: throw new InvalidOperationException("Invalid time manager type: " + details[1]);
					}
					break;
				case "w":
					if (details.Length != 2) InvalidParameter(option);
					try { DiagramWidth = double.Parse(details[1]); }
					catch (FormatException) { InvalidParameter(option); }
					break;
				case "v":
					if (details.Length < 2) InvalidParameter(option);
					switch (details[1])
					{
						case "d":
							if (details.Length > 2) InvalidParameter(option);
							ValueManagerType = ValueManagerType.Fitting;
							break;
						case "s":
							if (details.Length != 2 && details.Length != 4) InvalidParameter(option);
							ValueManagerType = ValueManagerType.Fixed;
							if (details.Length == 4)
								try { ValueRange = new Range<double>(double.Parse(details[2]), double.Parse(details[3])); }
								catch (FormatException) { InvalidParameter(option); }
							break;
						default: throw new InvalidOperationException("Invalid value manager type: " + details[1]);
					}
					break;
				case "l":
					if (details.Length != 2) InvalidParameter(option);
					try { LineWidth = double.Parse(details[1]); }
					catch (FormatException) { InvalidParameter(option); }
					break;
				case "mx":
					if (details.Length != 2) InvalidParameter(option);
					try { MarkerCountX = int.Parse(details[1]); }
					catch (FormatException) { InvalidParameter(option); }
					break;
				case "my":
					if (details.Length != 2) InvalidParameter(option);
					try { MarkerCountY = int.Parse(details[1]); }
					catch (FormatException) { InvalidParameter(option); }
					break;
				case "pc":
					if (details.Length != 2) InvalidParameter(option);
					try { DiagramColor = ColorUtility.FromHtmlString(details[1]); }
					catch (FormatException) { InvalidParameter(option); }
					catch (ArgumentOutOfRangeException) { InvalidParameter(option); }
					break;
				case "bc":
					if (details.Length != 2) InvalidParameter(option);
					try { BackgroundColor = ColorUtility.FromHtmlString(details[1]); }
					catch (FormatException) { InvalidParameter(option); }
					catch (ArgumentOutOfRangeException) { InvalidParameter(option); }
					break;
				default: InvalidParameter(option); break;
			}
		}

		static void InvalidParameter(string parameter)
		{
			throw new InvalidOperationException("Invalid parameter: \"" + parameter + "\"");
		}
	}
}
