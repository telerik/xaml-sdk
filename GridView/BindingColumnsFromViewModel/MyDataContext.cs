using System;
using System.Collections.ObjectModel;
using System.Linq;
using Telerik.Windows.Controls;
using Telerik.Windows.Data;

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

        RadObservableCollection<GridViewColumn> columns;
        public RadObservableCollection<GridViewColumn> Columns
        {
            get
            {
                if (columns == null)
                {
                    columns = new RadObservableCollection<GridViewColumn>();

                    // The calls to SuspendNotifications and ResumeNotifications can be replaced with the AddRange method.
                    // Required when adding a large number of columns simultaneously to avoid performance issues.
                    columns.SuspendNotifications();
                    columns.Add(new GridViewDataColumn() { DataMemberBinding = new System.Windows.Data.Binding("ID") });
                    columns.Add(new GridViewDataColumn() { DataMemberBinding = new System.Windows.Data.Binding("Name") });
                    columns.ResumeNotifications();
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
