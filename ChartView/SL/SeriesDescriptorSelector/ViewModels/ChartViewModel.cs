using System;
using System.Collections.Generic;

namespace SeriesDescriptorSelector
{
    public class ChartViewModel
    {
        public ChartViewModel()
        {
            this.SeriesData = this.GetSeriesData();
        }

        public List<SeriesViewModelBase> SeriesData { get; set; }

        private List<SeriesViewModelBase> GetSeriesData()
        {
            List<SeriesViewModelBase> seriesData = new List<SeriesViewModelBase>();

            Random r = new Random();
            DateTime date = new DateTime(2015, 1, 1);
            MonthSeriesViewModel svm1 = new MonthSeriesViewModel();
            for (int i = 0; i < 12; i++)
            {
                svm1.MonthData.Add(new MonthSaleInfo { Date = date.AddMonths(i), TotalAmmount = r.Next(600, 850), });
            }
            seriesData.Add(svm1);

            DailySeriesViewModel svm2 = new DailySeriesViewModel();
            svm2.DailyData.Add(new DailyProductInfo { Date = date, UnitsSold = 70 });
            for (int i = 1; i < 350; i++)
            {
                svm2.DailyData.Add(new DailyProductInfo { Date = date.AddDays(i), UnitsSold = r.Next(30, 45), });
            }
            seriesData.Add(svm2);

            return seriesData;
        }
    }
}
