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
    public class NodeStyleSelector : StyleSelector
    {
        public Style DecisionNodeStyle { get; set; }
        public Style StartNodeStyle { get; set; }
        public Style EndNodeStyle { get; set; }
        public Style RectangleNodeStyle { get; set; }

        public override Style SelectStyle(object item, DependencyObject container)
        {
            if (item is DecisionNode)
                return DecisionNodeStyle;
            else if (item is RectangleNode)
                return RectangleNodeStyle;
            else if (item is EllipseNode)
            {
                switch (((EllipseNode)item).Type)
                {
                    case EllipseNodeType.Start:
                        return StartNodeStyle;
                    case EllipseNodeType.End:
                        return EndNodeStyle;
                    default:
                        return base.SelectStyle(item, container);
                }
            }
            else return base.SelectStyle(item, container);
        }
    }
}
