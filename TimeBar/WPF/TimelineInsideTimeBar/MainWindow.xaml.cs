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
using Telerik.Windows.Controls.Timeline;

namespace TimelineInsideTimeBar
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            var startDate = new DateTime(2010, 1, 1);
            var endDate = new DateTime(2011, 12, 31, 23, 59, 59);

            var items = new List<Item>();
            Random r = new Random();
            for (DateTime i = startDate; i < endDate; i = i.AddMonths(1))
            {
                items.Add(new Item() { Date = i, Duration = TimeSpan.FromDays(r.Next(50, 100)) });
            }

            for (int i = 0; i < 15; i++)
            {
                items.Add(new Item()
                {
                    Date = startDate.AddMonths(r.Next(0, 25)).AddDays(15)
                });
            }
            radTimeline.ItemsSource = items;
        }

        private void radTimeline_Loaded(object sender, RoutedEventArgs e)
        {
            var panel = ChildrenOfTypeExtensions.FindChildByType<TimelineAnnotationsPanel>(radTimeline);
            var timelineBorder = ChildrenOfTypeExtensions.ChildrenOfType<Border>(panel).FirstOrDefault();
            timelineBorder.Visibility = System.Windows.Visibility.Collapsed;
        }

    }

    public class Item
    {
        public TimeSpan Duration { get; set; }
        public DateTime Date { get; set; }
    }
}
