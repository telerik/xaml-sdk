using System;
using System.Linq;
using System.Windows;
using Telerik.Windows.Data;
using System.Collections;
using System.ComponentModel;
using System.Collections.Specialized;
using Telerik.Windows.Controls.GridView;
using Telerik.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;
using System.Windows.Controls;

namespace WpfApplication1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            this.clubsGrid.AddHandler(GridViewHeaderCell.MouseLeftButtonDownEvent, new MouseButtonEventHandler(OnMouseLeftButtonDown), true);
        }

        private void OnMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            var clickedElement = e.OriginalSource as FrameworkElement;
            if (clickedElement != null)
            {
                var headerCell = clickedElement.ParentOfType<GridViewHeaderCell>();
                if (headerCell != null)
                {
                    var column = headerCell.Column;

                    if (column.IsSelected)
                    {
                        column.IsSelected = false;
                    }
                    else
                    {
                        column.IsSelected = true;
                    }
                }
            }
        }

        private void CheckBox_Checked(object sender, RoutedEventArgs e)
        {
            var headerCell = (sender as CheckBox).ParentOfType<GridViewHeaderCell>();
            if (headerCell != null)
            {
                var column = headerCell.Column;
                this.clubsGrid2.SelectCellRegion(new CellRegion(column.DisplayIndex, 0, 1, this.clubsGrid2.Items.Count));
            }
        }

        private void CheckBox_Unchecked(object sender, RoutedEventArgs e)
        {
            var headerCell = (sender as CheckBox).ParentOfType<GridViewHeaderCell>();
            if (headerCell != null)
            {
                var column = headerCell.Column;
                this.clubsGrid2.UnselectCellRegion(new CellRegion(column.DisplayIndex, 0, 1, this.clubsGrid2.Items.Count));
            }
        }
    }
}
