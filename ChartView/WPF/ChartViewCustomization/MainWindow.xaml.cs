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
using Telerik.Windows.Controls.ChartView;

namespace ChartViewCustomization
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private const int months = 17;
        Random r = new Random();
        List<string> competitorsNames = new List<string> { "Me", "PeopleMyAge", "Community" };
        List<DetailedInfo> pieData = new List<DetailedInfo>();
        List<DetailedInfo> barData = new List<DetailedInfo>();
        List<DetailedInfo> paceSplineAreaData = new List<DetailedInfo>();
        List<DetailedInfo> elevationSplineAreaData = new List<DetailedInfo>();

        public MainWindow()
        {
            InitializeComponent();
            PopulateBarSeriesWithData();
            PopulatePieWithData();
            PopulateAreaWithData();
        }

        private void PopulateAreaWithData()
        {
            int previousPaceValue = 60;
            int previousElevationValue = 10;
            for (int i = 0; i < 100; i++)
            {
                paceSplineAreaData.Add(new DetailedInfo() { Pace = r.Next(previousPaceValue - 2, previousPaceValue + 2) });
                previousPaceValue = r.Next(previousPaceValue - 2, previousPaceValue + 2);

                elevationSplineAreaData.Add(new DetailedInfo() { Elevation = r.Next(previousElevationValue - 2, previousElevationValue + 2) });
            }
            elevationArea.ItemsSource = elevationSplineAreaData;
            paceArea.ItemsSource = paceSplineAreaData;
        }

        private void PopulatePieWithData()
        {
            string[] pieSliceMainColors = new string[] { "#FFB3FF00", "#FF1669B9", "#FFFF0000" };
            string[] pieSliceSecondColors = new string[] { "#FF415806", "#FF013A71", "#FF000000" };

            for (int i = 0; i < 3; i++)
            {
                pieData.Add(new DetailedInfo() { CompetitorName = competitorsNames[i], TotalDistance = r.Next(1, 40), MainColor = pieSliceMainColors[i], SecondColor = pieSliceSecondColors[i] });
            }
            pie.ItemsSource = pieData;
        }

        private void PopulateBarSeriesWithData()
        {
            for (int i = 0; i < months; i++)
            {
                barData.Add(new DetailedInfo() { Date = new DateTime(2013, 1, 1).AddMonths(i), DistanceRun = r.Next(5, 50) });
            }

            barSeries.ItemsSource = barData;
        }
    }

    public class DetailedInfo
    {

        public double DistanceRun
        {
            get;
            set;
        }

        public DateTime Date
        {
            get;
            set;
        }

        public double Elevation
        {
            get;
            set;
        }

        public double Pace
        {
            get;
            set;
        }

        public double TotalDistance
        {
            get;
            set;
        }

        public string CompetitorName
        {
            get;
            set;
        }

        public string MainColor
        {
            get;
            set;
        }

        public string SecondColor
        {
            get;
            set;
        }
    }
}
