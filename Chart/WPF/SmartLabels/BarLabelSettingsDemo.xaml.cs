using System.Windows.Controls;

namespace SmartLabels
{
	public partial class BarLabelSettingsDemo : UserControl
	{
		public BarLabelSettingsDemo()
		{
			InitializeComponent();
			this.DataContext = new int[] { 1, 5, 6, 9, 5, 7 };
		}
	}
}
