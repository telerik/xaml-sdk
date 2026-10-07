using System;
using Telerik.Windows.Controls;

namespace DragDrop
{
    public class DataItem : ViewModelBase, ITimelineItem
    {
        private DateTime startDate;

        public TimeSpan Duration { get; set; }
        public int RowIndex { get; set; }
        public object GroupKey { get; set; }

        public DateTime StartDate
        {
            get
            {
                return this.startDate;
            }
            set
            {
                if (this.startDate != value)
                {
                    this.startDate = value;
                    OnPropertyChanged("StartDate");
                }
            }
        }

        public DataItem()
        {
            this.RowIndex = -1;
        }
    }
}
