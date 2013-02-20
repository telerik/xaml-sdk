using System;
using System.Linq;
using System.ComponentModel;
using System.Collections.ObjectModel;

namespace BringIntoView
{
    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public class BusinessItem : INotifyPropertyChanged
    {
        public BusinessItem(BusinessItem parent)
        {
            this.Items = new ObservableCollection<BusinessItem>();
            this.Parent = parent;
        }

        public ObservableCollection<BusinessItem> Items { get; set; }
        public BusinessItem Parent { get; private set; }

        public string Name { get; set; }
        public int Level { get; set; }

        private bool isSelected;
        public bool IsSelected
        {
            get
            {
                return isSelected;
            }
            set
            {
                if (value != isSelected)
                {
                    isSelected = value;
                    OnPropertyChanged("IsSelected");
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(String propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        public string GetPath()
        {
            string path = this.Name;
            BusinessItem nextParent = this.Parent;

            while (nextParent != null)
            {
                path = nextParent.Name + @"\" + path;
                nextParent = nextParent.Parent;
            }

            return path;
        }
    }
}
