using System.Collections.ObjectModel;
using Telerik.Windows.Controls;

namespace DragDropDataConversion
{
    public class ViewModel : ViewModelBase
    {
        private ObservableCollection<Customer> customers;

        private ObservableCollection<Project> projects;

        public ViewModel()
        {
            this.Customers = new ObservableCollection<Customer>()
			{
				new Customer { Id = 1, Name = "Customer 1", Project = "Project 1" },
				new Customer { Id = 2, Name = "Customer 2", Project = "Project 2" },
				new Customer { Id = 3, Name = "Customer 3", Project = "Project 3" },
				new Customer { Id = 4, Name = "Customer 4", Project = "Project 4" },
				new Customer { Id = 5, Name = "Customer 5", Project = "Project 5" } 
			};
            this.Projects = new ObservableCollection<Project>()
			{
				new Project() { Name = "Project 6", Id = 6, Person = "Customer 6" },
				new Project() { Name = "Project 7", Id = 6, Person = "Customer 7" },
				new Project() { Name = "Project 8", Id = 6, Person = "Customer 8" },
				new Project() { Name = "Project 9", Id = 6, Person = "Customer 9" },
				new Project() { Name = "Project 10", Id = 6, Person = "Customer 10" }
			};
        }

        public ObservableCollection<Project> Projects
        {
            get { return this.projects; }
            set
            {
                if (this.projects != value)
                {
                    this.projects = value;
                    this.OnPropertyChanged(() => this.Projects);
                }
            }
        }

        public ObservableCollection<Customer> Customers
        {
            get { return this.customers; }
            set
            {
                if (this.customers != value)
                {
                    this.customers = value;
                    this.OnPropertyChanged(() => this.Customers);
                }
            }
        }
    }
}
