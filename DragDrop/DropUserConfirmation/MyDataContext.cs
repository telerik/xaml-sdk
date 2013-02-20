using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;

namespace DragDrop
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

		ObservableCollection<MyObject> view2;
		public ObservableCollection<MyObject> View2
		{
			get
			{
				if (view2 == null)
				{
					view2 = new ObservableCollection<MyObject>(from i in Enumerable.Range(0, 100) select new MyObject() { ID = i, Name = string.Format("Name{0}", i) });
				}

				return view2;
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
