using System.Windows.Forms;

namespace Visualizer
{
	partial class TextDialog : Form
	{
		public string Result { get { return textBox.Text; } }

		public TextDialog(string title, string description, string text)
		{
			InitializeComponent();

			Text = title;
			descriptionLabel.Text = description;
			textBox.Text = text;
		}
	}
}
