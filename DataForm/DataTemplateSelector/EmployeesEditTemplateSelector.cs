using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;

namespace DataTemplateSelector
{
     
#if WPF
 public class EmployeesEditTemplateSelector : System.Windows.Controls.DataTemplateSelector
#else
    public class EmployeesEditTemplateSelector : Telerik.Windows.Controls.DataTemplateSelector
#endif 
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
