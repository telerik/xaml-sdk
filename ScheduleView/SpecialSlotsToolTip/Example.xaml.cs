using System.Windows.Controls;

namespace SpecialSlotsToolTip
{
	/// <summary>
	/// Interaction logic for Example.xaml
	/// </summary>
	public partial class Example : UserControl
	{
		public Example()
        {
            this.DataContext = new ViewModel();
			InitializeComponent();
		}
	}
}
