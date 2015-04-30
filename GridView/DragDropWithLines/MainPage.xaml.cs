using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using Telerik.Windows.Controls;

namespace DragDropWithLines
{
    public partial class MainPage : UserControl
    {
        private bool isEnabled = true;
        public MainPage()
        {
            this.InitializeComponent();
            this.radGridView.ItemsSource = MessageViewModel.Generate();
            RowReorderBehavior.SetIsEnabled(this.radGridView, isEnabled);
        }

        private void ButtonBase_OnClick(object sender, RoutedEventArgs e)
        {
            isEnabled = !isEnabled;
            RowReorderBehavior.SetIsEnabled(this.radGridView, isEnabled);
        }
    }
}
