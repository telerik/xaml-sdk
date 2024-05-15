using Telerik.Windows.Controls;

namespace SaveLoadLayout
{
	public class LayoutDataViewModel : ViewModelBase
	{
		private string xml;

		public string Xml
		{
			get
			{
				return this.xml;
			}

			set
			{
				if (this.xml != value)
				{
					this.xml = value;
					this.OnPropertyChanged("Xml");
				}
			}
		}
	}
}
