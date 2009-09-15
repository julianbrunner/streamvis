using System;
using Visualizer.Data;
using Visualizer.Plotting.Timing;

namespace Visualizer.Plotting.Data
{
	public class PerPixelDataManager : DataManager
	{
		readonly double sampleFrequency;
		readonly Layouter layouter;
		readonly TimeManager timeManager;

		public PerPixelDataManager(EntryData entryData, double sampleFrequency, Layouter layouter, TimeManager timeManager)
			: base(entryData)
		{
			this.sampleFrequency = sampleFrequency;
			this.layouter = layouter;
			this.timeManager = timeManager;

			this.layouter.ViewportChanged += layouter_ViewportChanged;

			layouter_ViewportChanged(this, EventArgs.Empty);
		}

		void layouter_ViewportChanged(object sender, EventArgs e)
		{
			int width = layouter.Area.Width;
			Time time = timeManager.Range.Range.End - timeManager.Range.Range.Start;

			double pixelsPerSecond = width / time.Seconds;

			EntryResampler.SampleDistance = new Time(1.0) / (sampleFrequency * pixelsPerSecond);
		}
	}
}