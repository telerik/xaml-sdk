using System.Collections.Generic;
using System.Linq;
using System.Windows;

namespace SearchCarouselItems
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Search_Employees(object sender, RoutedEventArgs e)
        {
            if (!this.MyCarousel.IsLoaded)
            {
                return;
            }

            var searchQuery = this.TextBoxSearchName.Text;

            if (string.IsNullOrWhiteSpace(searchQuery))
            {
                return;
            }

            var items = (IEnumerable<Employee>)this.MyCarousel.ItemsSource;
            Employee selectedEmployee = null;

            if (items != null)
            {
                selectedEmployee = items.FirstOrDefault(x => x.FirstName.ToLower().Contains(searchQuery.ToLower()));

                this.MyCarousel.BringDataItemIntoView(selectedEmployee);
                this.MyCarousel.SelectedItem = selectedEmployee;
            }
        }
    }
}
