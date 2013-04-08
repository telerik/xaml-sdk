using System.Collections.Generic;
using System.Windows.Controls;

namespace GroupingAndAggregation
{
	public partial class MainPage : UserControl
	{
		public MainPage()
		{
			InitializeComponent();
			this.DataContext = new List<CountryData>()
			{
				new CountryData { Year = 2008, Region = "Europe", Description = "Apple", Value = 145 },
				new CountryData { Year = 2009, Region = "North America", Description = "Banana", Value = 132 },
				new CountryData { Year = 2009, Region = "Asia", Description = "Apple", Value = 164 },
				new CountryData { Year = 2008, Region = "Asia", Description = "Banana", Value = 187 },
				new CountryData { Year = 2008, Region = "Europe", Description = "Banana", Value = 186 },
				new CountryData { Year = 2009, Region = "Europe", Description = "Apple", Value = 131 },
				new CountryData { Year = 2008, Region = "North America", Description = "Banana", Value = 173 },
				new CountryData { Year = 2009, Region = "Asia", Description = "Banana",	Value = 172 },
				new CountryData { Year = 2009, Region = "North America", Description = "Apple", Value = 140 },
				new CountryData { Year = 2008, Region = "Asia", Description = "Apple", Value = 129 },
				new CountryData { Year = 2009, Region = "Europe", Description = "Banana", Value = 158 },
				new CountryData { Year = 2008, Region = "North America", Description = "Apple", Value = 149 },
			};
		}
	}
}
