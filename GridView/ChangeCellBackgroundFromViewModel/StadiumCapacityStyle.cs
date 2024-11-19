using System.Windows;
using Telerik.Windows.Controls;
using Telerik.Windows.Controls.GridView;

namespace ChangeCellBackgroundFromViewModel
{
    public class StadiumCapacityStyle : System.Windows.Controls.StyleSelector
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
