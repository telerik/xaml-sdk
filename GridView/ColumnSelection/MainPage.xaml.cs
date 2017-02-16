using System;
using System.Linq;
using System.Windows.Controls;
using Telerik.Windows.Controls;
using System.Windows.Controls.Primitives;
using Telerik.Windows.Controls.GridView;
using System.Windows;
using System.Windows.Input;

namespace SilverlightApplication1
{
    public partial class MainPage : UserControl
    {
        public MainPage()
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