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
using System.Drawing;
using System.Windows.Forms;
using Graphics;

namespace Visualizer.Environment
{
	[TypeConverter(typeof(ExpansionConverter))]
	class RectangleSelectorSettings
	{
		readonly RectangleSelector rectangleSelector;

		[DisplayName("Update")]
		public bool IsUpdated
		{
			get { return rectangleSelector.IsUpdated; }
			set { rectangleSelector.IsUpdated = value; }
		}
		[DisplayName("Draw")]
		public bool IsDrawn
		{
			get { return rectangleSelector.IsDrawn; }
			set { rectangleSelector.IsDrawn = value; }
		}
		[DisplayName("Button")]
		public MouseButtons Button
		{
			get { return rectangleSelector.Button; }
			set { rectangleSelector.Button = value; }
		}
		[DisplayName("Color")]
		public Color Color
		{
			get { return rectangleSelector.Color; }
			set { rectangleSelector.Color = value; }
		}
		[DisplayName("Width")]
		public float Width
		{
			get { return rectangleSelector.Width; }
			set { rectangleSelector.Width = value; }
		}

		public RectangleSelectorSettings(RectangleSelector rectangleSelector)
		{
			this.rectangleSelector = rectangleSelector;
		}
	}
}
