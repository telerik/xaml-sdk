using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Telerik.Windows.Controls;

namespace ChangeThemeRuntime
{
    /// <summary>
    /// Interaction logic for BasicControls.xaml
    /// </summary>
    public partial class BasicControlsView : UserControl
    {
        public BasicControlsView()
        {
            InitializeComponent();
            var items = new ObservableCollection<string> { "Item 1", "Item 2", "Item 3", "Item 4", "Item 5" };
            this.AutoCompleteBox.ItemsSource = items;
            this.AutoCompleteBox.SelectedItems = new ObservableCollection<string> { items[1] };
        }

        private void OnAutoSuggestBoxTextChanged(object sender, Telerik.Windows.Controls.AutoSuggestBox.TextChangedEventArgs e)
        {
            if (e.Reason == Telerik.Windows.Controls.AutoSuggestBox.TextChangeReason.UserInput)
            {
                var autoSuggestBox = (RadAutoSuggestBox)sender;
                List<string> suggestions = new List<string>()
                {
                    autoSuggestBox.Text + "1",
                    autoSuggestBox.Text + "2",
                    autoSuggestBox.Text + "3",
                    autoSuggestBox.Text + "4",
                };

                autoSuggestBox.ItemsSource = suggestions;
            }
        }
    }
}
