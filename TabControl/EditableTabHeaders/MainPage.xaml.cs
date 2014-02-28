using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;

namespace EditableTabHeaders
{
    public partial class MainPage : UserControl
    {
        public MainPage()
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
