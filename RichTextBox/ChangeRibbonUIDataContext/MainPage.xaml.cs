using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using Telerik.Windows.Controls;

namespace ChangeRibbonUIDataContext
{
    public partial class MainPage : UserControl
    {
        public MainPage()
        {
            InitializeComponent();
        }

        private void radRichTextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            this.radRichTextBoxRibbonUI.DataContext = ((RadRichTextBox)sender).Commands;
        }

    }
}
