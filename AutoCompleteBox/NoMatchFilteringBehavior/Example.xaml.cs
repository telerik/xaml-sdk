using System.Windows;
using System.Windows.Controls;
using Telerik.Windows.Controls;

namespace NoMatchFilteringBehavior
{
	/// <summary>
	/// Interaction logic for Example.xaml
	/// </summary>
	public partial class Example : UserControl
	{
		public Example()
		{
			InitializeComponent();
		}

		private void SearchControl_GotFocus_1(object sender, RoutedEventArgs e)
		{
			var autoCompleteBox = sender as RadAutoCompleteBox;
            if (!autoCompleteBox.IsDropDownOpen)
            {
                if (string.IsNullOrEmpty(autoCompleteBox.SearchText))
                {
                    autoCompleteBox.Populate("");
                }
                else
                {
                    autoCompleteBox.Populate(autoCompleteBox.SearchText);
                }
            }
		}
	}
}
