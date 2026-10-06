using System;
using System.Windows;

namespace HowToImplementCustomZoomLevelsForMapZoomBar
{
    public partial class MainWindow : Window
    {
        private const string ImagePathFormat = "/HowToImplementCustomZoomLevelsForMapZoomBar;component/Images/{0}.png";

        public MainWindow()
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
