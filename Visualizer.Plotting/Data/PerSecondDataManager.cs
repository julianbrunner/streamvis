using Visualizer.Data;

namespace Visualizer.Plotting.Data
{
	public class PerSecondDataManager : DataManager
	{
		public PerSecondDataManager(EntryData entryData, double sampleFrequency)
			: base(entryData)
		{
			EntryResampler.SampleDistance = new Time(1.0) / sampleFrequency;
		}
	}
}
