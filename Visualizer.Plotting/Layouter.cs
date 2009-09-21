using System.Drawing;
using Graphics;
using OpenTK.Math;

namespace Visualizer.Plotting
{
	public class Layouter
	{
		const int borderLeft = 80;
		const int borderRight = 0;
		const int borderTop = 7;
		const int borderBottom = 25;

		readonly Viewport viewport;

		public PointF this[double x, double y]
		{
			get
			{
				return new PointF
				(
					(float)(Area.Left + x * Area.Width),
					(float)(Area.Bottom - y * Area.Height)
				);
			}
		}

		public Rectangle Area { get; private set; }
		public Matrix4 Transformation { get; private set; }

		public Layouter(Viewport viewport)
		{
			this.viewport = viewport;
		}

		public virtual void Update()
		{
			Rectangle clientArea = viewport.ClientRectangle;

			Area = new Rectangle
			(
				clientArea.Left + borderLeft,
				clientArea.Top + borderTop,
				clientArea.Width - borderLeft - borderRight,
				clientArea.Height - borderTop - borderBottom
			);

			Transformation = Matrix4.Scale(Area.Width, -Area.Height, 1) * Matrix4.CreateTranslation(Area.Left, Area.Bottom, 0);
		}
	}
}
