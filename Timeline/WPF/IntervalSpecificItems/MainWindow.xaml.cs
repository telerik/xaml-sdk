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
using Telerik.Windows.Controls;

namespace IntervalSpecificItems
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void RadTimeline1_ItemIntervalChanged(object sender, Telerik.Windows.Controls.TimeBar.DrillEventArgs e)
        {
            RadTimeline timeline = sender as RadTimeline;

            (timeline.DataContext as TimelineViewModel).CurrentInterval = timeline.CurrentItemInterval;
        }
    }
}
