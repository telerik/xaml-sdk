using System;
using System.Windows;
using Telerik.Windows.Controls;
using Telerik.Windows.Controls.GridView;

namespace CustomColumn
{
    public class GridViewPercentageColumn : GridViewDataColumn
    {
        public override FrameworkElement CreateCellElement(GridViewCell cell, object dataItem)
        {
            var bar = cell.Content as RadProgressBar;

            if (bar == null)
            {
                bar = new RadProgressBar();
                bar.Height = 20;
                bar.SetBinding(RadProgressBar.ValueProperty, this.DataMemberBinding);
                cell.Content = bar;
            }

            return bar;
        }

        public override FrameworkElement CreateCellEditElement(GridViewCell cell, object dataItem)
        {
            var slider = new RadSlider();
            slider.Maximum = 0;
            slider.Maximum = 100;
            slider.SmallChange = 1;
            slider.LargeChange = 10;
            slider.SetBinding(RadSlider.ValueProperty, this.DataMemberBinding);
            return slider;
        }
    } 
}
