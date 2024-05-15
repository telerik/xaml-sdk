using System.Collections.ObjectModel;

namespace SearchCarouselItems
{
    public class EmployeeService
    {
        public static ObservableCollection<Employee> GetEmployees()
        {
            ObservableCollection<Employee> employees = new ObservableCollection<Employee>();
            Employee employee = new Employee();
            employee.FirstName = "Margaret";
            employee.LastName = "Peacock";
            employee.Position = "Sales Representative";
            employee.Age = 24;
            employees.Add(employee);
            employee = new Employee();
            employee.FirstName = "Steven";
            employee.LastName = "Buchanan";
            employee.Position = "Sales Manager";
            employee.Age = 44;
            employees.Add(employee);
            employee = new Employee();
            employee.FirstName = "Michael";
            employee.LastName = "Suyama";
            employee.Position = "Sales Representative";
            employee.Age = 33;
            employees.Add(employee);
            employee = new Employee();
            employee.FirstName = "Robert";
            employee.LastName = "King";
            employee.Position = "Sales Representative";
            employee.Age = 28;
            employees.Add(employee);
            employee = new Employee();
            employee.FirstName = "Laura";
            employee.LastName = "Callahan";
            employee.Position = "Inside Sales Coordinator";
            employee.Age = 26;
            employees.Add(employee);
            employee = new Employee();
            employee.FirstName = "Anne";
            employee.LastName = "Dodsworth";
            employee.Position = "Sales Representative";
            employee.Age = 30;
            employees.Add(employee);

            return employees;
        }
    }
}
