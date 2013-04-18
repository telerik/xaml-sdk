using System;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Media;
using Telerik.Windows.Controls.Map;

namespace InformationLayerMapShapeExtendedData
{
    public partial class MainPage : UserControl
    {
        public MainPage()
        {
            InitializeComponent();

            this.FillInformationLayer();
        }

        private void FillInformationLayer()
        {
            // Create extended property set.
            // It can be shared between the number
            // of the map shapes.
            ExtendedPropertySet propertySet = new ExtendedPropertySet();
            propertySet.RegisterProperty("Name", "City Name", typeof(string), String.Empty);
            propertySet.RegisterProperty("Population", "Population", typeof(int), 0);

            MapEllipse sofiaEllipse = new MapEllipse()
            {
                ShapeFill = new MapShapeFill()
                {
                    Stroke = new SolidColorBrush(Colors.Red),
                    StrokeThickness = 2,
                    Fill = new SolidColorBrush(Colors.Transparent)
                },
                Width = 20,
                Height = 20,
                Location = new Location(42.6957539183824, 23.3327663758679),
            };

            // Create extended data for the ellipse
            // using existing property set.
            ExtendedData sofiaData = new ExtendedData(propertySet);

            // Set the extended properties.
            sofiaData.SetValue("Name", "Sofia");
            sofiaData.SetValue("Population", 1300000);

            // Assign extended data to the map shape.
            sofiaEllipse.ExtendedData = sofiaData;

            // Assign tooltip which uses the extended properties.
            ToolTip tooltip = new ToolTip();
            Binding tooltipBinding = new Binding()
            {
                Converter = new ExtendedDataConverter(),
                ConverterParameter = "{Name}: {Population} people",
                Source = sofiaEllipse.ExtendedData
            };
            tooltip.SetBinding(System.Windows.Controls.ToolTip.ContentProperty, tooltipBinding);
            ToolTipService.SetToolTip(sofiaEllipse, tooltip);

            this.informationLayer.Items.Add(sofiaEllipse);
        }
    }
}
