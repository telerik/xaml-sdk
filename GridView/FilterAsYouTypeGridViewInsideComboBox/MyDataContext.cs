using System;
using System.Linq;
using Telerik.Windows.Controls;
using Telerik.Windows.Data;

namespace FilterAsYouTypeGridViewInsideComboBox
{
    public class MyDataContext : ViewModelBase
    {
        QueryableCollectionView items;
        public QueryableCollectionView Items
        {
            get
            {
                if (items == null)
                {
                    items = new QueryableCollectionView(from i in Enumerable.Range(0, 10000)
                                                         select new MyObject() { ID = i, Name = String.Format("Name{0}", i) });

                    items.FilterDescriptors.Add(new FilterDescriptor("Name", FilterOperator.Contains, text != null ? text : ""));
                }
                return items;
            }
        }

        string text;
        public string Text
        {
            get
            {
                return text;
            }
            set
            {
                if (text != value)
                {
                    text = value;

                    if (items != null)
                    {
                        ((FilterDescriptor)items.FilterDescriptors[0]).Value = text;
                    }

                    this.OnPropertyChanged("Text");
                }
            }
        }

        MyObject selectedItem;
        public MyObject SelectedItem
        {
            get
            {
                return selectedItem;
            }
            set
            {
                if (selectedItem != value)
                {
                    selectedItem = value;

                    if (selectedItem != null)
                    {
                        text = selectedItem.Name;
                    }

                    this.OnPropertyChanged("Text");

                    this.OnPropertyChanged("SelectedItem");
                }
            }
        }
    }

    public class MyObject : ViewModelBase
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
    }
}
