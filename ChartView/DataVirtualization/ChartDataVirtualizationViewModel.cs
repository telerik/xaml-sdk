using System;
using System.Collections.ObjectModel;
using System.Linq;
using Telerik.Windows.Controls;

namespace DataVirtualization
{
    public class ChartDataVirtualizationViewModel : ViewModelBase
    {
        private const int FullDataCount = 1000000;
        private const double RangeOffset = 0.01;
        private static Random randomNumberGenerator = new Random();        

        private ObservableCollection<PlotInfo> fullData;
        private ObservableCollection<PlotInfo> visibleData;                
        private double minimumX;
        private double maximumX;

        public ChartDataVirtualizationViewModel()
        {
            this.fullData = this.GenerateFullData();
            this.MinimumX = this.fullData.FirstOrDefault().XValue;
            this.MaximumX = this.fullData.LastOrDefault().XValue;
            this.visibleData = new ObservableCollection<PlotInfo>();                        
        }

        public double MinimumX
        {
            get { return this.minimumX; }
            set
            {
                if (this.minimumX != value)
                {
                    this.minimumX = value;
                    this.OnPropertyChanged("MinimumX");
                }
            }
        }

        public double MaximumX
        {
            get { return this.maximumX; }
            set
            {
                if (this.maximumX != value)
                {
                    this.maximumX = value;
                    this.OnPropertyChanged("MaximumX");
                }
            }
        }

        public ObservableCollection<PlotInfo> FullData
        {
            get { return this.fullData; }
        }

        public ObservableCollection<PlotInfo> VisibleData
        {
            get { return this.visibleData; }
            private set
            {
                if (this.visibleData != value)
                {
                    this.visibleData = value;
                    this.OnPropertyChanged("VisibleData");
                }
            }
        }      

        public void UpdateVisibleData(double minimum, double maximum)
        {
            double delta = maximum - minimum;
            double offset = delta * RangeOffset;
            double offsetMinimum = minimum - offset;
            double offsetMaximum = maximum + offset;

            this.VisibleData = this.GetVisibleItems(offsetMinimum, offsetMaximum);
        }

        private ObservableCollection<PlotInfo> GetVisibleItems(double minimum, double maximum)
        {
            ObservableCollection<PlotInfo> result = new ObservableCollection<PlotInfo>();
            for (int i = 0; i < this.fullData.Count; i++)
            {
                PlotInfo info = this.fullData[i];
                if (minimum <= info.XValue && info.XValue <= maximum)
                {
                    result.Add(info);
                }
            }
            return result;
        }

        private ObservableCollection<PlotInfo> GenerateFullData()
        {
            ObservableCollection<PlotInfo> result = new ObservableCollection<PlotInfo>();
            for (int i = 0; i < FullDataCount; i++)
            {
                result.Add(new PlotInfo() { XValue = i, YValue = randomNumberGenerator.Next(100, 300) });
            }
            return result;
        }
    }
}
