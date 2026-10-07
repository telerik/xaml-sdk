using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace TrackBallLikeAnnotations
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            this.KeyDown += new KeyEventHandler(MainWindow_KeyDown);

            var vm = new MainViewModel();
            vm.Data1 = new List<PlotInfo> 
            { 
                new PlotInfo { Date = new DateTime(2013, 11, 21), YVal = 40, },
                new PlotInfo { Date = new DateTime(2013, 11, 22), YVal = 50, },
                new PlotInfo { Date = new DateTime(2013, 11, 23), YVal = 90, },
                new PlotInfo { Date = new DateTime(2013, 11, 24), YVal = 90, },
                new PlotInfo { Date = new DateTime(2013, 11, 25), YVal = 40, },
                new PlotInfo { Date = new DateTime(2013, 11, 26), YVal = 30, },
                new PlotInfo { Date = new DateTime(2013, 11, 27), YVal = 40, },
                new PlotInfo { Date = new DateTime(2013, 11, 28), YVal = 40, },
            };
            vm.Data2 = new List<PlotInfo> 
            { 
                new PlotInfo { Date = new DateTime(2013, 11, 21), YVal = 500, },
                new PlotInfo { Date = new DateTime(2013, 11, 22), YVal = 800, },
                new PlotInfo { Date = new DateTime(2013, 11, 23), YVal = 1100, },
                new PlotInfo { Date = new DateTime(2013, 11, 24), YVal = 1100, },
                new PlotInfo { Date = new DateTime(2013, 11, 25), YVal = 1200, },
                new PlotInfo { Date = new DateTime(2013, 11, 26), YVal = 1000, },
                new PlotInfo { Date = new DateTime(2013, 11, 27), YVal = 700, },
                new PlotInfo { Date = new DateTime(2013, 11, 28), YVal = 900, },
            };

            this.DataContext = vm;
        }

        void MainWindow_KeyDown(object sender, KeyEventArgs e)
        {
            if (cbNavigate.IsChecked == true)
            {
                if (e.Key == Key.Right)
                {
                    ChartUtilities.RightKeyPressed();
                }
                if (e.Key == Key.Left)
                {
                    ChartUtilities.LeftKeyPressed();
                }
            }
        }

        private void CheckBox_Click(object sender, RoutedEventArgs e)
        {
            ChartUtilities.HidesAnnotationsOnMouseLeave = ((CheckBox)sender).IsChecked == true;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            DateTime date;
            Random r = new Random();
            switch (r.Next(5))
            {
                case 0: date = new DateTime(2013, 11, 21); break;
                case 1: date = new DateTime(2013, 11, 22); break;
                case 2: date = new DateTime(2013, 11, 23); break;
                case 3: date = new DateTime(2013, 11, 24); break;
                case 4: date = new DateTime(2013, 11, 25); break;
                case 5: date = new DateTime(2013, 11, 26); break;
                case 6: date = new DateTime(2013, 11, 27); break;
                default: date = new DateTime(2013, 11, 28); break;
            }

            ChartUtilities.UpdateCharts("g1", date);
        }
    }
}
