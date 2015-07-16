using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Data;
using Telerik.Windows.Controls;
using Telerik.Windows.Controls.GridView;

namespace CreateCustomDateTimePickerColumn
{
    public class GenericDateTimePickerColumn: GridViewBoundColumnBase
    {
        public override FrameworkElement CreateCellEditElement(GridViewCell cell, object dataItem)
        {
            #if SILVERLIGHT
            this.BindingTarget = GenericDateTimePickerSL.SelectedDateTimeProperty;
            var picker = new GenericDateTimePickerSL();
            #else
            this.BindingTarget = GenericDateTimePicker.SelectedDateTimeProperty;
            var picker = new GenericDateTimePicker();
            #endif
            picker.SetBinding(this.BindingTarget, this.CreateValueBinding());
            return picker;
        }

        private Binding CreateValueBinding()
        {
            var valueBinding = new Binding();
            valueBinding.Mode = BindingMode.TwoWay;
            valueBinding.NotifyOnValidationError = true;
            valueBinding.ValidatesOnExceptions = true;
            valueBinding.UpdateSourceTrigger = UpdateSourceTrigger.Explicit;
            valueBinding.Path = new PropertyPath(this.DataMemberBinding.Path.Path);
            return valueBinding;
        }
    }
}
