using System;
using Telerik.Windows.Controls;

namespace RestrictTheEnteredDate
{
    public class ViewModel : ViewModelBase
    {
        private DateTime selectableDateStart;
        private DateTime selectableDateEnd;

        public ViewModel()
        {
            var currentYear = DateTime.Today.Year;
            var currentMonth = DateTime.Today.Month;

            this.SelectableDateStart = new DateTime(currentYear, currentMonth, 1);
            this.SelectableDateEnd = new DateTime(currentYear, currentMonth, DateTime.DaysInMonth(currentYear, currentMonth));
        }

        public DateTime SelectableDateStart
        {
            get
            {
                return this.selectableDateStart;
            }
            set
            {
                this.selectableDateStart = value;
                this.OnPropertyChanged(() => this.SelectableDateStart);
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
                this.selectableDateEnd = value;
                this.OnPropertyChanged(() => this.SelectableDateEnd);
            }
        }
    }
}
