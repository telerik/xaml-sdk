using System.Collections.ObjectModel;

namespace ChangeThemeRuntime
{
	public class EmployeeService
	{
		public enum OccupationPositions
		{
			SalesRepresentative,
			SalesManager,
			VicePresident,
			InsideSalesCoordinator,
			ProductManager
		}

		public static ObservableCollection<Employee> GetEmployees()
		{
			ObservableCollection<Employee> employees = new ObservableCollection<Employee>();
			Employee employee = new Employee();
			employee.FirstName = "Maria";
			employee.LastName = "Anders";
			employee.IsMarried = true;
			employee.Title = OccupationPositions.SalesRepresentative;
			employee.City = "Seattle";
			employee.Country = "USA";
			employee.Phone = "(206) 555-9857";
			employee.Age = 24;
			employees.Add(employee);
			employee = new Employee();
			employee.FirstName = "Ana";
			employee.LastName = "Trujillo";
			employee.IsMarried = true;
			employee.Age = 44;
			employee.Title = OccupationPositions.VicePresident;
			employee.City = "Tacoma";
			employee.Country = "USA";
			employee.Phone = "(206) 555-9482";
			employees.Add(employee);
			employee = new Employee();
			employee.FirstName = "Antonio";
			employee.LastName = "Moreno";
			employee.IsMarried = true;
			employee.Age = 33;
			employee.Title = OccupationPositions.SalesRepresentative;
			employee.City = "Kirkland";
			employee.Country = "USA";
			employee.Phone = "(206) 555-3412";
			employees.Add(employee);
			employee = new Employee();
			employee.FirstName = "Thomas";
			employee.LastName = "Hardy";
			employee.IsMarried = false;
			employee.Age = 13;
			employee.Title = OccupationPositions.SalesRepresentative;
			employee.City = "Redmond";
			employee.Country = "USA";
			employee.Phone = "(206) 555-4848";
			employees.Add(employee);
			employee = new Employee();
			employee.FirstName = "Hanna";
			employee.LastName = "Moos";
			employee.IsMarried = false;
			employee.Age = 28;
			employee.Title = OccupationPositions.SalesManager;
			employee.City = "London";
			employee.Country = "UK";
			employee.Phone = "(71) 555-7773";
			employees.Add(employee);
			employee = new Employee();
			employee.FirstName = "Frederique";
			employee.LastName = "Citeaux";
			employee.IsMarried = true;
			employee.Age = 67;
			employee.Title = OccupationPositions.InsideSalesCoordinator;
			employee.City = "Seattle";
			employee.Country = "USA";
			employee.Phone = "(206) 555-1189";
			employees.Add(employee);
			employee = new Employee();
			employee.FirstName = "Martin";
			employee.LastName = "Sommer";
			employee.IsMarried = false;
			employee.Age = 22;
			employee.Title = OccupationPositions.SalesRepresentative;
			employee.City = "London";
			employee.Country = "UK";
			employee.Phone = "(71) 555-3295";
			employees.Add(employee);
			employee = new Employee();
			employee.FirstName = "Laurence";
			employee.LastName = "Lebihan";
			employee.IsMarried = false;
			employee.Title = OccupationPositions.SalesRepresentative;
			employee.City = "London";
			employee.Country = "UK";
			employee.Phone = "(71) 555-4444";
			employee.Age = 32;
			employees.Add(employee);
			employee = new Employee();
			employee.FirstName = "Elizabeth";
			employee.LastName = "Lincoln";
			employee.IsMarried = false;
			employee.Title = OccupationPositions.SalesRepresentative;
			employee.City = "London";
			employee.Country = "UK";
			employee.Phone = "(71) 555-9812";
			employee.Age = 19;
			employees.Add(employee);
			employee = new Employee();
			employee.FirstName = "Victoria";
			employee.LastName = "Ashworth";
			employee.IsMarried = true;
			employee.Age = 29;
			employee.Title = OccupationPositions.ProductManager;
			employee.City = "Seattle";
			employee.Country = "USA";
			employee.Phone = "(71) 555-1297";
			employees.Add(employee);
			return employees;
		}
	}
}
