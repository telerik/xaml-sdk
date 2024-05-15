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

        public ObservableCollection<DataItem> SelectedCartesianDataItems { get; set; }
        public ObservableCollection<DataItem> SelectedPieDataItems { get; set; }

        public MainViewModel()
        {
            this.CartesianDataItems = GetRandomData(10);
            this.SelectedCartesianDataItems = new ObservableCollection<DataItem>();

            this.PieDataItems = GetRandomData(5);
            this.SelectedPieDataItems = new ObservableCollection<DataItem>();
            this.SelectedPieDataItems.Add(this.PieDataItems.Last());
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
