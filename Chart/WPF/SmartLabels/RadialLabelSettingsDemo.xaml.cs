using System.Windows.Controls;

namespace SmartLabels
{
	public partial class RadialLabelSettingsDemo : UserControl
	{
		public RadialLabelSettingsDemo()
		{
			InitializeComponent();
			this.DataContext = new int[] { 1, 5, 6, 9, 5, 7 };
		}
	}
}
