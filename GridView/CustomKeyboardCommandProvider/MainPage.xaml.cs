using System;
using System.Linq;
using System.Windows.Controls;
using WpfApplication1;

namespace SilverlightApplication1
{
    public partial class MainPage : UserControl
    {
        public MainPage()
        {
            InitializeComponent();

            this.clubsGrid.KeyboardCommandProvider = new KeyboardCommandProvider(this.clubsGrid);
        }
    }
}