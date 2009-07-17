namespace Visualizer.Plotting
{
	public abstract class ValueManager
	{
		public abstract Range<double> Range { get; }

		public virtual void Update() { }
	}
}
