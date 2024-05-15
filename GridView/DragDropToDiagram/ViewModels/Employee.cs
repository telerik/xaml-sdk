namespace DragDropToDiagram_WPF.ViewModels
{
    public class Employee
    {
        public Employee(string firstName, string lastName, string city, string country)
        {
            this.FirstName = firstName;
            this.LastName = lastName;
            this.City = city;
            this.Country = country;
        }
        private string firstname;
        public string FirstName
        {
            get { return firstname; }
            set { firstname = value; }
        }

        private string lastName;
        public string LastName
        {
            get { return lastName; }
            set { lastName = value; }
        }

        private string city;
        public string City
        {
            get { return city; }
            set { city = value; }
        }

        private string country;
        public string Country
        {
            get { return country; }
            set { country = value; }
        }
    }
}
