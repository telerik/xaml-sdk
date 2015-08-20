using System;
using System.Collections.ObjectModel;
using System.Linq;

namespace BindingSelectedItemsToViewModel
{
    public class MainViewModel
    {
        private Random randomNumberGenerator = new Random(0);
        public ObservableCollection<DataItem> CartesianDataItems { get; set; }
        public ObservableCollection<DataItem> PieDataItems { get; set; }

        public ObservableCollection<DataItem> SelectedPieDataItems { get; set; }
        public ObservableCollection<DataItem> SelectedCartesianDataItems { get; set; }

        public MainViewModel()
        {
            this.CartesianDataItems = GetRandomData(10);
            this.PieDataItems = GetRandomData(5);

            this.SelectedPieDataItems = new ObservableCollection<DataItem>();         
            this.SelectedCartesianDataItems = new ObservableCollection<DataItem>();

            this.SelectedCartesianDataItems.CollectionChanged += SelectedCartesianDataItems_CollectionChanged;

            this.SelectedPieDataItems.Add(this.PieDataItems.Last());
        }

        void SelectedCartesianDataItems_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            if (e.OldItems != null)
            {
                foreach (DataItem item in e.OldItems)
                {
                    item.IsSelected = false;
                }
            }

            if (e.NewItems != null)
            {
                foreach (DataItem item in e.NewItems)
                {
                    item.IsSelected = true;
                }
            }
        }

        private ObservableCollection<DataItem> GetRandomData(int itemsCount)
        {
            var result = new ObservableCollection<DataItem>();
            for (int i = 0; i < itemsCount; i++)
            {
                var dataPoint = new DataItem() { Category = "Category " + i, Value = randomNumberGenerator.Next(100, 300) };
                result.Add(dataPoint);
            }
            return result;
        }
    }
}
