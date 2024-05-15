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