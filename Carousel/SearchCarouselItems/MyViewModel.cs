using System.Collections.ObjectModel;
using Telerik.Windows.Controls;

namespace SearchCarouselItems
{
    public class MyViewModel : ViewModelBase
    {
        private ObservableCollection<Employee> employees;

        public ObservableCollection<Employee> Employees
        {
            get
            {
                if (this.employees == null)
                {
                    this.employees = EmployeeService.GetEmployees();
                }

                return this.employees;
            }
        }
    }
}
