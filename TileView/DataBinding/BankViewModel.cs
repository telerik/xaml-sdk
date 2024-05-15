using System.Collections.ObjectModel;

namespace DataBinding
{
    public class BankViewModel
    {
        public ObservableCollection<CustomerAccount> Customers { get; set; }

        public BankViewModel()
        {
            this.Customers = new ObservableCollection<CustomerAccount>();
            this.AddCustomers();
        }        

        public void AddCustomers()
        {
            this.Customers.Add(new CustomerAccount()
            {
                Name = "Michael Johnson",
                City = "New York",
                Balance = 1200,
                Description = "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. "
            });
            this.Customers.Add(new CustomerAccount()
            {
                Name = "Alan Rickman",
                City = "Boston",
                Balance = 2500,
                Description = "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. "
            });
            this.Customers.Add(new CustomerAccount()
            {
                Name = "Jesse Hernandez",
                City = "Miami",
                Balance = 5600,
                Description = "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. "
            });
            this.Customers.Add(new CustomerAccount()
            {
                Name = "Mike Dunbar",
                City = "Las Vegas",
                Balance = 9000,
                Description = "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. "
            });
            this.Customers.Add(new CustomerAccount()
            {
                Name = "John Waldner",
                City = "Chicago",
                Balance = 5800,
                Description = "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. "
            });
            this.Customers.Add(new CustomerAccount()
            {
                Name = "Carla Archie",
                City = "San Francisco",
                Balance = 3600,
                Description = "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. "
            });
        }
    }
}
