using System.Collections.ObjectModel;
using Telerik.Windows.Controls;

namespace MvvmSupport
{
	public class ExampleViewModel : ViewModelBase
	{
		private ObservableCollection<Product> _data;
		public ObservableCollection<Product> Data
		{
			get
			{
				return this._data;
			}
			set
			{
				if (this._data != value)
				{
					this._data = value;
					this.OnPropertyChanged("Data");
				}
			}
		}

		public ExampleViewModel()
		{
			this.Data = Product.GetData(15);
		}
	}
}
