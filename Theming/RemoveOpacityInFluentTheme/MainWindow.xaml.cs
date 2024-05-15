using System.Windows;
using System.Windows.Media;
using Telerik.Windows.Controls;

namespace RemoveOpacityInFluentTheme
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        public static Color RgbFromArgbAndBackgroundColor(Color targetColor, Color backgroundColor)
        {
            var baseAlpha = targetColor.A / 255.0;
            var reverseAlpha = 1.0 - baseAlpha;
            byte redValue = (byte)((targetColor.R * baseAlpha) + (backgroundColor.R * reverseAlpha));
            byte greenValue = (byte)((targetColor.G * baseAlpha) + (backgroundColor.G * reverseAlpha));
            byte blueValue = (byte)((targetColor.B * baseAlpha) + (backgroundColor.B * reverseAlpha));

            return Color.FromArgb(255, redValue, greenValue, blueValue);
        }

        private void RadButton_Click(object sender, RoutedEventArgs e)
        {
            Color mainColor = FluentPalette.Palette.MainColor;
            Color primaryColor = FluentPalette.Palette.PrimaryColor;
            Color mouseOverColor = FluentPalette.Palette.MouseOverColor;
            Color basicColor = FluentPalette.Palette.BasicColor;
            Color pressedColor = FluentPalette.Palette.PressedColor;

            FluentPalette.Palette.MainColor = RgbFromArgbAndBackgroundColor(mainColor, FluentPalette.Palette.PrimaryBackgroundColor);
            FluentPalette.Palette.PrimaryColor = RgbFromArgbAndBackgroundColor(primaryColor, FluentPalette.Palette.PrimaryBackgroundColor);
            FluentPalette.Palette.MouseOverColor = RgbFromArgbAndBackgroundColor(mouseOverColor, FluentPalette.Palette.PrimaryBackgroundColor);
            FluentPalette.Palette.BasicColor = RgbFromArgbAndBackgroundColor(basicColor, FluentPalette.Palette.PrimaryBackgroundColor);
            FluentPalette.Palette.PressedColor = RgbFromArgbAndBackgroundColor(pressedColor, FluentPalette.Palette.PrimaryBackgroundColor);
        }
    }
}
