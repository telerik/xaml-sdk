using System;
using System.Windows;
using System.Windows.Controls;

namespace HierarchyExpandButtonStyleSelector
{
#if !SILVERLIGHT
    public class MyHierarchyExpandButtonStyleSelector : System.Windows.Controls.StyleSelector
#else
	public class MyHierarchyExpandButtonStyleSelector : Telerik.Windows.Controls.StyleSelector
#endif
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
