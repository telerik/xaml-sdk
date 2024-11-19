using System.Windows;

namespace DataTemplateSelector
{
    public class EmployeesEditTemplateSelector : System.Windows.Controls.DataTemplateSelector
    {
        public override DataTemplate SelectTemplate(object item, DependencyObject container)
        {
            Employee employee = item as Employee;
            if (employee == null)
            {
                return null;
            }
            else if (employee.Salary > 2500)
            {
                return this.BigSalaryTemplate;
            }
            else
            {
                return this.SmallSalaryTemplate;
            }
        }

        public DataTemplate BigSalaryTemplate { get; set; }
        public DataTemplate SmallSalaryTemplate { get; set; }
    }
}
