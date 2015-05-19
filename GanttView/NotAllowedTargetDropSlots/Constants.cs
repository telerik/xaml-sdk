using System;
using System.Collections.ObjectModel;
using Telerik.Windows.Controls.Scheduling;

namespace NotAllowedTargetDropSlots
{
    public static class Constants
    {
        public static DateTime ExampleStartDate = new DateTime(2015, 5, 19);

        public static ObservableCollection<DateRange> LockedRanges = new ObservableCollection<DateRange>() 
        {
            new DateRange(Constants.ExampleStartDate.AddDays(1), Constants.ExampleStartDate.AddDays(4)),
            new DateRange(Constants.ExampleStartDate.AddDays(9), Constants.ExampleStartDate.AddDays(12))
        };
    }
}
