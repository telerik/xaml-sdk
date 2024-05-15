using System.Collections.ObjectModel;

namespace WithDataForm
{
	public class Person
	{
		public Person()
		{
			this.SelectedCountries = new ObservableCollection<Country>();
		}

		public string Name { get; set; }

		public ObservableCollection<Country> SelectedCountries { get; set; }

		public Country SelectedCountry { get; set; }
	}
}
