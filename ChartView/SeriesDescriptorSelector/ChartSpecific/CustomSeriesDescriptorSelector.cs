using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Telerik.Windows.Controls.ChartView;

namespace SeriesDescriptorSelector
{
    public class CustomSeriesDescriptorSelector : ChartSeriesDescriptorSelector
    {
        public ChartSeriesDescriptor DailyDescriptor { get; set; }
        public ChartSeriesDescriptor MonthDescriptor { get; set; }

        public override ChartSeriesDescriptor SelectDescriptor(ChartSeriesProvider provider, object context)
        {
            DailySeriesViewModel dailyVM = context as DailySeriesViewModel;
            MonthSeriesViewModel monthlyVM = context as MonthSeriesViewModel;

            if (dailyVM != null)
            {
                return this.DailyDescriptor;
            }
            else if (monthlyVM != null)
            {
                return this.MonthDescriptor;
            }

            throw new NotSupportedException("Not supported series viewmodel type.");
        }
    }
}
