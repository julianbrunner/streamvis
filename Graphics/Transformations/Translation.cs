using System.Drawing;
using OpenTK.Graphics;

namespace Graphics.Transformations
{
	public class Translation : Transformation
	{
		readonly PointF offset;

		public Translation(PointF offset)
		{
			this.offset = offset;
		}

		public override void Apply()
		{
			GL.Translate(offset.X, offset.Y, 0);
		}
	}
}
