using System.Windows;
using System.Windows.Controls;

namespace HierarchyExpandButtonStyleSelector
{
    public class MyHierarchyExpandButtonStyleSelector : StyleSelector
	{
		public override Style SelectStyle(object item, DependencyObject container)
		{
			if (item is Club)
			{
				Club club = item as Club;
				if (club.StadiumCapacity > 50000)
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
