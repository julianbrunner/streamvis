namespace Visualizer.Plotting
{
	public struct Marker<T>
	{
		public readonly T Value;
		public readonly float Position;

		public Marker(T value, float position)
		{
			this.Value = value;
			this.Position = position;
		}
	}
}
