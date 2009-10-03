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

using System.ComponentModel;
using Visualizer.Drawing;

namespace Visualizer.Environment
{
	// TODO: Add description for properties
	[TypeConverter(typeof(ExpandableObjectConverter))]
	class GraphSettingsSettings
	{
		readonly GraphSettings graphSettings;

		[DisplayName("Extend Graphs")]
		public bool ExtendGraphs
		{
			get { return graphSettings.ExtendGraphs; }
			set { graphSettings.ExtendGraphs = value; }
		}
		[DisplayName("Line Width")]
		public double LineWidth
		{
			get { return graphSettings.LineWidth; }
			set { graphSettings.LineWidth = value; }
		}

		public GraphSettingsSettings(GraphSettings graphSettings)
		{
			this.graphSettings = graphSettings;
		}
	}
}
