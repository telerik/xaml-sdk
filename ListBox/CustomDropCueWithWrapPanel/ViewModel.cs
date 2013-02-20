using System.Collections.Generic;
using System.Collections.ObjectModel;
using Telerik.Windows.Controls;

namespace CustomDropCueWithWrapPanel
{
	public class ViewModel : ViewModelBase
	{
		private ObservableCollection<Item> dataList;

		public ViewModel()
		{
			this.DataList = new ObservableCollection<Item>(this.GetNumbers());
		}

		/// <Summary>Gets or sets DataList and notifies for changes</Summary>
		public ObservableCollection<Item> DataList
		{
			get
			{ 
				return this.dataList; 
			}

			set
			{
				if (this.dataList != value)
				{
					this.dataList = value;
					this.OnPropertyChanged(() => this.DataList);
				}
			}
		}

		private IEnumerable<Item> GetNumbers()
		{
			for (int i = 0; i < 50; i++)
			{
				yield return new Item { Number = i };
			}
		}
	}
}
