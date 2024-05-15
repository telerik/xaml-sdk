using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace AddNewItemsWithButton
{
    public partial class Example : UserControl
    {
        public Example()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var inputText = this.AutoCompleteBox.SearchText;
            var itemsSource = this.AutoCompleteBox.ItemsSource as ObservableCollection<Country>;
            var newItem = new Country() { Name = this.AutoCompleteBox.SearchText };
            if (!itemsSource.Any(item => item.Name == newItem.Name))
            {
                itemsSource.Add(newItem);
                this.AutoCompleteBox.ItemsSource = itemsSource;
                this.AutoCompleteBox.SelectedItem = newItem;
                this.ResultTextBlock.Text = string.Format("Successfully added new item ({0}) to the ItemsSource collection.", newItem.Name);
            }
        }
    }
}
