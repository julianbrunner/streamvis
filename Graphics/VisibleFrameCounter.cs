using System.Drawing;

namespace Graphics
{
	public class VisibleFrameCounter : FrameCounter
	{
		readonly Viewport viewport;
		readonly Color color;
		readonly TextAlignment alignment;
		
		public PointF Position { get; set; }
		
		public VisibleFrameCounter(Viewport viewport, Color color, TextAlignment alignment)
		{
			this.viewport = viewport;
			this.color = color;
			this.alignment = alignment;
		}
		
		public void Draw()
		{
			viewport.DrawNumber(FramesPerSecond, Position, color, alignment);
		}
	}
}
