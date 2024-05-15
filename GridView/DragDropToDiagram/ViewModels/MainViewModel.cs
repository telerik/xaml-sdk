using System.Collections.ObjectModel;

namespace DragDropToDiagram_WPF.ViewModels
{
    public class MainViewModel
    {
        public EmployeeGraphSource EmployeeGraphSource { get; set; }
        public ObservableCollection<Employee> EmployeeData { get; set; }

        public MainViewModel()
        {
            EmployeeData = GetEmployee();
            EmployeeGraphSource = new EmployeeGraphSource();
        }

        private ObservableCollection<Employee> GetEmployee()
        {
            var data = new ObservableCollection<Employee>();
            data.Add(new Employee("Nancy", "Davolio", "Seatle", "USA"));
            data.Add(new Employee("Andrew", "Fuller", "Tacoma", "USA"));
            data.Add(new Employee("Janet", "Leverling", "Kirkland", "USA"));
            data.Add(new Employee("Margaret", "Peacock", "Redmond", "USA"));
            data.Add(new Employee("Steven", "Buchanan", "London", "UK"));
            data.Add(new Employee("Michael", "Suyama", "London", "UK"));
            data.Add(new Employee("Robert", "King", "London", "UK"));
            data.Add(new Employee("Laura", "Callahan", "Seattle", "USA"));
            data.Add(new Employee("Anne", "Dodsworth", "Seattle", "USA"));

            return data;
        }
    }
}
