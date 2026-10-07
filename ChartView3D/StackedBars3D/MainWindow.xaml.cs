using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;

namespace StackedBars3D
{
    public partial class MainWindow : Window
    {
        private static Random r = new Random(0);

        public MainWindow()
        {
            InitializeComponent();
            this.DataContext = this.GetData();
        }

        private ObservableCollection<PlotInfo> GetData()
        {   
            var xCategories = new List<string>() { "Jan", "Feb", "Mar" };
            var yCategories = new List<string>() { "2017", "2018", "2019" };

            var result = new ObservableCollection<PlotInfo>();

            for (int i = 0; i < xCategories.Count; i++)
            {
                for (int k = 0; k < yCategories.Count; k++)
                {
                    var plotInfo = new PlotInfo() { StackedZValues = new List<double>() };
                    plotInfo.XCategory = xCategories[i];
                    plotInfo.YCategory = yCategories[k];

                    for (int y = 0; y < 3; y++)
                    {
                        plotInfo.StackedZValues.Add(r.Next(100, 3000));
                    }

                    result.Add(plotInfo);
                }
            }

            return result;
        }
    }
}
