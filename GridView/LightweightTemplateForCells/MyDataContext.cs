using System;
using System.Collections.ObjectModel;
using System.Linq;

namespace LightweightTemplateForCells
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
                    view = new ObservableCollection<MyObject>(from i in Enumerable.Range(0, 1000)
                                                              select
                                                                  new MyObject()
                                                                  {
                                                                      ID = i
                                                                  });
                }

                return view;
            }
        }
    }

    public class MyObject
    {
        public int ID { get; set; }
    }
}
