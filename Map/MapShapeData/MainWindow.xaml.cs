using System.Windows;
using Telerik.Windows.Controls.Map;

namespace MapShapeData
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            OpenStreetMapProvider openStreetMap = new OpenStreetMapProvider()
            {
                // This user agent should be set per application.
                // Please specify different string in your application.
                StandardModeUserAgent = "Telerik UI for WPF SDK samples"
            };
            this.radMap.Provider = openStreetMap;
        }

        private void EllipseDataToggle_Checked(object sender, RoutedEventArgs e)
        {
            this.VisualizationLayer.Items.Add(this.Resources["EllipseData"]);
        }

        private void EllipseDataToggle_Unchecked(object sender, RoutedEventArgs e)
        {
            this.VisualizationLayer.Items.Remove(this.Resources["EllipseData"]);
        }

        private void LineDataToggle_Checked(object sender, RoutedEventArgs e)
        {
            this.VisualizationLayer.Items.Add(this.Resources["LineData"]);
        }

        private void LineDataToggle_Unchecked(object sender, RoutedEventArgs e)
        {
            this.VisualizationLayer.Items.Remove(this.Resources["LineData"]);
        }

        private void PathDataToggle_Checked(object sender, RoutedEventArgs e)
        {

            this.VisualizationLayer.Items.Add(this.Resources["PathData"]);
        }

        private void PathDataToggle_Unchecked(object sender, RoutedEventArgs e)
        {
            this.VisualizationLayer.Items.Remove(this.Resources["PathData"]);
        }

        private void CombinedGeometryDataToggle_Checked(object sender, RoutedEventArgs e)
        {
            this.VisualizationLayer.Items.Add(this.Resources["CombinedGeometryData"]);
        }

        private void CombinedGeometryDataToggle_Unchecked(object sender, RoutedEventArgs e)
        {
            this.VisualizationLayer.Items.Remove(this.Resources["CombinedGeometryData"]);
        }

        private void PolygonDataToggle_Checked(object sender, RoutedEventArgs e)
        {
            this.VisualizationLayer.Items.Add(this.Resources["PolygonData"]);
        }

        private void PolygonDataToggle_Unchecked(object sender, RoutedEventArgs e)
        {
            this.VisualizationLayer.Items.Remove(this.Resources["PolygonData"]);
        }

        private void PolylineDataToggle_Checked(object sender, RoutedEventArgs e)
        {
            this.VisualizationLayer.Items.Add(this.Resources["PolylineData"]);
        }

        private void PolylineDataToggle_Unchecked(object sender, RoutedEventArgs e)
        {
            this.VisualizationLayer.Items.Remove(this.Resources["PolylineData"]);
        }

        private void RectangleDataToggle_Checked(object sender, RoutedEventArgs e)
        {
            this.VisualizationLayer.Items.Add(this.Resources["RectangleData"]);
        }

        private void RectangleDataToggle_Unchecked(object sender, RoutedEventArgs e)
        {
            this.VisualizationLayer.Items.Remove(this.Resources["RectangleData"]);
        }
    }
}
