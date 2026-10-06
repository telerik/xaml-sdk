using System.Collections.ObjectModel;
using System.Windows.Controls;
using Telerik.Windows.Controls;

namespace CustomizingItems
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

		private DataTemplateSelector customItemTemplateSelector;
		public DataTemplateSelector CustomItemTemplateSelector
		{
			get
			{
				return this.customItemTemplateSelector;
			}
			set
			{
				this.customItemTemplateSelector = value;
			}
		}

		public ExampleViewModel()
		{
			this.Data = Product.GetData(15);
		}
	}
}
