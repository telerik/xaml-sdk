using System.Globalization;
using System.Linq;
using System.Windows.Controls;
using System.Windows.Media;
using Telerik.Windows.Controls.Map;
using Telerik.Windows.Input;

namespace HowToDisplayInfoFromCustomShapefile
{
    public partial class MainPage : UserControl
    {
        public MainPage()
        {
            InitializeComponent();
        }

        private void MapShapeReader_ReadCompleted(object sender, ReadShapesCompletedEventArgs eventArgs)
        {
            InformationLayer layer = (sender as MapShapeReader).Layer;

            // extract the seat colorization information from the data attributes
            foreach (MapShape shape in layer.Items)
            {
                byte red = byte.Parse(shape.ExtendedData.GetValue("RGB0").ToString(), CultureInfo.InvariantCulture);
                byte green = byte.Parse(shape.ExtendedData.GetValue("RGB1").ToString(), CultureInfo.InvariantCulture);
                byte blue = byte.Parse(shape.ExtendedData.GetValue("RGB2").ToString(), CultureInfo.InvariantCulture);

                shape.Fill = new SolidColorBrush(Color.FromArgb(255, red, green, blue));
                shape.MouseLeftButtonDown += Shape_MouseLeftButtonDown;
            }

            // make sure the map zoom / center settings ensure best view of the loaded shapes
            this.SetBestView(layer);
        }

        private void SetBestView(InformationLayer layer)
        {
            LocationRect bestView = layer.GetBestView(layer.Items.Cast<object>());
            RadMap1.SetView(bestView);
        }

        private void Shape_MouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            MapShape shape = sender as MapShape;
            string sector = shape.ExtendedData.GetValue("LAYER").ToString();

            this.StartTransitionAnimation();
            (this.Resources["DataContext"] as ExampleViewModel).UpdateViewModel(sector);
        }

        private void StartTransitionAnimation()
        {
            if (this.TransitionWrapper != null)
            {
                this.TransitionWrapper.PrepareAnimation();
                this.TransitionWrapper.StartAnimation();
            }
        }
    }
}
