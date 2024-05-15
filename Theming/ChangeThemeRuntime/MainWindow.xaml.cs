using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using Telerik.Windows.Controls;

namespace ChangeThemeRuntime
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void RadComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            string themeName = (e.AddedItems[0] as RadComboBoxItem).Content.ToString();

            Application.Current.Resources.MergedDictionaries.Clear();
            Application.Current.Resources.MergedDictionaries.Add(new ResourceDictionary()
            {
                Source = new Uri("/Telerik.Windows.Themes." + themeName + ";component/Themes/System.Windows.xaml", UriKind.RelativeOrAbsolute)
            });
            Application.Current.Resources.MergedDictionaries.Add(new ResourceDictionary()
            {
                Source = new Uri("/Telerik.Windows.Themes." + themeName + ";component/Themes/Telerik.Windows.Controls.xaml", UriKind.RelativeOrAbsolute)
            });
            Application.Current.Resources.MergedDictionaries.Add(new ResourceDictionary()
            {
                Source = new Uri("/Telerik.Windows.Themes." + themeName + ";component/Themes/Telerik.Windows.Controls.Input.xaml", UriKind.RelativeOrAbsolute)
            });
            Application.Current.Resources.MergedDictionaries.Add(new ResourceDictionary()
            {
                Source = new Uri("/Telerik.Windows.Themes." + themeName + ";component/Themes/Telerik.Windows.Controls.GridView.xaml", UriKind.RelativeOrAbsolute)
            });
            Application.Current.Resources.MergedDictionaries.Add(new ResourceDictionary()
            {
                Source = new Uri("/Telerik.Windows.Themes." + themeName + ";component/Themes/Telerik.Windows.Controls.Data.xaml", UriKind.RelativeOrAbsolute)
            });
            Application.Current.Resources.MergedDictionaries.Add(new ResourceDictionary()
            {
                Source = new Uri("/Telerik.Windows.Themes." + themeName + ";component/Themes/Telerik.Windows.Controls.Navigation.xaml", UriKind.RelativeOrAbsolute)
            });
            Application.Current.Resources.MergedDictionaries.Add(new ResourceDictionary()
            {
                Source = new Uri("/Telerik.Windows.Themes." + themeName + ";component/Themes/Telerik.Windows.Controls.ScheduleView.xaml", UriKind.RelativeOrAbsolute)
            });
        }
    }
}
