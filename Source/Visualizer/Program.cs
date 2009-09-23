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
using System.Windows.Forms;

namespace Visualizer
{
	static class Program
	{
		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		[STAThread]
		static void Main(string[] args)
		{
			if (args.Contains("--help"))
			{
				const int column1 = 0;
				const int column2 = 2;
				const int column3 = 6;
				const int column4 = 25;

				#region Help Message
				Console.WriteLine();

				Write(column1, "Stream Visualizer is an application that can be used to visualize, capture and replay data streams.");
				Console.WriteLine();
				Console.WriteLine();

				Write(column1, "Parameters:");
				Console.WriteLine();
				Console.WriteLine();

				Write(column2, "<port>");
				Write(column4, "Specifies that all streams of <port> should be included in the stream selection list. A test bottle will be retrieved from the specified port to build a list of possible streams before the visualization begins.");
				Console.WriteLine();
				Console.WriteLine();

				Write(column2, "<port>:<streams>");
				Write(column4, "Specifies that the <streams> of <port> should be included in the stream selection list.");
				Console.WriteLine();
				Console.WriteLine();
				Write(column3, "<streams>");
				Write(column4, "<range>,<range>,...");
				Console.WriteLine();
				Console.WriteLine();
				Write(column3, "<range>");
				Write(column4, "<path>|<path>-<path>");
				Console.WriteLine();
				Console.WriteLine();
				Write(column3, "<path>");
				Write(column4, "A path to the stream, subpaths are seperated by dots. For example \"1.2.3\" specifies that the stream should be generated from the third value in the second bottle in the first bottle of the port.");
				Console.WriteLine();
				Console.WriteLine();

				Write(column2, "+m|-m");
				Write(column4, "Enables or disables minimal mode. In minimal mode, everything except for the graph area is hidden from the user interface to maximize the available drawing-space. The default values is disabled.");
				Console.WriteLine();
				Console.WriteLine();

				Write(column2, "w:<width>");
				Write(column4, "Lets you specify the width of the drawing-area in total seconds. The default value is \"10\".");
				Console.WriteLine();
				Console.WriteLine();

				Write(column2, "l:<width>");
				Write(column4, "Lets you specify the width (thickness) of the graph lines in pixels. The default value is \"1.0\".");
				Console.WriteLine();
				Console.WriteLine();

				Write(column2, "+e|-e");
				Write(column4, "Enables or disables the graph extension feature. When graph extension is enabled, all graphs that have at least one sampled data entry are extended across the whole width of the coordinate system using known adjacent values. The default values is enabled.");
				Console.WriteLine();
				Console.WriteLine();

				Write(column2, "+a|-a");
				Write(column4, "Enables or disables antialiasing of graph lines. The default values is enabled.");
				Console.WriteLine();
				Console.WriteLine();

				Write(column2, "+v|-v");
				Write(column4, "Enables or disables vertical synchronization. The default values is enabled.");
				Console.WriteLine();
				Console.WriteLine();

				Write(column2, "+l|-l");
				Write(column4, "Enables or disables the logging of all received data. Disable if you plan to run the visualizer for a long time and you don't want to save the results. The default values is enabled.");
				Console.WriteLine();
				Console.WriteLine();

				Write(column2, "t:(c|s:[<c>]|w:[<c>])");
				Write(column4, "Lets you specify the type of diagram that is used. The default value is \"c\".");
				Console.WriteLine();
				Console.WriteLine();
				Write(column3, "c");
				Write(column4, "Continuous diagram. The graphs are drawn across the entire drawing-area, the most recent data entry lies on the right border of the drawing-area.");
				Console.WriteLine();
				Console.WriteLine();
				Write(column3, "s:[<c>]");
				Write(column4, "Shifting diagram. The graphs will shift <c> times the drawing-area width to the left whenever the most recent data entry reaches the right border of the drawing-area. The default value for <c> is \"0.8\".");
				Console.WriteLine();
				Console.WriteLine();
				Write(column3, "w:[<c>]");
				Write(column4, "Wrapping diagram. The graphs don't move at all, once the most recent data entry reaches the right border, the graphs wrap around and draw over the oldest entries starting from the left. The most recent data entry will push a gap of <c> times the drawing-area width in front of it. The default value for <c> is \"0.2\".");
				Console.WriteLine();
				Console.WriteLine();

				Write(column2, "r:<low>:<high>");
				Write(column4, "Fixes the value range of the diagram. By default, the value range is automatically fitted to the displayed graphs.");
				Console.WriteLine();
				Console.WriteLine();

				Write(column2, "s:(s:[<c>]|p:[<c>])");
				Write(column4, "Lets you specify the sampler type and settings used for drawing graphs. The default value is \"p:1\".");
				Console.WriteLine();
				Console.WriteLine();
				Write(column3, "s:[<c>]");
				Write(column4, "Per-Second sampler. The data will be resampled to <c> entries per second. The default value for <c> is \"10\".");
				Console.WriteLine();
				Console.WriteLine();
				Write(column3, "p:[<c>]");
				Write(column4, "Per-Pixel sampler. The data will be resampled to <c> entries per pixel. The default value for <c> is \"0.1\".");
				Console.WriteLine();
				Console.WriteLine();

				Write(column2, "ix:<n>");
				Write(column4, "Sets the number of intervals that the X-Axis is divided into. The default value is \"5\".");
				Console.WriteLine();
				Console.WriteLine();

				Write(column2, "iy:<n>");
				Write(column4, "Sets the number of intervals that the Y-Axis is divided into. The default value is \"5\".");
				Console.WriteLine();
				Console.WriteLine();

				Write(column2, "pc:<color>");
				Write(column4, "Sets the diagram color (coordinate system and labels) in HTML notation. The default value is \"FFFFFF\".");
				Console.WriteLine();
				Console.WriteLine();

				Write(column2, "bc:<color>");
				Write(column4, "Sets the background color in HTML notation. The default value is \"000000\".");
				Console.WriteLine();
				Console.WriteLine();
				#endregion

				return;
			}

			Console.WriteLine("Initializing...");

			Parameters parameters;

			try { parameters = new Parameters(args); }
			catch (InvalidOperationException e) { Console.WriteLine(e.Message); return; }

			Application.EnableVisualStyles();
			Application.SetCompatibleTextRenderingDefault(false);
			Application.Run(new MainWindow(parameters));
		}
		static void Write(int column, string text)
		{
			Console.CursorLeft = column;
			foreach (string word in text.Split(' '))
			{
				if (Console.CursorLeft + word.Length + 1 >= Console.BufferWidth)
				{
					Console.WriteLine();
					Console.CursorLeft = column;
				}

				Console.Write(word);
				Console.Write(" ");
			}
		}
	}
}
