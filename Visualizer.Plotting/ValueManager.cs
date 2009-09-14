namespace Visualizer.Plotting
{
	public abstract class ValueManager
	{
		public abstract _Range<double> Range { get; }

		public virtual void Update() { }
	}
}
