using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Resources;
using Telerik.Windows.Controls.Map;

namespace KMLManualReading
{
    public partial class MainPage : UserControl
    {
        public MainPage()
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
