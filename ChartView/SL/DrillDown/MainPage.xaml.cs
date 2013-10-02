using System;
using System.Windows;
using System.Windows.Controls;
using DrillDown.ChartUtilities;

namespace DrillDown
{
    public partial class MainPage : UserControl
    {
        public MainPage()
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
