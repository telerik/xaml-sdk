using System;
using System.Windows;
using System.Windows.Controls;
using Telerik.Windows.Controls;

namespace ChangeThemeVariationRuntime
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void RadComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var selectedItem = (RadComboBoxItem)e.AddedItems[0];
            var colorVariationName = selectedItem.Content.ToString();
            var colorVariation = (FluentPalette.ColorVariation)Enum.Parse(typeof(FluentPalette.ColorVariation), colorVariationName);
            FluentPalette.LoadPreset(colorVariation);
            OnResetTheme();
        }

        private static void OnResetTheme()
        {
            Application.Current.Resources.MergedDictionaries.Clear();
            Application.Current.Resources.MergedDictionaries.Add(new ResourceDictionary()
            {
                Source = new Uri("/Telerik.Windows.Themes.Fluent;component/Themes/System.Windows.xaml", UriKind.RelativeOrAbsolute)
            });
            Application.Current.Resources.MergedDictionaries.Add(new ResourceDictionary()
            {
                Source = new Uri("/Telerik.Windows.Themes.Fluent;component/Themes/Telerik.Windows.Controls.xaml", UriKind.RelativeOrAbsolute)
            });
            Application.Current.Resources.MergedDictionaries.Add(new ResourceDictionary()
            {
                Source = new Uri("/Telerik.Windows.Themes.Fluent;component/Themes/Telerik.Windows.Controls.Input.xaml", UriKind.RelativeOrAbsolute)
            });
        }
    }
}
