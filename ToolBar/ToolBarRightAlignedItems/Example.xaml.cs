using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using Telerik.Windows.Controls.ToolBar;

namespace ToolbarRightAlignedItems
{
    /// <summary>
    /// Interaction logic for Example.xaml
    /// </summary>
    public partial class Example : UserControl
    {
        public Example()
        {
            InitializeComponent();

            this.itemAlignmentCombo.ItemsSource = Enum.GetValues(typeof(ItemAlignment));
            this.overflowModeCombo.ItemsSource = Enum.GetValues(typeof(Telerik.Windows.Controls.OverflowMode));
        }

        private void OnOrientationChanged()
        {
            Orientation orientaion = this.toolbar.Orientation;
            bool isHorizontal = orientaion == Orientation.Horizontal;

            toolbar.Orientation = orientaion;
            toolbar.VerticalAlignment = isHorizontal ? VerticalAlignment.Top : VerticalAlignment.Stretch;
            toolbar.Width = isHorizontal ? double.NaN : 75;
            int column = isHorizontal ? 0 : 1;
            int columnSpan = isHorizontal ? 2 : 1;
            int rowSpan = isHorizontal ? 1 : 2;

            Grid.SetColumn(this.toolbar, column);
            Grid.SetColumnSpan(this.toolbar, columnSpan);
            Grid.SetRowSpan(this.toolbar, rowSpan);
        }

        private void RadButton_Click(object sender, RoutedEventArgs e)
        {
            bool isHorizontal = this.toolbar.Orientation == Orientation.Horizontal;
            this.toolbar.Orientation = isHorizontal ? Orientation.Vertical : Orientation.Horizontal;
            this.OnOrientationChanged();
        }
    }
}
