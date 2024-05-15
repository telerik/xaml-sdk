using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Telerik.Windows.Controls;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace DefiningCustomDataFields
{
    public class DataFormNumericUpDownField : DataFormDataField
    {
        protected override DependencyProperty GetControlBindingProperty()
        {
            return RadNumericUpDown.ValueProperty;
        }

        protected override Control GetControl()
        {
            DependencyProperty dependencyProperty = this.GetControlBindingProperty();
            RadNumericUpDown numericUpDown = new RadNumericUpDown();
            if (this.DataMemberBinding != null)
            {
                var binding = this.DataMemberBinding;
                numericUpDown.SetBinding(dependencyProperty, binding);
            }
            numericUpDown.SetBinding(RadNumericUpDown.IsEnabledProperty, new Binding("IsReadOnly") { Source = this, Converter = new InvertedBooleanConverter() });

            return numericUpDown;
        }
    }
}
