using System.Collections.ObjectModel;

namespace DataPager_ChangePageSizeDynamically
{
    public class MyViewModel
    {
        public ObservableCollection<Employee> Employees
        {
            get
            {
                var locations = new ObservableCollection<Employee>();
                for (int i = 1; i <= 1000; i++)
                {
                    Employee employee = new Employee();
                    employee.Name = "Name " + i;
                    employee.Company = "Company " + i;
                    employee.Position = "Position" + i;

                    locations.Add(employee);
                }
                return locations;
            }
        }
    }
}
