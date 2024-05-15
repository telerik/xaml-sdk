using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using Telerik.Windows.Controls;

namespace MinimumPopulateDelay
{
    public class ViewModel : ViewModelBase
    {
        private ObservableCollection<Item> items;
        private ObservableCollection<int> delays;
        private int selectedDelay;

        public ViewModel()
        {
            this.items = this.GetItems(20);
            this.delays = new ObservableCollection<int>()
            {
                1, 2, 3, 4, 5
            };
            this.selectedDelay = this.delays[1];
        }

        public int SelectedDelay
        {
            get
            {
                return this.selectedDelay;
            }

            set
            {
                if (this.selectedDelay != value)
                {
                    this.selectedDelay = value;
                    this.OnPropertyChanged(() => this.SelectedDelay);
                }
            }
        }

        public ObservableCollection<int> Delays
        {
            get
            {
                return this.delays;
            }

            set
            {
                if (this.delays != value)
                {
                    this.delays = value;
                    this.OnPropertyChanged(() => this.Delays);
                }
            }
        }

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
                result.Add(new Item() { Name = string.Format("Item {0}", i), Number = i });
            }

            return result;
        }
    }
}
