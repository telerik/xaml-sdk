using System.Collections.ObjectModel;
using Telerik.Windows.Controls;

namespace WithGridView
{
	public class ViewModel : ViewModelBase
	{
		private ObservableCollection<Employe> employees;

		private ObservableCollection<Country> countriesList;

		public ViewModel()
		{
			this.countriesList = GenerateCountryList();

			this.employees = GenerateEmployees();
		}

		public ObservableCollection<Employe> Employees
		{
			get
			{ 
				return this.employees;
			}

			set
			{
				if (this.employees != value)
				{
					this.employees = value;
					this.OnPropertyChanged(() => this.Employees);
				}
			}
		}

		private ObservableCollection<Employe> GenerateEmployees()
		{
			var result = new ObservableCollection<Employe>()
			{
				new Employe() { FirstName = "Maria", LastName = "Anders", Countries = this.countriesList },
				new Employe() { FirstName = "Ana", LastName = "Trujillo", Countries = this.countriesList, SelectedCountry = this.countriesList[0] },
				new Employe() { FirstName = "Antonio", LastName = "Moreno", Countries = this.countriesList }
			};

			return result;
		}

		private ObservableCollection<Country> GenerateCountryList()
		{
			var result = new ObservableCollection<Country>()
			{
				new Country() { Name = "Australia" , Capital = "Canberra" },
				new Country() { Name = "Bulgaria", Capital = "Sofia" },
				new Country() { Name = "Canada" , Capital = "Ottawa" },
				new Country() { Name = "Denmark" , Capital = "Copenhagen" },
				new Country() { Name = "France" , Capital = "Paris" },
				new Country() { Name = "Germany" , Capital = "Berlin" },
				new Country() { Name = "India" , Capital = "New Delhi" },
				new Country() { Name = "Italy" , Capital = "Rome" },
				new Country() { Name = "Norway" , Capital = "Oslo" },
				new Country() { Name = "Russia" , Capital = "Moscow" },
				new Country() { Name = "Spain " , Capital = "Madrid" },
				new Country() { Name = "United Kingdom" , Capital = "London" },
				new Country() { Name = "United States" , Capital = "Washington, D.C." },
			};

			return result;
		}
	}
}
