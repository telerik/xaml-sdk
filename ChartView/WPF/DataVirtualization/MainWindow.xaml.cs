using System.Windows;

namespace DataVirtualization
{
    public partial class MainWindow : Window
    {
        ChartDataVirtualizationViewModel model;

        public MainWindow()
        {
            InitializeComponent();
            this.DataContext = this.model = new ChartDataVirtualizationViewModel();
        }

        private void LinearAxis_ActualVisibleRangeChanged(object sender, Telerik.Charting.NumericalRangeChangedEventArgs e)
        {
            this.model.UpdateVisibleData(e.NewRange.Minimum, e.NewRange.Maximum);
        }
    }
}
