using System;
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

namespace BindingSelectedItemsToViewModel
{
    public partial class MainPage : UserControl
    {
        private MainViewModel viewModel;

        public MainPage()
        {
            InitializeComponent();
            this.viewModel = new MainViewModel();
            this.DataContext = this.viewModel;
        }

        private void gridView_SelectionChanged(object sender, Telerik.Windows.Controls.SelectionChangeEventArgs e)
        {
            if (e.AddedItems != null)
            {
                foreach (DataItem item in e.AddedItems)
                {
                    if (!this.viewModel.SelectedCartesianDataItems.Contains(item))
                    {
                        this.viewModel.SelectedCartesianDataItems.Add(item);
                    }
                }
            }

            if (e.RemovedItems != null)
            {
                foreach (DataItem item in e.RemovedItems)
                {
                    if (this.viewModel.SelectedCartesianDataItems.Contains(item))
                    {
                        this.viewModel.SelectedCartesianDataItems.Remove(item);
                    }
                }
            }
        }
    }
}
