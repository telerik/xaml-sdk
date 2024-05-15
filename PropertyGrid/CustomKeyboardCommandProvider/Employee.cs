using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KeyboardCommandProvider
{
    public class Employee
    {
        private string firstName;
        private string lastName;
        private string title;
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

        public string LastName
        {
            get
            {
                return this.lastName;
            }
            set
            {
                if (this.lastName != value)
                {
                    this.lastName = value;
                }
            }
        }

        public string Title
        {
            get
            {
                return title;
            }
            set
            {
                if (this.title != value)
                {
                    this.title = value;
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
