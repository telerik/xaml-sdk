using System;
using System.Collections.ObjectModel;
using Telerik.Windows.Controls;

namespace DragChartAnnotation
{
    public class MainViewModel : ViewModelBase
    {
        private static Random RandomNumberGenerator = new Random();
        private double lineAnnotationPosition;

        public ObservableCollection<PlotInfo> Items { get; set; }
        
        public double LineAnnotationPosition
        {
            get
            {
                return this.lineAnnotationPosition;
            }
            set
            {
                if (this.lineAnnotationPosition != value)
                {
                    this.lineAnnotationPosition = value;
                    OnPropertyChanged("LineAnnotationPosition");
                }
            }
        }        

        public MainViewModel()
        {
            this.Items = new ObservableCollection<PlotInfo>();
            for (int i = 0; i < 30; i++)
            {
                this.Items.Add(new PlotInfo() { Category = "C" + i, Value = RandomNumberGenerator.Next(100, 300) });
            }

            this.LineAnnotationPosition = this.Items[10].Value;
        }
    }
}
