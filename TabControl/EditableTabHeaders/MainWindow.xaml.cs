using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace EditableTabHeaders
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            this.radTabControl.ItemsSource = Enumerable.Range(1, 5).Select(num =>
            new TabItemModel()
            {
                Name = String.Format("Header {0}", num),
                Content = String.Format("Content {0}", num)
            });
        }
    }
}
