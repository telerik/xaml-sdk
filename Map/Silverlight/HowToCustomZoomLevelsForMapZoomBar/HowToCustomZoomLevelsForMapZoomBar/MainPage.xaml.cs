using System;
using System.Windows;
using System.Windows.Controls;

namespace HowToCustomZoomLevelsForMapZoomBar
{
    public partial class MainPage : UserControl
    {
        private const string ImagePathFormat = "/HowToCustomZoomLevelsForMapZoomBar;component/Images/{0}.png";

        public MainPage()
        {
            InitializeComponent();

            this.RadMap1.InitializeCompleted += new EventHandler(RadMap1_InitializeCompleted);
        }

        private void RadMap1_InitializeCompleted(object sender, EventArgs e)
        {
            // remove the default zoom levels
            this.RadMap1.MapZoomBar.Commands.Clear();

            this.AddCustomZoomAction(15, "Downtown");
            this.AddCustomZoomAction(18, "Neighborhood");
            this.AddCustomZoomAction(20, "Block");
        }

        private void AddCustomZoomAction(int zoomLevel, string label)
        {
            string imagePath = string.Format(ImagePathFormat, label);
            this.RadMap1.MapZoomBar.RegisterSetZoomLevelCommand(zoomLevel,
                label,
                this.LayoutRoot.Resources["CustomCommandDataTemplate"] as DataTemplate,
                new Uri(imagePath, UriKind.RelativeOrAbsolute));
        }
    }
}
