using System;
using System.Windows;
using DrillDown.ChartUtilities;

namespace DrillDown
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            this.DataContext = new MainViewModel();
        }

        private void ChartSelectionBehavior_SelectionChanged(object sender, Telerik.Windows.Controls.ChartView.ChartSelectionChangedEventArgs e)
        {
            this.Dispatcher.BeginInvoke((Action)(() =>
            {
                MainViewModel vm = (MainViewModel)this.DataContext;
                if (vm != null && e.AddedPoints.Count > 0)
                {
                    vm.HandleItemClicked(e.AddedPoints[0].DataItem);
                }
            }));
        }

        private void ButtonBack_Click(object sender, RoutedEventArgs e)
        {
            MainViewModel vm = this.DataContext as MainViewModel;
            if (vm != null)
            {
                vm.DrillUp();
            }
        }

        private void ButtonForward_Click(object sender, RoutedEventArgs e)
        {
            MainViewModel vm = this.DataContext as MainViewModel;
            if (vm != null)
            {
                vm.DrillDown();
            }
        }
    }
}
