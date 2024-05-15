using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using Telerik.Windows.Controls;

namespace SelectedItems
{
	public class MyViewModel : ViewModelBase
	{
		private ObservableCollection<Country> selectedCountries;

		public MyViewModel()
		{
			this.Countries = new ObservableCollection<Country>()
			{
				new Country() { Name = "Australia" },
				new Country() { Name = "Bulgaria" },
				new Country() { Name = "Canada" },
				new Country() { Name = "Denmark" },
				new Country() { Name = "France" },
				new Country() { Name = "Germany" },
				new Country() { Name = "India" },
				new Country() { Name = "Italy" },
				new Country() { Name = "Norway" },
				new Country() { Name = "Russia" },
				new Country() { Name = "Spain " },
				new Country() { Name = "United Kingdom" },
				new Country() { Name = "United States" }
			};

			this.SelectedCountries = new ObservableCollection<Country>()
			{
				this.Countries[0],
				this.Countries[2],
				this.Countries[4],
				this.Countries[6],
				this.Countries[8]
			};
		}

		public ObservableCollection<Country> Countries { get; set; }

		/// <summary>
		/// Gets or sets SelectedCountries and notifies for changes
		/// </summary>
		public ObservableCollection<Country> SelectedCountries
		{
			get
			{
				return this.selectedCountries;
			}

			set
			{
				if (this.selectedCountries != value)
				{
					this.selectedCountries = value;
					this.OnPropertyChanged(() => this.SelectedCountries);
				}
			}
		}
	}
}
