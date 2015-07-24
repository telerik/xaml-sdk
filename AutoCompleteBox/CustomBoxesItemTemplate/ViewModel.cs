using System.Collections.ObjectModel;

namespace CustomBoxesItemTemplate
{
    public class ViewModel
    {
        public ObservableCollection<Country> Countries { get; set; }

        public ViewModel()
        {
            this.Countries = new ObservableCollection<Country>()
            {
                new Country() { Name = "Australia", Capital = "Canberra", Continent = Continent.Australia, Flag = "Images/au.png" },
                new Country() { Name = "Bulgaria", Capital = "Sofia", Continent = Continent.Europe, Flag = "Images/bg.png"  },
                new Country() { Name = "Canada", Capital = "Ottawa", Continent = Continent.NorthAmerica, Flag = "Images/ca.png"  },
                new Country() { Name = "Denmark", Capital = "Copenhagen", Continent = Continent.Europe, Flag = "Images/dk.png"  },
                new Country() { Name = "France", Capital = "Paris", Continent = Continent.Europe, Flag = "Images/fr.png"  },
                new Country() { Name = "Germany", Capital = "Berlin", Continent = Continent.Europe, Flag = "Images/de.png"  },
                new Country() { Name = "India", Capital = "New Delhi", Continent = Continent.Asia, Flag = "Images/in.png"  },
                new Country() { Name = "Italy", Capital = "Rome", Continent = Continent.Europe, Flag = "Images/it.png"  },
                new Country() { Name = "Norway", Capital = "Oslo", Continent = Continent.Europe, Flag = "Images/no.png"  },
                new Country() { Name = "Russia", Capital = "Moscow", Continent = Continent.Europe, Flag = "Images/ru.png"  },
                new Country() { Name = "Spain ", Capital = "Madrid", Continent = Continent.Europe, Flag = "Images/sp.png"  },
                new Country() { Name = "United Kingdom", Capital = "London", Continent = Continent.Europe, Flag = "Images/uk.png"  },
                new Country() { Name = "United States", Capital = "Washington, D.C.", Continent = Continent.NorthAmerica, Flag = "Images/usa.png"  },
                new Country() { Name = "Nigeria", Capital = "Abuja", Continent = Continent.Africa, Flag = "Images/ng.png"  },
                new Country() { Name = "Egypt", Capital = "Cairo", Continent = Continent.Africa, Flag = "Images/eg.png"  },
                new Country() { Name = "Brazil", Capital = "Brasilia", Continent = Continent.SouthAmerica, Flag = "Images/br.png"  },
                new Country() { Name = "Argentina ", Capital = "Buenos Aires", Continent = Continent.SouthAmerica, Flag = "Images/ar.png"  },
                new Country() { Name = "China", Capital = "Beijing", Continent = Continent.Asia, Flag = "Images/cn.png"  },
                new Country() { Name = "Japan", Capital = "Tokyo", Continent = Continent.Asia, Flag = "Images/jp.png"  },
            };
        }
    }
}
