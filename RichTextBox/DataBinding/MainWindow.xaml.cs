using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using Telerik.Windows.Controls;

namespace DataBinding
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            this.DataContext = new ExampleDataContext();

            InitializeComponent();

        }

        private void showXamlBtn_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            RadWindow window = new RadWindow() { Width = 500, Height = 400, WindowStartupLocation = WindowStartupLocation.CenterOwner };

            TextBox textBox = new TextBox() { IsReadOnly = true, VerticalScrollBarVisibility = ScrollBarVisibility.Auto, HorizontalScrollBarVisibility = ScrollBarVisibility.Auto };
            textBox.Text = (this.DataContext as ExampleDataContext).XamlData;
            window.Content = textBox;

            window.ShowDialog();
        }
    }
}
