using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Controls;
using System.Windows;
using StyleSelectors.ViewModels;
using Telerik.Windows.Controls;

namespace StyleSelectors.Selectors
{
    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public class LinkStyleSelector : StyleSelector
    {
        public Style NormalLinkStyle { get; set; }
        public Style RightCapLinkStyle { get; set; }
        public Style LeftCapLinkStyle { get; set; }

        public override Style SelectStyle(object item, DependencyObject container)
        {
            Link link = item as Link;
            if (link == null)
                return base.SelectStyle(item, container);
            else switch (link.Type)
                {
                    case LinkType.RightToLeft:
                        return LeftCapLinkStyle;
                    case LinkType.LeftToRight:
                        return RightCapLinkStyle;
                    case LinkType.Normal:
                        return NormalLinkStyle;
                    default:
                        return base.SelectStyle(item, container);
                }
        }
    }
}
