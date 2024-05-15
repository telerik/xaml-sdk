using System;
using System.Collections.Generic;
using System.Linq;
using Telerik.Windows.Controls;

namespace AllowedDates
{
    public class ViewModel : ViewModelBase
    {
        private DateTime selectableDateEnd;
        private DateTime selectableDateStart;
        private List<DateTime> blackoutDates;

        public ViewModel()
        {
            DateTime today = DateTime.Today;
            this.SelectableDateStart = new DateTime(today.Year, today.Month, 1);
            this.SelectableDateEnd = new DateTime(today.Year, today.Month, DateTime.DaysInMonth(today.Year, today.Month));

            this.BlackoutDates = GetDatesInMonth(today.Year, today.Month);
        }

        public DateTime SelectableDateStart
        {
            get
            {
                return this.selectableDateStart;
            }

            set
            {
                if (this.selectableDateStart != value)
                {
                    this.selectableDateStart = value;
                    this.OnPropertyChanged(() => this.SelectableDateStart);
                }
            }
        }

        public DateTime SelectableDateEnd
        {
            get
            {
                return this.selectableDateEnd;
            }

            set
            {
                if (this.selectableDateEnd != value)
                {
                    this.selectableDateEnd = value;
                    this.OnPropertyChanged(() => this.SelectableDateEnd);
                }
            }
        }

        public List<DateTime> BlackoutDates
        {
            get
            {
                return this.blackoutDates;
            }

            set
            {
                if (this.blackoutDates != value)
                {
                    this.blackoutDates = value;
                    this.OnPropertyChanged(() => this.BlackoutDates);
                }
            }
        }

        public static List<DateTime> GetDatesInMonth(int year, int month)
        {
            var result = Enumerable.Range(1, DateTime.DaysInMonth(year, month))
                             .Select(day => new DateTime(year, month, day))
                             .Where(dt => dt.DayOfWeek == DayOfWeek.Sunday || dt.DayOfWeek == DayOfWeek.Saturday).ToList();
            return result;
        }
    }
}
