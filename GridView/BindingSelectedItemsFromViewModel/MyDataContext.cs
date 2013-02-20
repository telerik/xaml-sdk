using System;
using System.Collections.ObjectModel;
using System.Linq;
using Telerik.Windows.Controls;

namespace BindingSelectedItemsFromViewModel
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

        ObservableCollection<object> selectedItems;
        public ObservableCollection<object> SelectedItems
        {
            get
            {
                if (selectedItems == null)
                {
                    selectedItems = new ObservableCollection<object>();
                    selectedItems.Add(View[0]);
                    selectedItems.Add(View[1]);
                }
                return selectedItems;
            }
        }

    }

    public class MyObject
    {
        public int ID { get; set; }
        public string Name { get; set; }
    }
}
