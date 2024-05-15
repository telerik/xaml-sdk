using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using Telerik.Windows.Controls;

namespace WithGridView
{
	public class Employe : ViewModelBase
	{
		private Country selectedCountry;

		public string FirstName { get; set; }

		public string LastName { get; set; }

		public ObservableCollection<Country> Countries { get; set; }

		public Country SelectedCountry
		{
			get
			{
				return this.selectedCountry;
			}

			set
			{
				if (this.selectedCountry != value)
				{
					this.selectedCountry = value;
					this.OnPropertyChanged(() => this.SelectedCountry);
				}
			}
		}
	}
}
