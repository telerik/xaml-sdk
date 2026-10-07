using System;
using System.Collections.ObjectModel;

namespace DragDrop
{
    public class MainViewModel
    {
        public static Random RandomNumberGenerator = new Random();

        public ObservableCollection<DataItem> Items { get; set; }
        public DateTime PeriodStart { get; set; }
        public DateTime PeriodEnd { get; set; }

        public MainViewModel()
        {
            this.PeriodStart = new DateTime(2015, 6, 16);
            this.PeriodEnd = new DateTime(2017, 10, 16);
            this.Items = GetSampleData();           
        }

        public ObservableCollection<DataItem> GetSampleData()
        {            
            var startDate = this.PeriodStart.AddDays(15);
            var source = new ObservableCollection<DataItem>();

            for (int i = 0; i < 5; i++)
            {
                source.Add(new DataItem()
                {
                    GroupKey = "GroupA",
                    StartDate = startDate,
                    Duration = TimeSpan.FromDays(RandomNumberGenerator.Next(10, 60)),
                    RowIndex = i % 2 == 0 ? 0 : 1
                });
                startDate = startDate.AddDays(60);
            }

            startDate = this.PeriodStart.AddDays(15);
            for (int i = 0; i < 5; i++)
            {
                source.Add(new DataItem() { GroupKey = "GroupB", StartDate = startDate });
                startDate = startDate.AddDays(60);
            }

            return source;
        }
    }
}
