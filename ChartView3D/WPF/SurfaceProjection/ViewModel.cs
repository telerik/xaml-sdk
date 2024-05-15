using System;
using System.Collections.ObjectModel;
using Telerik.Windows.Controls;

namespace SurfaceProjection
{
    public class ViewModel : ViewModelBase
    {
        private ObservableCollection<PlotInfo> points;

        public ObservableCollection<PlotInfo> Points
        {
            get
            {
                if(this.points == null)
                {
                    this.points = this.GenerateDataViaFunction();
                }

                return points;
            }
        }

        private ObservableCollection<PlotInfo> GenerateDataViaFunction()
        {
            // This function is used to generate the data for the surface
            // f(x,y) = z = sin(x * 2 * pi / maxX) * cos(y * 2 * pi / maxY) * 200
            // where    x e [0; maxX]
            // and      y e [0; maxY]

            ObservableCollection<PlotInfo> data = new ObservableCollection<PlotInfo>();
            double maxX = 30;
            double maxY = 30;

            for (int x = 0; x < maxX; x++)
            {
                for (int y = 0; y < maxY; y++)
                {
                    double xValue = Math.Sin(x * Math.PI / (0.50 * maxX));
                    double yValue = Math.Cos(y * Math.PI / (0.50 * maxY));
                    double z = 200 * xValue * yValue;
                    PlotInfo pi = new PlotInfo
                    {
                        XValue = x.ToString(),
                        YValue = y.ToString(),
                        ZValue = z,
                        ConstValue = 500
                    };
                    data.Add(pi);
                }
            }

            return data;
        }
    }
}
