using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace CustomCommandDescriptor
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private CustomCommandDescriptors customCommandDescriptors;
        public MainWindow()
        {
            InitializeComponent();
            this.customCommandDescriptors = new CustomCommandDescriptors(this.viewer);
            this.viewer.CommandDescriptors = this.customCommandDescriptors;
        }

        private void RadButton_Click(object sender, RoutedEventArgs e)
        {
            bool newState = !this.customCommandDescriptors.FitToWidthCommandDescriptor.IsEnabled;
            this.customCommandDescriptors.FitToWidthCommandDescriptor.IsEnabled = newState;

            if (newState)
            {
                this.ChangeStateButton.Content = "Disable";
            }
            else
            {
                this.ChangeStateButton.Content = "Enable";
            }
        }
    }
}
