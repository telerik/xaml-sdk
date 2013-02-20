using System.Collections.ObjectModel;
using Telerik.Windows.Controls;

namespace OpenWithDropDownButton
{
	public class ViewModel : ViewModelBase
	{
		private ObservableCollection<string> items;

		public ViewModel()
		{
			this.items = new ObservableCollection<string>();
			this.items = GetItems();
		}

		/// <summary>
		/// Gets or sets Items and notifies for changes
		/// </summary>
		public ObservableCollection<string> Items
		{
			get
			{
				return this.items;
			}

			set
			{
				if (this.items != value)
				{
					this.items = value;
					this.OnPropertyChanged(() => this.Items);
				}
			}
		}

		private ObservableCollection<string> GetItems()
		{
			ObservableCollection<string> result = new ObservableCollection<string>();
			for (int i = 0; i < 100; i++)
			{
				result.Add(string.Format("{0} Item", i));
			}

			return result;
		}
	}
}
