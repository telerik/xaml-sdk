using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CustomHyperlinkToolTip
{
    public class Employee
    {
        private string firstName;
        private DateTime birthDate;
        private int salary;

        public string FirstName
        {
            get
            {
                return this.firstName;
            }

            set
            {
                if (this.firstName != value)
                {
                    this.firstName = value;
                }
            }
        }

        public DateTime BirthDate
        {
            get
            {
                return birthDate;
            }
            set
            {
                if (this.birthDate != value)
                {
                    this.birthDate = value;
                }
            }
        }

        public int Salary
        {
            get
            {
                return salary;
            }
            set
            {
                if (this.salary != value)
                {
                    this.salary = value;
                }
            }
        }

        public override string ToString()
        {
            return this.FirstName;
        }
    }
}
