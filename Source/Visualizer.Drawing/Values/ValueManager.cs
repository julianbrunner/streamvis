namespace Visualizer.Drawing.Values
{
	public abstract class ValueManager
	{
		public abstract ValueRange Range { get; }

		public virtual void Update() { }
	}
}
