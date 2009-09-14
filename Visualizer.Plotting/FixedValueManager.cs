namespace Visualizer.Plotting
{
	public class FixedValueManager : ValueManager
	{
		readonly double rangeLow;
		readonly double rangeHigh;

		public override _Range<double> Range
		{
			get
			{
				return new _Range<double>
				(
					new Marker<double>(rangeLow, 0),
					new Marker<double>(rangeHigh, 1)
				);
			}
		}

		public FixedValueManager(double rangeLow, double rangeHigh)
		{
			this.rangeLow = rangeLow;
			this.rangeHigh = rangeHigh;
		}
	}
}
