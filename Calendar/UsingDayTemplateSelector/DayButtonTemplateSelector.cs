using System;
using System.Windows;
using System.Windows.Controls;
using Telerik.Windows.Controls.Calendar;
using System.Linq;
using System.Collections.Generic;
using Telerik.Windows.Controls;

namespace UsingDayTemplateSelector
{
    public class DayButtonTemplateSelector : DataTemplateSelector
    {
        public DataTemplate DefaultTemplate { get; set; }
        public DataTemplate BookedDayTemplate { get; set; }
        public DataTemplate SpecialHolidayTemplate { get; set; }

        public List<DateTime> BookedDays { get; set; }
        public List<DateTime> SpecialHolidays { get; set; }

        public DayButtonTemplateSelector()
        {
            this.BookedDays = new List<DateTime>()
            {
                DateTime.Now.AddDays(2),
                DateTime.Now.AddDays(6),
                DateTime.Now.AddDays(12)
            };

            this.SpecialHolidays = new List<DateTime>()
            {
                DateTime.Now.AddDays(1),
                DateTime.Now.AddDays(4),
                DateTime.Now.AddDays(9),
                DateTime.Now.AddDays(16)
            };
        }

        public override DataTemplate SelectTemplate(object item, DependencyObject container)
        {
            var calendarButton = item as CalendarButtonContent;
            var currDate = calendarButton.Date;

            if (calendarButton.ButtonType == CalendarButtonType.Date)
            {
                if (this.BookedDays.Any(a => a.Date.Day == currDate.Day))
                {
                    return this.BookedDayTemplate;
                }

                if (this.SpecialHolidays.Any(a => a.Date.Day == currDate.Day))
                {
                    return this.SpecialHolidayTemplate;
                }
            }

            return this.DefaultTemplate;
        }

    }
}
