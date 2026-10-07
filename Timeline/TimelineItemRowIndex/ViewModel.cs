using System;
using System.Collections.Generic;

namespace TimelineItemRowIndex
{
    public class ViewModel
    {
        public ViewModel()
        {
            this.Items = new List<TimelineData>()
            {
                new TimelineData() { Date = new DateTime(2011, 1, 1), Time = TimeSpan.FromDays(16), RowIndex = 0},
                new TimelineData() { Date = new DateTime(2011, 1, 24), Time = TimeSpan.FromDays(20), RowIndex = 1},

                new TimelineData() { Date = new DateTime(2011, 4, 15), Time = TimeSpan.FromDays(40), RowIndex = 2},
                new TimelineData() { Date = new DateTime(2011, 3, 1), Time = TimeSpan.FromDays(55), RowIndex = 3},
                new TimelineData() { Date = new DateTime(2011, 4, 1), Time = TimeSpan.FromDays(20), RowIndex = 4},

                new TimelineData() { Date= new DateTime(2011, 6, 1), Time = TimeSpan.FromDays(29), RowIndex = 5},
                new TimelineData() { Date= new DateTime(2011, 7, 1), Time = TimeSpan.FromDays(17), RowIndex = 6},
                new TimelineData() { Date= new DateTime(2011, 8, 1), Time = TimeSpan.FromDays(50), RowIndex = 7},
                new TimelineData() { Date= new DateTime(2011, 9, 1), Time = TimeSpan.FromDays(13), RowIndex = 8},
                new TimelineData() { Date= new DateTime(2011, 10, 1), Time = TimeSpan.FromDays(7), RowIndex = 9},
                new TimelineData() { Date= new DateTime(2011, 11, 1), Time = TimeSpan.FromDays(40), RowIndex = 10},
                new TimelineData() { Date= new DateTime(2011, 12, 1), Time = TimeSpan.FromDays(16), RowIndex = 11},
            };
        }

        public List<TimelineData> Items { get; private set; }
    }
}
