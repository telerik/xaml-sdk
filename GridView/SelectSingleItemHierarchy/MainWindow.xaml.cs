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
        }

        bool changingSelection = false;

        private void clubsGrid_SelectionChanged(object sender, SelectionChangeEventArgs e)
        {
            if (changingSelection)
                return;
            changingSelection = true;
            var dataControl = e.OriginalSource as GridViewDataControl;
            var selectedItem = dataControl.SelectedItem;

            this.UnSelect();
            dataControl.SelectedItems.Add(selectedItem);
            changingSelection = false;
        }


        private void UnSelect()
        {
            this.clubsGrid.UnselectAll();

            foreach (GridViewRow row in this.clubsGrid.ChildrenOfType<GridViewRow>())
            {
                if (row != null && row.IsExpanded == true)
                {
                    RadGridView childGrid = row.ChildrenOfType<RadGridView>().FirstOrDefault();
                    childGrid.UnselectAll();
                }
            }
        }
    }
}
