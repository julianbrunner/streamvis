using System.Drawing;

namespace Visualizer.Plotting
{
	public class Layouter
	{
		const int borderLeft = 50;
		const int borderRight = 0;
		const int borderTop = 20;
		const int borderBottom = 25;

		readonly IDrawer drawer;

		Rectangle graphsArea;

		public Layouter(IDrawer drawer)
		{
			this.drawer = drawer;
		}

		public virtual void Update()
		{
			Rectangle area = drawer.Area;

			this.graphsArea = new Rectangle
			(
				area.Left + borderLeft,
				area.Top + borderTop,
				area.Width - borderLeft - borderRight,
				area.Height - borderTop - borderBottom
			);
		}
		public PointF TransformGraph(float x, float y)
		{
			return new PointF
			(
				graphsArea.Left + x * graphsArea.Width,
				graphsArea.Bottom - y * graphsArea.Height
			);
		}
	}
}
