using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.Collections.ObjectModel;
using WpfApplication1;

namespace CollectionEditorCustomDataType
{
    public class MyViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private ObservableCollection<CustomString> values;

        public ObservableCollection<CustomString> Values
        {
            get
            {
                if (this.values == null)
                {
                    this.values = this.GenerateData();
                }

                return this.values;
            }
        }

        private ObservableCollection<CustomString> GenerateData()
        {
            ObservableCollection<CustomString> data = new ObservableCollection<CustomString>();

            for (int i = 0; i < 10; i++)
            {
                data.Add("Value " + i.ToString());
            }

            return data;
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
    }
}
