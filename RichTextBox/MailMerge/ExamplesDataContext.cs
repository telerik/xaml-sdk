using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MailMerge;

namespace MailMerge
{
    public class ExamplesDataContext
    {
        private readonly List<Employee> employees = new List<Employee>()
                    {
                        new Employee()
                        {
                            FirstName = "Andrew",
                            LastName = "Fuller", 
                            JobTitle = "Junior Support Officer",
                        }, 
                        new Employee()
                        {
                            FirstName = "Nancy",
                            LastName = "Davolio", 
                            JobTitle = "Front End Developer",
                        },
                        new Employee()
                        {
                            FirstName = "Robert",
                            LastName = "King", 
                            JobTitle = "Senior .NET Developer",
                        },
                        new Employee()
                        {
                            FirstName = "Margaret",
                            LastName = "Peacock", 
                            JobTitle = "C#/XAML Developer",
                        }
                    };

        public List<Employee> Employees
        {
            get
            {
                return employees;
            }
        }
    }
}
