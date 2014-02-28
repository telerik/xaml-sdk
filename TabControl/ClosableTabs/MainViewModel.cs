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

namespace ClosableTabs
{
    public class MainViewModel
    {
        public MainViewModel()
        {
            this.Tabs = new ObservableCollection<TabViewModel>();
            this.AddItem(null);
        }

        public ObservableCollection<TabViewModel> Tabs
        {
            get;
            private set;
        }

        public void AddItem(TabViewModel sender)
        {
            TabViewModel newTabItem = new TabViewModel(this);
            newTabItem.Header = "New Tab";
            newTabItem.IsSelected = true;
            if (sender != null)
            {
                int insertIndex = this.Tabs.IndexOf(sender) + 1;
                this.Tabs.Insert(insertIndex, newTabItem);
            }
            else
            {
                this.Tabs.Add(newTabItem);
            }
        }

        public void RemoveItem(TabViewModel tabItem)
        {
            this.Tabs.Remove(tabItem);
            tabItem.Dispose();
        }
    }
}
