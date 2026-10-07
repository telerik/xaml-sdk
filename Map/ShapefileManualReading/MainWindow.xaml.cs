using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Resources;
using Telerik.Windows.Controls.Map;

namespace ShapefileManualReading
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            this.FillInformationLayer();
        }

        private void FillInformationLayer()
        {
            StreamResourceInfo shapeResourceInfo = Application.GetResourceStream(new Uri("/ShapefileManualReading;component/Resources/world.shp", UriKind.RelativeOrAbsolute));
            StreamResourceInfo dbfResourceInfo = Application.GetResourceStream(new Uri("/ShapefileManualReading;component/Resources/world.dbf", UriKind.RelativeOrAbsolute));
            List<FrameworkElement> shapes = ShapeFileReader.Read(shapeResourceInfo.Stream, dbfResourceInfo.Stream);
            foreach (var shape in shapes)
            {
                this.informationLayer.Items.Add(shape);
            }
        }
    }
}
