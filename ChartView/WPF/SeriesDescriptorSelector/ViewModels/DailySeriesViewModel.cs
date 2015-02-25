using System;
using System.Collections.Generic;
using System.Windows.Media;

namespace SeriesDescriptorSelector
{
    public class DailySeriesViewModel : SeriesViewModelBase
    {
        public Brush DailyStroke { get; set; }

        public DailySeriesViewModel()
        {
            this.DailyStroke = new SolidColorBrush(Colors.Green);
            this.DailyData = new List<DailyProductInfo>();
        }

        public List<DailyProductInfo> DailyData { get; private set; }
    }
}
