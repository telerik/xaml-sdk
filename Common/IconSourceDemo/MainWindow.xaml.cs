using System;
using System.Linq;
using System.Windows;
using Telerik.Windows.Controls;

namespace IconSourceDemo
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

        private bool isDark;
        private void switchThemeBtn_Click(object sender, RoutedEventArgs e)
        {
            if (isDark)
            {
                IconSources.ChangeIconsSet(IconsSet.Light);
                this.SwitchTheme("Office2013");
            }
            else
            {
                IconSources.ChangeIconsSet(IconsSet.Dark);
                this.SwitchTheme("Expression_Dark");
            }

            this.isDark = !this.isDark;
        }

        private void SwitchTheme(string theme)
        {
            Application.Current.Resources.MergedDictionaries.Clear();
            Application.Current.Resources.MergedDictionaries.Add(new ResourceDictionary()
            {
                Source = new Uri("/Telerik.Windows.Themes." + theme + ";component/Themes/System.Windows.xaml", UriKind.RelativeOrAbsolute)
            });

            Application.Current.Resources.MergedDictionaries.Add(new ResourceDictionary()
            {
                Source = new Uri("/Telerik.Windows.Themes." + theme + ";component/Themes/Telerik.Windows.Controls.xaml", UriKind.RelativeOrAbsolute)
            });
        }
    }
}
