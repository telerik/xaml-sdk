using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ErrorBars
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private static Random randomNumberGenerator = new Random();

        public MainWindow()
        {
            InitializeComponent();

            this.DataContext = GetData();
        }

        private object GetData()
        {
            var data = new ObservableCollection<PlotInfo>();
            string[] categories = new string[5] { "A", "B", "C", "D", "E" };
            foreach (var category in categories)
            {
                var value = randomNumberGenerator.Next(100, 300);
                data.Add(new PlotInfo()
                {
                    Category = category,
                    Value = value,
                    ErrorHigh = GetError(value, 1),
                    ErrorLow = GetError(value, -1),
                });
            }
            return data;
        }

        private double GetError(double value, int direction)
        {
            var offsetPercentage = randomNumberGenerator.Next(10, 50);
            var offset = value * (offsetPercentage / 100d);
            offset *= direction;

            return value + offset;
        }
    }
}
