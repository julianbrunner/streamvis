namespace Visualizer.Plotting
{
	public struct Marker<T>
	{
		readonly T value;
		readonly float position;

		public T Value { get { return value; } }
		public float Position { get { return position; } }

		public Marker(T value, float position)
		{
			this.value = value;
			this.position = position;
		}
	}
}
