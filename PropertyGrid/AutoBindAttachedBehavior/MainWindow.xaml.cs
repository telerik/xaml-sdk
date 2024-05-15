using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

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

            var button = new Button();
            button.Height = 50;
            button.Width = 200;
            rpg.Item = button;
        }
    }
}
