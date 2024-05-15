using System;
using System.Linq;
using System.Windows.Controls;

namespace ClosableTabs
{
    public partial class MainPage : UserControl
    {
        public MainPage()
        {
            InitializeComponent();
            this.DataContext = new MainViewModel();
        }
    }
}