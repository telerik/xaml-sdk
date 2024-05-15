using System;
using System.Linq;
using System.Windows;
using Telerik.Windows.Controls;
using ComboboxColumn;

namespace WpfApplication1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            ((GridViewComboBoxColumn)this.pilotsGrid.Columns["Country"]).ItemsSource = Country.GetCountries();
        }
    }
}
