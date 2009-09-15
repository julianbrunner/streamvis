using System.Windows.Forms;
using Graphics;
using Visualizer.Data;

namespace Visualizer.Plotting.Data
{
	public class PerPixelDataManager : DataManager
	{
		double sampleFrequency;

		public PerPixelDataManager(EntryData entryData, double sampleFrequency, Viewport viewport)
			: base(entryData)
		{
			this.sampleFrequency = sampleFrequency;

			viewport.Layout += viewport_Layout;
		}

		void viewport_Layout(object sender, LayoutEventArgs e)
		{
			
		}
	}
}