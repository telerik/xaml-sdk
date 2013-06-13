using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

namespace RadContextMenuForRadGridView
{

    public class RadGridViewSampleData
    {
        public static ObservableCollection<Employee> GetEmployees()
        {
            ObservableCollection<Employee> employees = new ObservableCollection<Employee>();
            Employee employee = new Employee();
            employee.FirstName = "Maria";
            employee.LastName = "Anders";
            employee.IsMarried = true;
            employee.Age = 24;
            employees.Add(employee);
            employee = new Employee();
            employee.FirstName = "Ana";
            employee.LastName = "Trujillo";
            employee.IsMarried = true;
            employee.Age = 44;
            employees.Add(employee);
            employee = new Employee();
            employee.FirstName = "Antonio";
            employee.LastName = "Moreno";
            employee.IsMarried = true;
            employee.Age = 33;
            employees.Add(employee);
            employee = new Employee();
            employee.FirstName = "Thomas";
            employee.LastName = "Hardy";
            employee.IsMarried = false;
            employee.Age = 13;
            employees.Add(employee);
            employee = new Employee();
            employee.FirstName = "Hanna";
            employee.LastName = "Moos";
            employee.IsMarried = false;
            employee.Age = 28;
            employees.Add(employee);
            employee = new Employee();
            employee.FirstName = "Frederique";
            employee.LastName = "Citeaux";
            employee.IsMarried = true;
            employee.Age = 67;
            employees.Add(employee);
            employee = new Employee();
            employee.FirstName = "Martin";
            employee.LastName = "Sommer";
            employee.IsMarried = false;
            employee.Age = 22;
            employees.Add(employee);
            employee = new Employee();
            employee.FirstName = "Laurence";
            employee.LastName = "Lebihan";
            employee.IsMarried = false;
            employee.Age = 32;
            employees.Add(employee);
            employee = new Employee();
            employee.FirstName = "Elizabeth";
            employee.LastName = "Lincoln";
            employee.IsMarried = false;
            employee.Age = 9;
            employees.Add(employee);
            employee = new Employee();
            employee.FirstName = "Victoria";
            employee.LastName = "Ashworth";
            employee.IsMarried = true;
            employee.Age = 29;
            employees.Add(employee);
            return employees;
        }
    }

}
