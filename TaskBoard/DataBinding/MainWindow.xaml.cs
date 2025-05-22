using System.Windows;
using Telerik.Windows.Controls;

namespace DataBinding;

public partial class MainWindow : Window
{
    public MainWindow()
    {
        StyleManager.ApplicationTheme = new Windows11Theme();
        InitializeComponent();
        DataContext = new MainViewModel();
    }
}