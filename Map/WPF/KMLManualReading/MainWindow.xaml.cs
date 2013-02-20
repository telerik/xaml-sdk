using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Resources;
using Telerik.Windows.Controls.Map;

namespace KMLManualReading
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            this.LoadKMLData();
        }

        private void LoadKMLData()
        {
            StreamResourceInfo streamResource = Application.GetResourceStream(new Uri("/KMLManualReading;component/Resources/bulgaria.kml", UriKind.RelativeOrAbsolute));
            List<FrameworkElement> elements = KmlReader.Read(streamResource.Stream);
            foreach (FrameworkElement element in elements)
            {
                this.informationLayer.Items.Add(element);
            }
        }
    }
}
