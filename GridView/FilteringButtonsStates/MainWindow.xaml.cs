using System;
using System.Linq;
using System.Windows;
using Telerik.Windows.Data;
using System.Collections;
using System.ComponentModel;
using System.Collections.Specialized;

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

            this.clubsGrid.Columns["Name"].FilteringControl = new MyFilteringControl(this.clubsGrid.Columns["Name"]);
        }
    }
}
