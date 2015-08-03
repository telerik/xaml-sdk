using System.Collections.ObjectModel;

namespace DataValidation
{
    public class Team
    {
        public string Name
        {
            get;
            set;
        }
        public ObservableCollection<Employee> Employees
        {
            get;
            set;
        }
    }
}
