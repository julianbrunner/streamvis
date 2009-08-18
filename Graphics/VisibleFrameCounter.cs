using System.Drawing;

namespace Graphics
{
	public class VisibleFrameCounter : FrameCounter, IDrawable
	{
		readonly Drawer drawer;
		readonly Color color;
		readonly TextAlignment alignment;
		
		public PointF Position { get; set; }
		public bool IsDrawn { get; set; }
		
		public VisibleFrameCounter(Drawer drawer, Color color, TextAlignment alignment)
		{
			this.drawer = drawer;
			this.color = color;
			this.alignment = alignment;

			Position = Point.Empty;
			IsDrawn = true;
		}
		
		public void Draw()
		{
			if (IsDrawn) drawer.DrawNumber(FramesPerSecond, Position, color, alignment);
		}
	}
}
