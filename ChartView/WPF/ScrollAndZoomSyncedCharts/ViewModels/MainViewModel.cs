using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace ScrollAndZoomSyncedCharts
{
    public class MainViewModel : INotifyPropertyChanged
    {
        public List<UsersData> UsersData1 { get; private set; }
        public List<UsersData> UsersData2 { get; private set; }

        private double selectionStart;
        private double selectionEnd;

        public double SelectionStart
        {
            get { return this.selectionStart; }
            set
            {
                if (this.selectionStart != value)
                {
                    this.selectionStart = value;
                    this.Raise(this.PropertyChanged, "SelectionStart");
                }
            }
        }

        public double SelectionEnd
        {
            get { return this.selectionEnd; }
            set
            {
                if (this.selectionEnd != value)
                {
                    this.selectionEnd = value;
                    this.Raise(this.PropertyChanged, "SelectionEnd");
                }
            }
        }

        public MainViewModel()
        {
            this.selectionStart = 0.3;
            this.SelectionEnd = 0.8;

            this.UsersData1 = new List<UsersData> 
            {
                new UsersData { Date = new DateTime(2013, 4, 22), UsersCount = 10000 },
                new UsersData { Date = new DateTime(2013, 5, 22), UsersCount = 20000 },
                new UsersData { Date = new DateTime(2013, 6, 1), UsersCount = 22000 },
                new UsersData { Date = new DateTime(2013, 7, 1), UsersCount = 35000 },
                new UsersData { Date = new DateTime(2013, 8, 28), UsersCount = 40000 },
            };

            this.UsersData2 = new List<UsersData> 
            {
                new UsersData { Date = new DateTime(2013, 4, 22), UsersCount = 330 },
                new UsersData { Date = new DateTime(2013, 5, 2), UsersCount = 700 },
                new UsersData { Date = new DateTime(2013, 6, 12), UsersCount = 200 },
                new UsersData { Date = new DateTime(2013, 7, 13), UsersCount = 860 },
                new UsersData { Date = new DateTime(2013, 8, 28), UsersCount = 1234 },
            };
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void Raise(PropertyChangedEventHandler handler, string propertyName)
        {
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}
