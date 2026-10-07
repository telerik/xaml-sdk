using System.Windows;

namespace MiniMap
{
    public partial class MainWindow : Window
    {
        private const double MiniMapWidthScaleFactor = 0.15;
        private const double MiniMapHeightScaleFactor = 0.2;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void MapSizeChanged(object sender, SizeChangedEventArgs e)
        {
            this.MiniMap1.Width = e.NewSize.Width * MiniMapWidthScaleFactor;
            this.MiniMap1.Height = e.NewSize.Height * MiniMapHeightScaleFactor;
        }
    }
}
