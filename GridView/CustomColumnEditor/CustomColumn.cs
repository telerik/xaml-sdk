using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using Telerik.Windows.Controls;
using Telerik.Windows.Controls.GridView;

namespace CustomColumnEditor
{
    public class CustomColumn : GridViewBoundColumnBase
    {
        public override FrameworkElement CreateCellElement(GridViewCell cell, object dataItem)
        {
            TextBlock tb = new TextBlock();
            tb.SetBinding(TextBlock.TextProperty, new Binding(this.DataMemberBinding.Path.Path) { Converter = new MyConverter() });

            return tb;
        }

        public override FrameworkElement CreateCellEditElement(GridViewCell cell, object dataItem)
        {
            var editor = new CustomCaptainEditor();

            editor.SetBinding(CustomCaptainEditor.CaptainNameProperty,
                CreateBinding(string.Format("{0}.Name", this.DataMemberBinding.Path.Path)));
            editor.SetBinding(CustomCaptainEditor.CaptainPositionProperty,
                CreateBinding(string.Format("{0}.Position", this.DataMemberBinding.Path.Path)));

            return editor;
        }

        private Binding CreateBinding(string property)
        {
            Binding binding = new Binding(property);
            binding.Mode = BindingMode.TwoWay;
            binding.UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged;

            return binding;
        }
    }
}
