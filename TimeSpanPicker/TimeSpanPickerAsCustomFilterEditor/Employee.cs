using System;
using System.ComponentModel.DataAnnotations;

namespace TimeSpanPickerAsCustomFilterEditor
{
    public class Employee
    {
        public Employee(string name, string companyName, string title, TimeSpan duration)
        {
            this.Duration = duration;
            this.Name = name;
            this.CompanyName = companyName;
            this.Title = title;
        }

        public string Name
        {
            get;
            set;
        }

        public string CompanyName
        {
            get;
            set;
        }

        public string Title
        {
            get;
            set;
        }

        [Display(Order = 0)]
        public TimeSpan Duration
        {
            get;
            set;
        }
    }
}
