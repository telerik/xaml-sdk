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
            this.LayoutUpdated += new EventHandler(MainWindow_LayoutUpdated);
        }

        void MainWindow_LayoutUpdated(object sender, EventArgs e)
        {
            this.LayoutUpdated -= new EventHandler(MainWindow_LayoutUpdated);

            ScrollBar verticalScrollBar1 = this.clubsGrid1.ChildrenOfType<ScrollBar>().Where(b => b.Name == "PART_VerticalScrollBar").FirstOrDefault();
            ScrollBar horizontalScrollBar1 = this.clubsGrid1.ChildrenOfType<ScrollBar>().Where(b => b.Name == "PART_HorizontalScrollBar").FirstOrDefault();
            ScrollBar verticalScrollBar2 = this.clubsGrid2.ChildrenOfType<ScrollBar>().Where(b => b.Name == "PART_VerticalScrollBar").FirstOrDefault();
            ScrollBar horizontalScrollBar2 = this.clubsGrid2.ChildrenOfType<ScrollBar>().Where(b => b.Name == "PART_HorizontalScrollBar").FirstOrDefault();

            verticalScrollBar1.ValueChanged += new RoutedPropertyChangedEventHandler<double>(verticalScrollBar1_ValueChanged);
            horizontalScrollBar1.ValueChanged += new RoutedPropertyChangedEventHandler<double>(horizontalScrollBar1_ValueChanged);
            verticalScrollBar2.ValueChanged += new RoutedPropertyChangedEventHandler<double>(verticalScrollBar2_ValueChanged);
            horizontalScrollBar2.ValueChanged += new RoutedPropertyChangedEventHandler<double>(horizontalScrollBar2_ValueChanged);
        }

        void horizontalScrollBar1_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            (this.clubsGrid2.ChildrenOfType<GridViewScrollViewer>().FirstOrDefault()).ScrollToHorizontalOffset(e.NewValue);
        }

        void verticalScrollBar1_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            (this.clubsGrid2.ChildrenOfType<GridViewScrollViewer>().FirstOrDefault()).ScrollToVerticalOffset(e.NewValue);
        }

        void horizontalScrollBar2_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            (this.clubsGrid1.ChildrenOfType<GridViewScrollViewer>().FirstOrDefault()).ScrollToHorizontalOffset(e.NewValue);
        }

        void verticalScrollBar2_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            (this.clubsGrid1.ChildrenOfType<GridViewScrollViewer>().FirstOrDefault()).ScrollToVerticalOffset(e.NewValue);
        }
    }
}
