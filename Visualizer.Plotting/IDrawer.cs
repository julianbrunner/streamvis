using System.Collections.Generic;
using System.Drawing;

namespace Visualizer.Plotting
{
	public interface IDrawer
	{
		Rectangle Area { get; }

		void Begin();
		void End();
		void DrawNumber(double number, PointF position, Color color, TextAlignment alignment);
		void DrawLineStrip(IEnumerable<PointF> points, Color color, float width);
		void DrawLine(PointF from, PointF to, Color color, float width);
	}
}
