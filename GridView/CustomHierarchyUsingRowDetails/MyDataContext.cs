using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
namespace CustomColumn
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
                    view = new ObservableCollection<MyObject>(from i in Enumerable.Range(0, 100) select 
                                                                  new MyObject() 
                                                                  { 
                                                                      ID = i, 
                                                                      Name = string.Format("Name{0}", i),
                                                                      IsExpandable = i < 50,
                                                                      IsExpanded = i < 50 && i % 3 == 0
                                                                  });
                }

                return view;
            }
        }
    }

    public class MyObject : INotifyPropertyChanged
    {
        private int id;
        public int ID
        {
            get { return id; }
            set 
            {
                if (id != value)
                {
                    id = value;
                    this.OnPropertyChanged("ID");
                }
            }
        }

        private string name;
        public string Name
        {
            get { return name; }
            set
            {
                if (name != value)
                {
                    name = value;
                    this.OnPropertyChanged("Name");
                }
            }
        }

        private bool isExpandable;
        public bool IsExpandable
        {
            get { return isExpandable; }
            set
            {
                if (isExpandable != value)
                {
                    isExpandable = value;
                    this.OnPropertyChanged("IsExpandable");
                }
            }
        }

        private bool isExpanded;
        public bool IsExpanded
        {
            get { return isExpanded; }
            set
            {
                if (isExpanded != value)
                {
                    isExpanded = value;
                    this.OnPropertyChanged("IsExpanded");
                }
            }
        }

        private void OnPropertyChanged(string propertyName)
        {
            if (this.PropertyChanged != null)
            {
                this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
