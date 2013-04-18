using System;
using System.Windows;
using System.Windows.Input;
using Telerik.Windows.Controls.Map;

namespace HowToSelectMapShapes
{
    public partial class MainWindow : Window
    {
        private bool initialized;

        // selected segment and polyline
        private MapLine selectedLine;
        private MapPolyline selectedPolyline;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void radMap_InitializeCompleted(object sender, EventArgs e)
        {
            if (!initialized)
            {
                initialized = true;

                this.BuildPolyline();
            }
        }

        private void BuildPolyline()
        {
            LocationCollection points = new LocationCollection();
            points.Add(new Location(40, -100));
            points.Add(new Location(41, -101));
            points.Add(new Location(40, -102));
            points.Add(new Location(43, -103));
            points.Add(new Location(45, -97));

            MapPolyline polyline = new MapPolyline();
            polyline.Points = points;
            this.SetDefaultStyle(polyline);

            this.polylineLayer.Items.Add(polyline);
            this.BuildLines(polyline);
        }

        private void BuildLines(MapPolyline polyline)
        {
            for (int i = 0; i < polyline.Points.Count - 1; i++)
            {
                Location point1 = polyline.Points[i];
                Location point2 = polyline.Points[i + 1];
                MapLine line = new MapLine()
                {
                    Point1 = point1,
                    Point2 = point2
                };

                this.SetDefaultStyle(line);

                line.MouseLeftButtonDown += new MouseButtonEventHandler(line_MouseLeftButtonDown);
                line.Tag = polyline;

                this.lineLayer.Items.Add(line);
            }
        }

        private void line_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            MapLine line = sender as MapLine;
            if (line != null)
            {
                if (this.selectedLine != null)
                {
                    this.SetDefaultStyle(this.selectedLine);
                }

                if (this.selectedPolyline != null)
                {
                    this.SetDefaultStyle(this.selectedPolyline);
                }

                this.selectedLine = line;
                this.selectedPolyline = line.Tag as MapPolyline;

                this.SetSelectedStyle(this.selectedLine);
                this.SetSelectedStyle(this.selectedPolyline);
            }
        }

        private void SetDefaultStyle(MapShape shape)
        {
            if (shape is MapLine)
            {
                shape.Style = this.Resources["defaultLineStyle"] as Style;
            }
            else
            {
                shape.Style = this.Resources["defaultPolylineStyle"] as Style;
            }
        }

        private void SetSelectedStyle(MapShape shape)
        {
            if (shape is MapLine)
            {
                shape.Style = this.Resources["selectedLineStyle"] as Style;
            }
            else
            {
                shape.Style = this.Resources["selectedPolylineStyle"] as Style;
            }
        }
    }
}
