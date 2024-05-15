using System;
using System.Collections.ObjectModel;
using System.Linq;

namespace ColumnChooser
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
    }

    public class MyObject
    {
        public int ID { get; set; }
        public string Name { get; set; }
    }
}
