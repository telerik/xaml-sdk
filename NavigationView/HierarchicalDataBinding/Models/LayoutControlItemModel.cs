using System.Collections.ObjectModel;

namespace HierarchicalDataBinding.Models
{
    public class LayoutControlItemModel : ControlItemModel
    {
        private ObservableCollection<Customer> layoutControlItems;

        public ObservableCollection<Customer> LayoutControlItems
        {
            get
            {
                if (this.layoutControlItems == null)
                {
                    this.layoutControlItems = this.GenerateCustomers();
                }

                return this.layoutControlItems;
            }

        }

        private ObservableCollection<Customer> GenerateCustomers()
        {
            var customers = new ObservableCollection<Customer>();
            for (int i = 1; i <= 5; i++)
            {
                var customer = new Customer() { Name = "Customer " + i, Age = 25 + i };
                customers.Add(customer);
            }

            return customers;
        }
    }

    public class Customer
    {
        public string Name { get; set; }
        public int Age { get; set; }
    }
}
