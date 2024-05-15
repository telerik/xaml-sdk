using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Telerik.Windows.Controls;

namespace AdjustPositionOfPanelBarItemsNoScrollBar
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private double sumAllHeights = 0.0;
        private RadPanelBarItem lastExpanded;

        public MainWindow()
        {
            InitializeComponent();
        }

        //Used to Scroll to last SelectedItem in Multiple ExpandMode (similar to AutoScrollToSelectedItem property in RadTreeView)
        private void radPanelBar_Expanded(object sender, Telerik.Windows.RadRoutedEventArgs e)
        {
            ExpandMode myName;
            if (Enum.TryParse(this.radPanelBar.ExpandMode.ToString(), out myName))
            {
                switch (myName)
                {
                    case ExpandMode.Multiple:
                        this.radPanelBar.ScrollViewer.VerticalScrollBarVisibility = ScrollBarVisibility.Auto;
                        AutoScrollToLastExpandedItem(e);
                        break;
                    case ExpandMode.Single:
                        this.radPanelBar.ScrollViewer.VerticalScrollBarVisibility = ScrollBarVisibility.Disabled;
                        return;
                }
            }
        }

        private void AutoScrollToLastExpandedItem(Telerik.Windows.RadRoutedEventArgs e)
        {
            var container = e.OriginalSource as RadPanelBarItem;
            this.lastExpanded = container;
            this.sumAllHeights = 0.0;

            if (this.radPanelBar != null && container != null)
            {
                for (int i = 0; i < container.Index; i++)
                {
                    var currContainer = this.radPanelBar.ItemContainerGenerator.ContainerFromIndex(i) as RadPanelBarItem;
                    if (currContainer != null)
                    {
                        sumAllHeights += currContainer.ActualHeight;
                    }
                }
                this.radPanelBar.ScrollViewer.ScrollToVerticalOffset(sumAllHeights);
            }
        }
    }
}
