using System.Drawing;
using Graphics;

namespace Visualizer.Plotting
{
	public class Layouter
	{
		const int borderLeft = 80;
		const int borderRight = 0;
		const int borderTop = 20;
		const int borderBottom = 25;

		readonly Viewport viewport;

		Rectangle graphsArea;
		
		public Rectangle GraphsArea { get { return graphsArea; } }

		public Layouter(Viewport viewport)
		{
			this.viewport = viewport;
		}

		public virtual void Update()
		{
			// TODO: Try and do this in a more straightforward way if performance allows it
			
			Rectangle area = viewport.ClientRectangle;

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
