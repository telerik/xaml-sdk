using System;
using System.Linq;
using System.Windows.Controls;
using CreateCustomDateTimePickerColumn;

namespace CreateCustomDateTimePickerColumn
{
    public partial class MainPage : UserControl
    {
        public MainPage()
        {
            InitializeComponent();
			this.radGridView.ItemsSource = Club.GetClubs();
        }
    }
}