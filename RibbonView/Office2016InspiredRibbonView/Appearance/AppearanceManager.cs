using Office2016InspiredRibbonView_WPF.ViewModel;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Media;
using Telerik.Windows.Controls;
using Office2016InspiredRibbonView_WPF.Appearance;

namespace Office2016InspiredRibbonView_WPF.Appearance
{
    public static class AppearanceManager
    {
        private static readonly string StylesFolderName = "Styles";
        private static readonly string visualStudio2013Theme = "VisualStudio2013";
        private static IEnumerable<string> xamlFiles;
        private static IEnumerable<string> customStyles;

        private static IEnumerable<string> GetXamlFilesList()
        {
            var filesList = new List<string> 
            { 
                "System.Windows.xaml",
                "Telerik.Windows.Controls.xaml",
                "Telerik.Windows.Controls.Input.xaml",
                "Telerik.Windows.Controls.Navigation.xaml",
                "Telerik.Windows.Controls.RibbonView.xaml"
            };

            return filesList;
        }

        private static IEnumerable<string> GetCustomStylesList()
        {
            var customStyles = new List<string> 
            {
                "ApplicationStyles.xaml",
                "QuickAccessToolbarStyles.xaml",
                "BackstageStyles.xaml",
                "RibbonWindowStyles.xaml",
                "RibbonViewStyles.xaml",
                "RibbonButtonsStyles.xaml"
            };

            return customStyles;
        }

        static AppearanceManager()
        {
            xamlFiles = GetXamlFilesList();
            customStyles = GetCustomStylesList();
        }

        public static void ChangeAppearance(Office2016ColorVariations colorVariation)
        {
            SetThemeWithSpecificColorVariation(colorVariation);

            switch (colorVariation)
            {
                case Office2016ColorVariations.Colorful:
                    Office2016Palette.Palette.ApplicationButtonBackground = Office2016Palette.ColorfulApplicationButtonBackgroundColor;
                    Office2016Palette.Palette.BackstageBackground = Office2016Palette.ColorfulBackstageBackgroundColor;
                    Office2016Palette.Palette.MouseOverBackground = Office2016Palette.ColorfulMouseOverBackgroundColor;
                    Office2016Palette.Palette.PressedBackground = Office2016Palette.ColorfulPressedBackgroundColor;
                    Office2016Palette.Palette.TabMouseOverForeground = Office2016Palette.ColorfulTabMouseOverForegroundColor;
                    Office2016Palette.Palette.TabSelectedForeground = Office2016Palette.ColorfulTabSelectedForegroundColor;
                    Office2016Palette.Palette.TabMouseOverBackground = Office2016Palette.ColorfulTabMouseOverBackgroundColor;
                    Office2016Palette.Palette.TabMouseOverBorderBrush = Office2016Palette.ColorfulTabMouseOverBorderBrushColor;
                    Office2016Palette.Palette.MouseOverCheckedBorderBrush = Office2016Palette.ColorfulMouseOverCheckedBorderBrushColor;

                    VisualStudio2013Palette.Palette.HeaderColor = (Color)ColorConverter.ConvertFromString("#FF2A579A");
                    VisualStudio2013Palette.Palette.MouseOverColor = (Color)ColorConverter.ConvertFromString("#FFC5C5C5");
                    VisualStudio2013Palette.Palette.AccentDarkColor = (Color)ColorConverter.ConvertFromString("#FFAEAEAE");
                    VisualStudio2013Palette.Palette.PrimaryColor = (Color)ColorConverter.ConvertFromString("#FFF1F1F1");
                    VisualStudio2013Palette.Palette.SelectedColor = (Color)ColorConverter.ConvertFromString("#FFFFFFFF");
                    VisualStudio2013Palette.Palette.AccentColor = (Color)ColorConverter.ConvertFromString("#FF2A579A");
                    VisualStudio2013Palette.Palette.BasicColor = (Color)ColorConverter.ConvertFromString("#FFAEAEAE");
                    VisualStudio2013Palette.Palette.AccentMainColor = (Color)ColorConverter.ConvertFromString("#FF3E6DB6");
                    VisualStudio2013Palette.Palette.SemiBasicColor = (Color)ColorConverter.ConvertFromString("#FF19478A");
                    VisualStudio2013Palette.Palette.StrongColor = (Color)ColorConverter.ConvertFromString("#FF777777");
                    VisualStudio2013Palette.Palette.MainColor = (Color)ColorConverter.ConvertFromString("#FFFFFFFF");
                    break;
                case Office2016ColorVariations.White:
                    Office2016Palette.Palette.ApplicationButtonBackground = Office2016Palette.WhiteApplicationButtonBackgroundColor;
                    Office2016Palette.Palette.BackstageBackground = Office2016Palette.WhiteBackstageBackgroundColor;
                    Office2016Palette.Palette.MouseOverBackground = Office2016Palette.WhiteMouseOverBackgroundColor;
                    Office2016Palette.Palette.PressedBackground = Office2016Palette.WhitePressedBackgroundColor;
                    Office2016Palette.Palette.TabMouseOverForeground = Office2016Palette.WhiteTabMouseOverForegroundColor;
                    Office2016Palette.Palette.TabSelectedForeground = Office2016Palette.WhiteTabSelectedForegroundColor;
                    Office2016Palette.Palette.TabMouseOverBackground = Office2016Palette.WhiteTabMouseOverBackgroundColor;
                    Office2016Palette.Palette.TabMouseOverBorderBrush = Office2016Palette.WhiteTabMouseOverBorderBrushColor;
                    Office2016Palette.Palette.MouseOverCheckedBorderBrush = Office2016Palette.WhiteMouseOverCheckedBorderBrushColor;

                    //ribbon titlebackground color
                    VisualStudio2013Palette.Palette.HeaderColor = (Color)ColorConverter.ConvertFromString("#FFFFFFFF");
                    // mouse over color
                    VisualStudio2013Palette.Palette.MouseOverColor = (Color)ColorConverter.ConvertFromString("#FFC2D5F2");
                    // pressed color
                    VisualStudio2013Palette.Palette.AccentDarkColor = (Color)ColorConverter.ConvertFromString("#FFA3BDE3");
                    //background of RibbonView (where all groups reside)
                    VisualStudio2013Palette.Palette.PrimaryColor = (Color)ColorConverter.ConvertFromString("#FFFFFFFF");
                    //all text colors in RibbonView
                    VisualStudio2013Palette.Palette.SelectedColor = (Color)ColorConverter.ConvertFromString("#FF000000");
                    //borderbrush color
                    VisualStudio2013Palette.Palette.AccentColor = (Color)ColorConverter.ConvertFromString("#FF000000");
                    //Border around Ribbon items when checked
                    VisualStudio2013Palette.Palette.BasicColor = (Color)ColorConverter.ConvertFromString("#FFD4D4D4");
                    //Backstage Item Background when selected
                    VisualStudio2013Palette.Palette.AccentMainColor = (Color)ColorConverter.ConvertFromString("#FF3E6DB6");
                    //Backstage Item Background when hovered
                    VisualStudio2013Palette.Palette.SemiBasicColor = (Color)ColorConverter.ConvertFromString("#FF19478A");
                    //all path geometries normal fill
                    VisualStudio2013Palette.Palette.StrongColor = (Color)ColorConverter.ConvertFromString("#FF777777");
                    //background of combobox and quickstyles gallery
                    VisualStudio2013Palette.Palette.MainColor = (Color)ColorConverter.ConvertFromString("#FFFFFFFF");
                    break;
                case Office2016ColorVariations.Dark:
                    Office2016Palette.Palette.ApplicationButtonBackground = Office2016Palette.DarkApplicationButtonBackgroundColor;
                    Office2016Palette.Palette.BackstageBackground = Office2016Palette.DarkBackstageBackgroundColor;
                    Office2016Palette.Palette.MouseOverBackground = Office2016Palette.DarkMouseOverBackgroundColor;
                    Office2016Palette.Palette.PressedBackground = Office2016Palette.DarkPressedBackgroundColor;
                    Office2016Palette.Palette.TabMouseOverForeground = Office2016Palette.DarkTabMouseOverForegroundColor;
                    Office2016Palette.Palette.TabSelectedForeground = Office2016Palette.DarkTabSelectedForegroundColor;
                    Office2016Palette.Palette.TabMouseOverBackground = Office2016Palette.DarkTabMouseOverBackgroundColor;
                    Office2016Palette.Palette.TabMouseOverBorderBrush = Office2016Palette.DarkTabMouseOverBorderBrushColor;
                    Office2016Palette.Palette.MouseOverCheckedBorderBrush = Office2016Palette.DarkMouseOverCheckedBorderBrushColor;

                    VisualStudio2013Palette.Palette.HeaderColor = (Color)ColorConverter.ConvertFromString("#FF444444");
                    VisualStudio2013Palette.Palette.MouseOverColor = (Color)ColorConverter.ConvertFromString("#FF969696");
                    VisualStudio2013Palette.Palette.AccentDarkColor = (Color)ColorConverter.ConvertFromString("#FF808080");
                    VisualStudio2013Palette.Palette.PrimaryColor = (Color)ColorConverter.ConvertFromString("#FFB2B2B2");
                    VisualStudio2013Palette.Palette.SelectedColor = (Color)ColorConverter.ConvertFromString("#FFFFFFFF");
                    VisualStudio2013Palette.Palette.AccentColor = (Color)ColorConverter.ConvertFromString("#FF444444");
                    VisualStudio2013Palette.Palette.BasicColor = (Color)ColorConverter.ConvertFromString("#FF808080");
                    VisualStudio2013Palette.Palette.AccentMainColor = (Color)ColorConverter.ConvertFromString("#FF3E6DB6");
                    VisualStudio2013Palette.Palette.SemiBasicColor = (Color)ColorConverter.ConvertFromString("#FF19478A");
                    VisualStudio2013Palette.Palette.StrongColor = Colors.Black;
                    VisualStudio2013Palette.Palette.MainColor = (Color)ColorConverter.ConvertFromString("#FFD4D4D4");
                    break;
                default:
                    break;
            }

            OnColorVariationChanged(colorVariation);
        }

        private static void SetThemeWithSpecificColorVariation(Office2016ColorVariations colorVariation)
        {
            MainViewModel.SelectedColorVariation = colorVariation;

            Application.Current.Resources.MergedDictionaries.Clear();

            foreach (var file in xamlFiles)
            {
                string path = string.Format(@"/Telerik.Windows.Themes.{0};component/Themes/" + file, visualStudio2013Theme);
                Application.Current.Resources.MergedDictionaries.Add(new ResourceDictionary()
                {
                    Source = new Uri(path, UriKind.RelativeOrAbsolute)
                });
            }

            foreach (var item in customStyles)
            {
                Application.Current.Resources.MergedDictionaries.Add(new ResourceDictionary()
                {
                    Source = new Uri(string.Format(@"pack://application:,,,/Office2016InspiredRibbonView_WPF;component/{0}/{1}", AppearanceManager.StylesFolderName, item), UriKind.RelativeOrAbsolute)
                });
            }
        }

        public static event EventHandler<AppearanceChangedEventArgs> ColorVariationChanged;
        private static void OnColorVariationChanged(Office2016ColorVariations colorVariation)
        {
            if (ColorVariationChanged != null)
            {
                ColorVariationChanged(null, new AppearanceChangedEventArgs(colorVariation));
            }
        }
    }
}
