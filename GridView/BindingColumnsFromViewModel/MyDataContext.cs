using System;
using System.Collections.ObjectModel;
using System.Linq;
using Telerik.Windows.Controls;

namespace BindingColumnsFromViewModel
{
    public class MyDataContext
    {
        ObservableCollection<MyObject> view;
        public ObservableCollection<MyObject> View
        {
            get
            {
                if (view == null)
                {
                    view = new ObservableCollection<MyObject>(from i in Enumerable.Range(0, 100) select new MyObject() { ID = i, Name = string.Format("Name{0}", i) });
                }

                return view;
            }
        }

        ObservableCollection<GridViewColumn> columns;
        public ObservableCollection<GridViewColumn> Columns
        {
            get
            {
                if (columns == null)
                {
                    columns = new ObservableCollection<GridViewColumn>();
                    columns.Add(new GridViewDataColumn() { DataMemberBinding = new System.Windows.Data.Binding("ID") });
                    columns.Add(new GridViewDataColumn() { DataMemberBinding = new System.Windows.Data.Binding("Name") });
                }
                return columns;
            }
        }

    }

    public class MyObject
    {
        public int ID { get; set; }
        public string Name { get; set; }
    }
}
