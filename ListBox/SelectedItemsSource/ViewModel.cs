using System.Collections.ObjectModel;
using System.Windows.Input;
using Telerik.Windows.Controls;

namespace SelectedItemsSource
{
    public class ViewModel : ViewModelBase
    {
        private ObservableCollection<Item> items;
        private ObservableCollection<Item> selectedItemsSource;

        public ViewModel()
        {
            this.Items = this.GetItems(100);
            this.SelectedItemsSource = new ObservableCollection<Item>() { this.Items[0], this.Items[2], this.Items[4], this.Items[6], this.Items[7] };
            this.AddItemCommand = new DelegateCommand(OnAddItemCommandExecuted);
            this.RemoveItemCommand = new DelegateCommand(OnRemoveItemCommandExecuted);
            this.ClearItemCommand = new DelegateCommand(OnClearItemCommandExecuted);
        }

        public ICommand AddItemCommand { get; set; }
        public ICommand RemoveItemCommand { get; set; }
        public ICommand ClearItemCommand { get; set; }

        /// <summary>
        /// Gets or sets SelectedItems and notifies for changes
        /// </summary>
        public ObservableCollection<Item> SelectedItemsSource
        {
            get
            {
                return this.selectedItemsSource;
            }

            set
            {
                if (this.selectedItemsSource != value)
                {
                    this.selectedItemsSource = value;
                    this.OnPropertyChanged(() => this.SelectedItemsSource);
                }
            }
        }

        /// <summary>
        /// Gets or sets Items and notifies for changes
        /// </summary>
        public ObservableCollection<Item> Items
        {
            get
            {
                return this.items;
            }

            set
            {
                if (this.items != value)
                {
                    this.items = value;
                    this.OnPropertyChanged(() => this.Items);
                }
            }
        }

        private ObservableCollection<Item> GetItems(int size)
        {
            var result = new ObservableCollection<Item>();
            for (int i = 0; i < size; i++)
            {
                result.Add(new Item(string.Format("Item {0}", i)));
            }

            return result;
        }

        private void OnAddItemCommandExecuted(object obj)
        {
            if (!this.SelectedItemsSource.Contains(this.Items[1]))
            {
                this.SelectedItemsSource.Add(this.Items[1]);
            }
        }

        private void OnRemoveItemCommandExecuted(object obj)
        {
            if (this.SelectedItemsSource.Contains(this.Items[1]))
            {
                this.SelectedItemsSource.Remove(this.Items[1]);
            }
        }

        private void OnClearItemCommandExecuted(object obj)
        {
            this.SelectedItemsSource.Clear();
        }
    }
}
