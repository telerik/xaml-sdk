using System;
using System.Windows;
using System.Windows.Controls;
using Telerik.Windows.Controls;

namespace RemoveDeleteButton
{
    public class CustomAppointmentStyleSelector : OrientedAppointmentItemStyleSelector
    {
        public Style CustomHorizontalStyle { get; set; }

        public Style CustomVerticalStyle { get; set; }

        public override Style SelectStyle(object item, DependencyObject container, ViewDefinitionBase activeViewDefinition)
        {
            if (activeViewDefinition.GetOrientation() == Orientation.Horizontal)
            {
                return this.CustomHorizontalStyle;
            }
            else if (activeViewDefinition.GetOrientation() == Orientation.Vertical)
            {
                return this.CustomVerticalStyle;
            }

            return base.SelectStyle(item, container, activeViewDefinition);
        }
    }
}