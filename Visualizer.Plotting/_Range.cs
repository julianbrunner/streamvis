namespace Visualizer.Plotting
{
	public struct _Range<T>
	{
		readonly Marker<T> start;
		readonly Marker<T> end;

		public Marker<T> Start { get { return start; } }
		public Marker<T> End { get { return end; } }

		public _Range(Marker<T> start, Marker<T> end)
		{
			this.start = start;
			this.end = end;
		}

		public float Map(float value)
		{
			return start.Position + value * (end.Position - start.Position);
		}
	}
}
