using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GroupStyle
{
    public class Employee
    {
        private string firstName;
        private string lastName;
        private string occupation;
        private int salary;
        private bool isMarried;
        private DateTime startingDate;

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

        public string Occupation
        {
            get
            {
                return occupation;
            }
            set
            {
                if (this.occupation != value)
                {
                    this.occupation = value;
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

        public bool IsMarried
        {
            get
            {
                return isMarried;
            }
            set
            {
                if (this.isMarried != value)
                {
                    this.isMarried = value;
                }
            }
        }
        public DateTime StartingDate
        {
            get
            {
                return startingDate;
            }
            set
            {
                if (this.startingDate != value)
                {
                    this.startingDate = value;
                }
            }
        }


        public override string ToString()
        {
            return this.FirstName;
        }
    }
}
