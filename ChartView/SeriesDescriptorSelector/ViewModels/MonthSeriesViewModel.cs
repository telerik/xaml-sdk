using System;
using System.Collections.Generic;

namespace SeriesDescriptorSelector
{
    public class MonthSeriesViewModel : SeriesViewModelBase
    {
        public MonthSeriesViewModel()
        {
            this.MonthData = new List<MonthSaleInfo>();
        }

        public List<MonthSaleInfo> MonthData { get; private set; }
    }
}
