using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using Telerik.Windows.Controls;

namespace BindingToDynamicObject
{
    public class MyViewModel: ViewModelBase
    {
        private ObservableCollection<MyDataRow> data;

        public ObservableCollection<MyDataRow> Data 
        {
            get 
            {
                if (this.data == null)
                {
                    this.data = this.GenerateData();
                }

                return this.data;
            }
        }

        private ObservableCollection<MyDataRow> GenerateData()
        {
            ObservableCollection<MyDataRow> items = new ObservableCollection<MyDataRow>();

            for (int i = 0; i < 10; i++)
            {
                dynamic item = new MyDataRow();
                
                item["ID"] = i;
                item["Name"] = "Name " + i.ToString();

                items.Add(item);
            }

            return items;
        }
    }
}
