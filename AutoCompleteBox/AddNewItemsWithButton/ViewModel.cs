using System.Collections.ObjectModel;

namespace AddNewItemsWithButton
{
    public class ViewModel
    {
        public ObservableCollection<Country> Countries { get; set; }

        public ViewModel()
        {
            this.Countries = new ObservableCollection<Country>()
			{
				new Country() { Name = "Australia", Capital = "Canberra", Id = 1 },
				new Country() { Name = "Bulgaria", Capital = "Sofia" , Id = 2 },
				new Country() { Name = "Canada", Capital = "Ottawa" , Id = 3 },
				new Country() { Name = "Denmark", Capital = "Copenhagen" , Id = 4 },
				new Country() { Name = "France", Capital = "Paris" , Id = 5 },
				new Country() { Name = "Germany", Capital = "Berlin" , Id = 6 },
				new Country() { Name = "India", Capital = "New Delhi" , Id = 7 },
				new Country() { Name = "Italy", Capital = "Rome" , Id = 8 },
				new Country() { Name = "Norway", Capital = "Oslo" , Id = 9 },
				new Country() { Name = "Russia", Capital = "Moscow" , Id = 10 },
				new Country() { Name = "Spain ", Capital = "Madrid" , Id = 11 },
				new Country() { Name = "United Kingdom", Capital = "London" , Id = 12 },
				new Country() { Name = "United States", Capital = "Washington, D.C." , Id = 13 },
			};
        }
    }
}
