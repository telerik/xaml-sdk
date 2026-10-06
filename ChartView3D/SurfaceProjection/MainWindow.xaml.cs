using System;
using System.Windows;

namespace SurfaceProjection
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            this.DataContext = new ViewModel();

            this.colorizer.OnColorizingCompleted += ColorizerOnColorizingCompleted;
        }

        private void ColorizerOnColorizingCompleted(object sender, EventArgs e)
        {
            ProjectionColorizer customColorizer = new ProjectionColorizer();
            customColorizer.CustomMaterial = this.colorizer.Material;
            customColorizer.TextureCoordinates = this.colorizer.TextureCoordinates;
            this.constlevelseries.Colorizer = customColorizer;
        }
    }
}
