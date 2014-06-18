using System;
using System.Collections.ObjectModel;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using Telerik.Windows.Controls;

namespace TreeListViewBringItem
{
    public class DataItem : ViewModelBase
    {
        private string name;
        public string Name
        {
            get { return this.name; }
            set
            {
                if (this.name != value)
                {
                    this.name = value;
                    this.OnPropertyChanged("Name");
                }
            }
        }

        private ObservableCollection<DataItem> children;
        public ObservableCollection<DataItem> Children
        {
            get
            {
                if (children == null)
                {
                    children = new ObservableCollection<DataItem>();
                    for (int i = 0; i < 100; i++)
                    {
                        DataItem item = new DataItem() { Name = this.Name + "." + i};
                        children.Add(item);
                    }
                }
                return children;
            }
        }

        public bool IsExpandable
        {
            get
            {
                return true;
            }
        }
    }
}
