using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telerik.Windows.Controls;

namespace TestApplication.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        private ObservableCollection<Item> items;

        public MainViewModel()
        {
            this.items = GetItems(100000);
        }

        private static ObservableCollection<Item> GetItems(int size)
        {
            var result = new ObservableCollection<Item>();

            for (int i = 1; i <= size; i++)
            {
                result.Add(new Item() { Name = string.Format("Item {0}", i) });
            }

            return result;
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
    }
}
