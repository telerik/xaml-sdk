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
using System.Windows.Navigation;

namespace CustomCommandDescriptor
{
    public partial class MainPage : UserControl
    {
        private CustomCommandDescriptors customCommandDescriptors;

        public MainPage()
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
