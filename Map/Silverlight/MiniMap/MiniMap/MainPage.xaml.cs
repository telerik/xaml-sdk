using System.Windows;
using System.Windows.Controls;

namespace MiniMap
{
    public partial class MainPage : UserControl
    {
        private const double MiniMapWidthScaleFactor = 0.15;
        private const double MiniMapHeightScaleFactor = 0.2;

        public MainPage()
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
