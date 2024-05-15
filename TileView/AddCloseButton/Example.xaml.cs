using System;
using System.Collections;
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

namespace AddCloseButton
{
    public partial class Example : UserControl
    {
        static Example()
        {
            var deleteBinding = new CommandBinding(TileViewCommandsExtension.Delete, OnDeleteCommandExecute, OnCanDeleteCommandExecute);
            CommandManager.RegisterClassCommandBinding(typeof(RadTileViewItem), deleteBinding);
        }

        private static void OnCanDeleteCommandExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }

        private static void OnDeleteCommandExecute(object sender, ExecutedRoutedEventArgs e)
        {
            var tileViewItem = sender as RadTileViewItem;
            var tileView = tileViewItem.ParentTileView as RadTileView;
            if (tileViewItem == null || tileView == null) return;

            if (tileView.ItemsSource != null)
            {
                var dataItem = tileView.ItemContainerGenerator.ItemFromContainer(tileViewItem) as DataItem;

                // Note: This will change the DataContext's Items collection.
                var source = tileView.ItemsSource as IList;
                if (dataItem != null && source != null)
                    source.Remove(dataItem);
            }
            else
            {
                tileView.Items.Remove(tileViewItem);
            }
        }

        public Example()
        {
            InitializeComponent();
            this.DataContext = new MainViewModel();
        }

        private void AddItemButton_Click(object sender, RoutedEventArgs e)
        {
            var mainViewModel = this.DataContext as MainViewModel;

            // Any of the fields is empty - animate an error message
            if (String.IsNullOrEmpty(this.headerTBox.Text) ||
                String.IsNullOrEmpty(this.contentTBox.Text))
            {
                this.errorTBlock.Opacity = 1;

                var a = new DoubleAnimation
                {
                    From = 1.0,
                    To = 0.0,
                    FillBehavior = FillBehavior.Stop,
                    BeginTime = TimeSpan.FromSeconds(2),
                    Duration = new Duration(TimeSpan.FromSeconds(0.8))
                };
                var storyboard = new Storyboard();

                storyboard.Children.Add(a);
                Storyboard.SetTarget(a, this.errorTBlock);
                Storyboard.SetTargetProperty(a, new PropertyPath(OpacityProperty));
                storyboard.Completed += delegate { this.errorTBlock.Opacity = 0; };
                storyboard.Begin();

                return;
            }

            // Otherwise add new item in the TileView
            DataItem dataItem = new DataItem()
            {
                Header = this.headerTBox.Text,
                Content = this.contentTBox.Text
            };

            mainViewModel.Items.Add(dataItem);

            this.headerTBox.Text = String.Empty;
            this.contentTBox.Text = String.Empty;
        }
    }
}
