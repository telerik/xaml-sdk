using System.Collections.ObjectModel;

namespace Grouping
{
    public class ViewModel
    {
        public ViewModel()
        {
            this.Countries = new ObservableCollection<Country>()
            {
                new Country() { Name = "Australia", Capital = "Canberra", Continent = Continent.Australia },
                new Country() { Name = "Bulgaria", Capital = "Sofia", Continent = Continent.Europe },
                new Country() { Name = "Canada", Capital = "Ottawa", Continent = Continent.NorthAmerica },
                new Country() { Name = "Denmark", Capital = "Copenhagen", Continent = Continent.Europe },
                new Country() { Name = "France", Capital = "Paris", Continent = Continent.Europe },
                new Country() { Name = "Germany", Capital = "Berlin", Continent = Continent.Europe },
                new Country() { Name = "India", Capital = "New Delhi", Continent = Continent.Asia },
                new Country() { Name = "Italy", Capital = "Rome", Continent = Continent.Europe },
                new Country() { Name = "Norway", Capital = "Oslo", Continent = Continent.Europe },
                new Country() { Name = "Russia", Capital = "Moscow", Continent = Continent.Europe },
                new Country() { Name = "Spain ", Capital = "Madrid", Continent = Continent.Europe  },
                new Country() { Name = "United Kingdom", Capital = "London", Continent = Continent.Europe },
                new Country() { Name = "United States", Capital = "Washington, D.C.", Continent = Continent.NorthAmerica },
                new Country() { Name = "Nigeria", Capital = "Abuja", Continent = Continent.Africa },
                new Country() { Name = "Egypt", Capital = "Cairo", Continent = Continent.Africa },
                new Country() { Name = "Brazil", Capital = "Brasilia", Continent = Continent.SouthAmerica  },
                new Country() { Name = "Argentina ", Capital = "Buenos Aires", Continent = Continent.SouthAmerica },
                new Country() { Name = "China", Capital = "Beijing", Continent = Continent.Asia },
                new Country() { Name = "Japan", Capital = "Tokyo", Continent = Continent.Asia },
            };
        }

        public ObservableCollection<Country> Countries { get; set; }
    }
}
