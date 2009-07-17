namespace Visualizer.Plotting
{
	public struct Range<T>
	{
		public readonly Marker<T> Start;
		public readonly Marker<T> End;

		public Range(Marker<T> start, Marker<T> end)
		{
			this.Start = start;
			this.End = end;
		}

		public float Map(float value)
		{
			return Start.Position + value * (End.Position - Start.Position);
		}
	}
}
