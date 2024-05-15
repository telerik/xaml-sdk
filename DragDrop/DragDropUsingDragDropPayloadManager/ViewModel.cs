using System.Collections.ObjectModel;
using Telerik.Windows.Controls;

namespace DragDropUsingDragDropPayloadManager
{
    public class ViewModel : ViewModelBase
    {
        public ObservableCollection<Customer> ItemsSource1 { get; set; }
        public ObservableCollection<string> ItemsSource2 { get; set; }

        public ViewModel()
        {
            this.ItemsSource1 = new ObservableCollection<Customer>
            {
                new Customer { FirstName= "John", LastName = "Smith", Age = 24},
                new Customer { FirstName= "George", LastName = "Lucas", Age = 35},
                new Customer { FirstName= "Justin", LastName = "Marks", Age = 16},
                new Customer { FirstName= "Emily", LastName = "Rose", Age = 40},
                new Customer { FirstName= "Mike", LastName = "Jones", Age = 20},
            };

            this.ItemsSource2 = new ObservableCollection<string>();
        }
    }
}
