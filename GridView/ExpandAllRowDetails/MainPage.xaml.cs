using System;
using System.Linq;
using System.Windows.Controls;
using Telerik.Windows.Controls;
using System.Windows.Controls.Primitives;
using Telerik.Windows.Controls.GridView;
using System.Windows;

namespace SilverlightApplication1
{
    public partial class MainPage : UserControl
    {
        public MainPage()
        {
            InitializeComponent();

            this.clubsGrid.RowLoaded += new EventHandler<Telerik.Windows.Controls.GridView.RowLoadedEventArgs>(clubsGrid_RowLoaded);
        }

        bool shouldExpandRowDetails = false;
        void clubsGrid_RowLoaded(object sender, Telerik.Windows.Controls.GridView.RowLoadedEventArgs e)
        {
            if (e.Row is GridViewRow)
            {
                if (shouldExpandRowDetails)
                {
                    (e.Row as GridViewRow).DetailsVisibility = Visibility.Visible;
                }
                else
                {
                    (e.Row as GridViewRow).DetailsVisibility = Visibility.Collapsed;
                }
            }
        }

        private void Button1_Click(object sender, RoutedEventArgs e)
        {
            var rows = this.clubsGrid.ChildrenOfType<GridViewRow>();
            foreach (GridViewRow row in rows)
            {
                row.DetailsVisibility = Visibility.Visible;
            }
            shouldExpandRowDetails = true;

        }

        private void Button2_Click(object sender, RoutedEventArgs e)
        {
            var rows = this.clubsGrid.ChildrenOfType<GridViewRow>();
            foreach (GridViewRow row in rows)
            {
                row.DetailsVisibility = Visibility.Collapsed;
            }
            shouldExpandRowDetails = false;
        }
    }
}