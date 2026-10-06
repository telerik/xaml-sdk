using System;
using System.Collections.ObjectModel;
using System.Windows;

namespace DefaultVisualMaterialSelector
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
                        
            var source = new ObservableCollection<PlotInfo>();
            string[] categories = new string[3] { "A", "B", "C" };

            for (int i = 0; i < categories.Length; i++)
            {
                for (int j = 0; j < 360; j += 36)
                {
                    double x = 200 + (100 * Math.Cos(j * (Math.PI / 180)));
                    double y = 200 + (100 * Math.Sin(j * (Math.PI / 180)));
                    var info = new PlotInfo()
                    {
                        XValue = x,
                        YValue = y,
                        ZValue = categories[i],
                    };
                    source.Add(info);
                }
            }

            this.DataContext = source;
        }
    }
}
