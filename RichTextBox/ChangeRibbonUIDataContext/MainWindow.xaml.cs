using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using Telerik.Windows.Controls;

namespace ChangeRibbonUIDataContext
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void radRichTextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            this.radRichTextBoxRibbonUI.DataContext = ((RadRichTextBox)sender).Commands;
        }
    }
}
