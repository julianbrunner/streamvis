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
using System.IO;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;
using System.Xml.Linq;
using Krach.Extensions;

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
				using (Stream stream = Assembly.GetExecutingAssembly().GetManifestResourceStream("Visualizer.Text.Help Message.xml"))
				using (TextReader reader = new StreamReader(stream))
				{
					XElement helpMessage = XElement.Load(reader);

					IDictionary<string, int> columns =
					(
						from column in helpMessage.Element("Columns").Elements()
						select new { Name = (string)column.Element("Name"), Value = (int)column.Element("Value") }
					)
					.ToDictionary(column => column.Name, column => column.Value);

					foreach (XElement textElement in helpMessage.Element("Data").Elements())
					{
						if (textElement.Name == "Text") Terminal.Write(columns[(string)textElement.Attribute("Column")], (string)textElement);
						if (textElement.Name == "NewLine") Console.WriteLine();
					}
				}

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
	}
}
