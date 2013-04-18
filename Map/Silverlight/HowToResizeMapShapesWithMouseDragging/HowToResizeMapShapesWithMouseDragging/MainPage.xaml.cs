using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using Telerik.Windows.Controls.Map;
using Telerik.Windows.Input;

namespace HowToResizeMapShapesWithMouseDragging
{
    public partial class MainPage : UserControl
    {
        private const string ImagePath = "/HowToResizeMapShapesWithMouseDragging;component/Resources/point_small.png";

        private bool initialized;
        private bool isDragging;

        public MainPage()
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

            this.polylineLayer.Items.Add(polyline);
            this.BuildPoints(polyline);
        }

        private void BuildPoints(MapPolyline polyline)
        {
            for (int i = 0; i < polyline.Points.Count; i++)
            {
                MapPinPoint pinPoint = new MapPinPoint();
                pinPoint.ImageSource = new BitmapImage(new Uri(ImagePath, UriKind.RelativeOrAbsolute));
                MapLayer.SetLocation(pinPoint, polyline.Points[i]);
                this.pointLayer.Items.Add(pinPoint);
                this.AttachMouseEvents(pinPoint);

            }
        }

        private void AttachMouseEvents(MapPinPoint pinPoint)
        {
            pinPoint.MouseLeftButtonDown += new MouseButtonEventHandler(pinPoint_MouseLeftButtonDown);
            pinPoint.MouseLeftButtonUp += new MouseButtonEventHandler(pinPoint_MouseLeftButtonUp);
            pinPoint.MouseMove += new MouseEventHandler(pinPoint_MouseMove);
        }

        private void pinPoint_MouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            (sender as MapPinPoint).CaptureMouse();

            this.isDragging = true;
        }

        private void pinPoint_MouseLeftButtonUp(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            (sender as MapPinPoint).ReleaseMouseCapture();
            this.isDragging = false;
        }

        private void pinPoint_MouseMove(object sender, MouseEventArgs e)
        {
            if (!this.isDragging)
                return;

            var pinPoint = sender as MapPinPoint;
            var oldLocation = MapLayer.GetLocation(pinPoint);
            var location = Location.GetCoordinates(radMap, e.GetPosition(radMap));
            MapLayer.SetLocation(sender as DependencyObject, location);

            var polyline = this.polylineLayer.Items[0] as MapPolyline;
            for (int i = 0; i < polyline.Points.Count; i++)
            {
                var locationPoint = polyline.Points[i];
                if (locationPoint == oldLocation)
                {
                    polyline.Points[i] = location;
                    break;
                }
            }
        }
    }
}
