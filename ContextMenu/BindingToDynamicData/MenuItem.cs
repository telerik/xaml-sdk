using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Windows.Input;

namespace BindingToDynamicData
{
    public class MenuItem
    {
        public MenuItem()
        {
            this.SubItems = new ObservableCollection<MenuItem>();
        }
        public string Text
        {
            get;
            set;
        }
        public Uri IconUrl
        {
            get;
            set;
        }
        public bool IsSeparator
        {
            get;
            set;
        }
        public ICommand Command
        {
            get;
            set;
        }
        public ObservableCollection<MenuItem> SubItems
        {
            get;
            set;
        }
    }
}
