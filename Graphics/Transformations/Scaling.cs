using OpenTK.Graphics;
using System.Drawing;

namespace Graphics.Transformations
{
	public class Scaling : Transformation
	{
		readonly PointF factor;
		
		public Scaling(PointF factor)
		{
			this.factor = factor;
		}
		
		public override void Apply()
		{
			GL.Scale(factor.X, factor.Y, 0);
		}
	}
}