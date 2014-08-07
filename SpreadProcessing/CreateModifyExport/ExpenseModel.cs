using System;
using System.Collections.Generic;

namespace CreateModifyExport
{
    public class ExpenseModel
    {
        private string description;
        private string department;
        private DateTime date;
        private double amount;

        public string Description
        {
            get
            {
                return this.description;
            }
            set
            {
                if (this.description != value)
                {
                    this.description = value;
                }
            }
        }

        public string Department
        {
            get
            {
                return this.department;
            }
            set
            {
                if (this.department != value)
                {
                    this.department = value;
                }
            }
        }

        public DateTime Date
        {
            get
            {
                return this.date;
            }
            set
            {
                if (this.date != value)
                {
                    this.date = value;
                }
            }
        }

        public double Amount
        {
            get
            {
                return this.amount;
            }
            set
            {
                if (this.amount != value)
                {
                    this.amount = value;
                }
            }
        }

        public static IEnumerable<ExpenseModel> GetExpenseData()
        {
            ExpenseModel[] expenses = new ExpenseModel[]
            {
                new ExpenseModel() 
                {
                    Description = "Accounting and legal", 
                    Department = "Marketing", 
                    Date = new DateTime(2014, 2, 20),
                    Amount = 610.5953 
                },
                new ExpenseModel() 
                { 
                    Description = "Salaries",
                    Department = "Sales",
                    Date = new DateTime(2014, 1, 3),
                    Amount = 3563.7902 
                },
                new ExpenseModel() { Description = "Salaries", Department = "Marketing", Date = new DateTime(2014, 1, 3), Amount = 1593.5101 },
                new ExpenseModel() { Description = "Equipment", Department = "Sales", Date = new DateTime(2014, 3, 30), Amount = 905.8607 },
                new ExpenseModel() { Description = "Gifts", Department = "Marketing", Date = new DateTime(2014, 2, 1), Amount = 105.5238 },
                new ExpenseModel() { Description = "Furniture", Department = "Sales", Date = new DateTime(2014, 1, 26), Amount = 589.7878 },
                new ExpenseModel() { Description = "Accounting and legal", Department = "Marketing", Date = new DateTime(2014, 3, 3), Amount = 475.4688 },
                new ExpenseModel() { Description = "Accounting and legal", Department = "Engineering", Date = new DateTime(2014, 2, 9), Amount = 487.9179 },
                new ExpenseModel() { Description = "Office supplies", Department = "Engineering", Date = new DateTime(2014, 1, 14), Amount = 336.1042 },
                new ExpenseModel() { Description = "Travel costs", Department = "Marketing", Date = new DateTime(2014, 2, 12), Amount = 291.1261 },
                new ExpenseModel() { Description = "Other", Department = "Marketing", Date = new DateTime(2014, 3, 4), Amount = 980.4335 },
                new ExpenseModel() { Description = "Gifts", Department = "Engineering", Date = new DateTime(2014, 1, 5), Amount = 103.6524 },
                new ExpenseModel() { Description = "Advertising", Department = "Marketing", Date = new DateTime(2014, 2, 4), Amount = 302.3102 },
                new ExpenseModel() { Description = "Salaries", Department = "Marketing", Date = new DateTime(2014, 3, 22), Amount = 1935.9950 },
                new ExpenseModel() { Description = "Web Hosting and Domains", Department = "Sales", Date = new DateTime(2014, 1, 22), Amount = 286.5915 },
                new ExpenseModel() { Description = "Gifts", Department = "Engineering", Date = new DateTime(2014, 1, 10), Amount = 543.9551 },
                new ExpenseModel() { Description = "Salaries", Department = "Engineering", Date = new DateTime(2014, 2, 15), Amount = 5483.9551 },
                new ExpenseModel() { Description = "Telephone", Department = "Marketing", Date = new DateTime(2014, 2, 4), Amount = 270.6534 },
                new ExpenseModel() { Description = "Other", Department = "Sales", Date = new DateTime(2014, 3, 2), Amount = 467.5892 },
                new ExpenseModel() { Description = "Salaries", Department = "Engineering", Date = new DateTime(2014, 3, 2), Amount = 3467.5892 },
                new ExpenseModel() { Description = "Insurance", Department = "Engineering", Date = new DateTime(2014, 1, 12), Amount = 533.0256 },
                new ExpenseModel() { Description = "Travel costs", Department = "Sales", Date = new DateTime(2014, 1, 9), Amount = 322.4842 },
            };

            return expenses;
        }
    }
}
