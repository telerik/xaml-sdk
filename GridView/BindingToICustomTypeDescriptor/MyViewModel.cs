using System;
using System.Collections.ObjectModel;
using Telerik.Windows.Controls;

namespace BindingToICustomTypeDescriptor
{
    public class MyViewModel : ViewModelBase
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

            MyDataRow row = new MyDataRow();

            row.AddPropertyValue("Name", "Liverpool");
            row.AddPropertyValue("StadiumCapacity", 45362);
            row.AddPropertyValue("Established", new DateTime(1892, 1, 1));
            items.Add(row);

            row = new MyDataRow();
            row.AddPropertyValue("Name", "Chelsea");
            row.AddPropertyValue("StadiumCapacity", 42055);
            row.AddPropertyValue("Established", new DateTime(1905, 1, 1));
            items.Add(row);

            row = new MyDataRow();
            row.AddPropertyValue("Name", "Manchester Utd.");
            row.AddPropertyValue("StadiumCapacity", 76212);
            row.AddPropertyValue("Established", new DateTime(1878, 1, 1));
            items.Add(row);

            row = new MyDataRow();
            row.AddPropertyValue("Name", "Arsenal");
            row.AddPropertyValue("StadiumCapacity", 60355);
            row.AddPropertyValue("Established", new DateTime(1886, 1, 1));
            items.Add(row);

            return items;
        }
    }
}
