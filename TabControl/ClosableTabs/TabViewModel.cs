using System;
using System.ComponentModel;
using Telerik.Windows.Controls;

namespace ClosableTabs
{
    public class TabViewModel : INotifyPropertyChanged, IDisposable
    {
        private bool isSelected;
        private readonly MainViewModel mainViewModel;

        public TabViewModel(MainViewModel mainViewModel)
        {
            this.mainViewModel = mainViewModel;
            this.mainViewModel.Tabs.CollectionChanged += this.Tabs_CollectionChanged;

            this.AddItemCommand = new DelegateCommand(
                delegate
                {
                    this.mainViewModel.AddItem(this);
                },
                delegate
                {
                    return this.mainViewModel.Tabs.Count < 5;
                });

            this.RemoveItemCommand = new DelegateCommand(
                delegate
                {
                    this.mainViewModel.RemoveItem(this);
                },
                delegate
                {
                    return this.mainViewModel.Tabs.Count > 1;
                });
        }

        public void Dispose()
        {
            this.mainViewModel.Tabs.CollectionChanged -= this.Tabs_CollectionChanged;
        }

        void Tabs_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            this.AddItemCommand.InvalidateCanExecute();
            this.RemoveItemCommand.InvalidateCanExecute();
        }

        public string Header
        {
            get;
            set;
        }

        public bool IsSelected
        {
            get
            {
                return this.isSelected;
            }
            set
            {
                if (this.isSelected != value)
                {
                    this.isSelected = value;
                    this.OnPropertyChanged("IsSelected");
                }
            }
        }

        public DelegateCommand AddItemCommand { get; set; }
        public DelegateCommand RemoveItemCommand { get; set; }

        #region INotifyPropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged(string propertyName)
        {
            if (this.PropertyChanged != null)
            {
                this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
        #endregion
    }
}
