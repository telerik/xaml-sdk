using System.Collections.ObjectModel;

namespace SeriesProvider
{
    public class SeriesViewModel
    {
        public string SeriesType { get; set; }

        public ObservableCollection<DataItem> Items { get; set; }
    }
}
