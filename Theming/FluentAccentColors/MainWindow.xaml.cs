using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Telerik.Windows.Controls;

namespace FluentAccentColors
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            this.LoadPalette();
            InitializeComponent();
        }

        private void LoadPalette()
        {
            StyleManager.ApplicationTheme = new FluentTheme();

            Color accent = AccentColorSet.ActiveSet["SystemAccent"];

            ThemeVariation variation = AccentColorSet.Variation;

            if (variation == ThemeVariation.Dark)
            {
                FluentPalette.LoadPreset(FluentPalette.ColorVariation.Dark);
            }

            FluentPalette.Palette.AccentColor = accent;
            if (variation == ThemeVariation.Dark)
            {
                // Accent color is overlayed with 40% white for dark variation
                FluentPalette.Palette.AccentFocusedColor = AccentColorSet.RgbFromArgbAndBackgroundColor(Color.FromArgb(AccentColorSet.TransfromPercent(0.4), 255, 255, 255), accent);
            }
            else
            {
                // Accent color is overlayed with 20% black for dark variation
                FluentPalette.Palette.AccentFocusedColor = AccentColorSet.RgbFromArgbAndBackgroundColor(Color.FromArgb(AccentColorSet.TransfromPercent(0.2), 0, 0, 0), accent);
            }

            FluentPalette.Palette.AccentMouseOverColor = AccentColorSet.RgbFromArgbAndBackgroundColor(Color.FromArgb(AccentColorSet.TransfromPercent(0.2), 255, 255, 255), accent);
            FluentPalette.Palette.AccentPressedColor = AccentColorSet.RgbFromArgbAndBackgroundColor(Color.FromArgb(AccentColorSet.TransfromPercent(0.3), 0, 0, 0), accent);
        }
    }
}
