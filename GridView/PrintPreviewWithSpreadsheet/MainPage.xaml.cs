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
using Telerik.Windows.Controls;
using Telerik.Windows.Data;

namespace PrintPreviewWithSpreadsheet
{
    public partial class MainPage : UserControl
    {
        public MainPage()
        {
            InitializeComponent();
        }
           

        private void RadButton_Click(object sender, RoutedEventArgs e)
        {
            this.clubsGrid.PrintPreview();
        }
        
    }
}
