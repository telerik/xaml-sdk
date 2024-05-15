using System;
using System.Reflection;
using System.Windows;

namespace FileSystemMapProvider
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
	{
        private static string imagePattern = AssemblyDirectory + @"\..\..\OpenStreet Images\{zoom}\os_{x}_{y}_{zoom}.png";
		public MainWindow()
		{
			InitializeComponent();
            this.radMap.Provider = new FileSystemProvider(imagePattern, false, false, false);
        }

        private void RadButton_Click(object sender, RoutedEventArgs e)
        {
            this.ChangeProvider(imagePattern, true, false, false);
        }

        private void RadButton_Click_1(object sender, RoutedEventArgs e)
        {
            this.ChangeProvider(imagePattern, false, true, false);
        }

        private void RadButton_Click_2(object sender, RoutedEventArgs e)
        {
            this.ChangeProvider(imagePattern, false, false, false);
        }

        private void RadButton_Click_3(object sender, RoutedEventArgs e)
        {
            this.ChangeProvider(imagePattern, false, false, true);
        }

        private void ChangeProvider(string format, bool isgrayscale, bool isinverted, bool isCusComColors)
        {
            this.radMap.Provider = null;
            this.radMap.Providers.Clear();
            this.radMap.Provider = new FileSystemProvider(format, isgrayscale, isinverted, isCusComColors);
        }

        public static string AssemblyDirectory
        {
            get
            {
                string codeBase = Assembly.GetExecutingAssembly().CodeBase;
                UriBuilder uri = new UriBuilder(codeBase);
                string path = Uri.UnescapeDataString(uri.Path);
                return System.IO.Path.GetDirectoryName(path);
            }
        }
    }
}
