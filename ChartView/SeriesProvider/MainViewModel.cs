using System.Collections.ObjectModel;

namespace SeriesProvider
{
    public class MainViewModel
    {
        public ObservableCollection<SeriesViewModel> Data
        {
            get;
            set;
        }

        public MainViewModel()
        {
            this.Data = new ObservableCollection<SeriesViewModel>()
            {
                new SeriesViewModel()
                {
                    SeriesType = "Bar",
                    Items = new ObservableCollection<DataItem>()
                    {
                        new DataItem() { Category = "A", Value = 5},
                        new DataItem() { Category = "B", Value = 7},
                        new DataItem() { Category = "C", Value = 6},
                        new DataItem() { Category = "D", Value = 8}
                    }
                },
                new SeriesViewModel()
                {
                    SeriesType = "Bar",
                    Items = new ObservableCollection<DataItem>()
                    {
                        new DataItem() { Category = "A", Value = 15},
                        new DataItem() { Category = "B", Value = 18},
                        new DataItem() { Category = "C", Value = 19},
                        new DataItem() { Category = "D", Value = 23}
                    }
                },
                new SeriesViewModel()
                {
                    SeriesType = "Line",
                    Items = new ObservableCollection<DataItem>()
                    {
                        new DataItem() { Category = "A", Value = 21},
                        new DataItem() { Category = "B", Value = 25},
                        new DataItem() { Category = "C", Value = 26},
                        new DataItem() { Category = "D", Value = 25}
                    }
                }
            };
        }
    }
}
