using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using Telerik.Windows.Controls;
using Telerik.Windows.Controls.GridView;

namespace ChangeCellBackgroundFromViewModel
{
#if !SILVERLIGHT
 public class StadiumCapacityStyle : System.Windows.Controls.StyleSelector
#else
     public class StadiumCapacityStyle : Telerik.Windows.Controls.StyleSelector
#endif 
    {
       
        public override Style SelectStyle(object item, DependencyObject container)
        {
            var viewModel = (container as GridViewCell).ParentOfType<RadGridView>().DataContext as MyViewModel;

            if (item is Club)
            {
                Club club = item as Club;
                if (club.StadiumCapacity > viewModel.Threshold)
                {
                    return BigStadiumStyle;
                }
                else
                {
                    return SmallStadiumStyle;
                }
            }
            return null;
        }
        public Style BigStadiumStyle { get; set; }
        public Style SmallStadiumStyle { get; set; }
    }
}
