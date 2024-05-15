using System;
using System.Linq;
using System.Windows.Controls;
using CloseFilteringPopupFilterButton;

namespace SilverlightApplication1
{
    public partial class MainPage : UserControl
    {
        public MainPage()
        {
            InitializeComponent();

            this.clubsGrid.Columns["Name"].FilteringControl = new MyFilteringControl(this.clubsGrid.Columns["Name"]);
        }
    }
}