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
                            RecipientPhoto = @"/MailMerge;component\Images\male1.png",
                        }, 
                        new Employee()
                        {
                            FirstName = "Nancy",
                            LastName = "Davolio", 
                            JobTitle = "Front End Developer",
                            RecipientPhoto = @"/MailMerge;component/Images/female1.png",
                        },
                        new Employee()
                        {
                            FirstName = "Robert",
                            LastName = "King", 
                            JobTitle = "Senior .NET Developer",
                            RecipientPhoto = @"/MailMerge;component/Images/male2.png",
                        },
                        new Employee()
                        {
                            FirstName = "Margaret",
                            LastName = "Peacock", 
                            JobTitle = "C#/XAML Developer",
                            RecipientPhoto = @"/MailMerge;component/Images/female2.png",
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
