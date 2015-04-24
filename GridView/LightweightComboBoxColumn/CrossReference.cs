using System.ComponentModel;

namespace LightweightComboBoxColumn
{
    public class CrossReference : INotifyPropertyChanged, IDataErrorInfo
    {
        public event PropertyChangedEventHandler PropertyChanged;
        
        private int itemKey;
        public int ItemKey 
        {
            get
            {
                return this.itemKey;
            }
            set
            {
                if (value != this.itemKey)
                {
                    this.itemKey = value;
                    this.OnPropertyChanged("ItemKey");
                }
            }
        }

        private string customerItem;
        public string CustomerItem 
        {
            get
            {
                return this.customerItem;
            }
            set
            {
                if (value != this.customerItem)
                {
                    this.customerItem = value;
                    this.OnPropertyChanged("CustomerItem");
                }
            } 
        }

		protected virtual void OnPropertyChanged(PropertyChangedEventArgs args)
		{
			PropertyChangedEventHandler handler = this.PropertyChanged;
			
            if (handler != null)
			{
				handler(this, args);
			}
		}

		private void OnPropertyChanged(string propertyName)
		{
			this.OnPropertyChanged(new PropertyChangedEventArgs(propertyName));
		}

        public string Error
        {
            get
            {
                return null;
            }
        }

        public string this[string columnName]
        {
            get
            {
                if (columnName == "ItemKey")
                {
                    if (this.ItemKey == 0)
                        return "ItemKey can not be 0";
                }

                if (columnName == "CustomerItem")
                {
                    if (string.IsNullOrWhiteSpace(this.CustomerItem))
                        return "CustomerItem can not be empty";
                }

                return null;
            }
        }
    }
}
