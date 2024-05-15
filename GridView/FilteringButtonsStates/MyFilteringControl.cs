using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Telerik.Windows.Controls.GridView;
using Telerik.Windows.Controls;
using System.Windows.Controls;

namespace WpfApplication1
{
    class MyFilteringControl : FilteringControl
    {
        private Button filterButton;
        public MyFilteringControl(Telerik.Windows.Controls.GridViewColumn column)
            : base(column)
        {
            this.Loaded += MyFilteringControl_Loaded;
        }

        void MyFilteringControl_Loaded(object sender, System.Windows.RoutedEventArgs e)
        {
            var buttons = this.ChildrenOfType<Button>();
            if (buttons.Count() > 0)
            {
                filterButton = this.ChildrenOfType<Button>().First();
                if (filterButton != null)
                    filterButton.IsEnabled = false;
                this.Column.ColumnFilterDescriptor.PropertyChanged += ColumnFilterDescriptor_PropertyChanged;
            }
        }

        void ColumnFilterDescriptor_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (filterButton != null)
                filterButton.IsEnabled = true;
        }

        protected override void OnApplyFilter()
        {
            base.OnApplyFilter();
            if (filterButton != null)
                filterButton.IsEnabled = true;
        }
        protected override void OnClearFilter()
        {
            base.OnClearFilter();
            if (filterButton != null)
                filterButton.IsEnabled = false;
        }
    }
}