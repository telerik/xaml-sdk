using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using Telerik.Windows.Controls;
using Telerik.Windows.Controls.Data.PropertyGrid;

namespace CustomHyperlinkToolTip
{
    public static class CustomToolTipBehavior
    {
        public static bool GetCustomToolTipOnFieldLoaded(FrameworkElement frameworkElement)
        {
            return (bool)frameworkElement.GetValue(CustomToolTipBehavior.CustomToolTipProperty);
        }

        public static void SetCustomToolTipOnFieldLoaded(FrameworkElement frameworkElement, bool value)
        {
            frameworkElement.SetValue(CustomToolTipBehavior.CustomToolTipProperty, value);
        }

        public static readonly DependencyProperty CustomToolTipProperty =
            DependencyProperty.RegisterAttached(
            "CustomToolTip",
            typeof(bool),
            typeof(CustomToolTipBehavior),
            new PropertyMetadata(OnSetCustomToolTip));

        private static void OnSetCustomToolTip(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            (d as RadPropertyGrid).FieldLoaded += new EventHandler<FieldEventArgs>(OnFieldLoaded);
        }

        private static void OnFieldLoaded(object sender, FieldEventArgs e)
        {
            var type = (e.Field.DataContext as Telerik.Windows.Controls.Data.PropertyGrid.PropertyDefinition).SourceProperty.PropertyType;
            var data = InfoTipData.CreateInfoTipData(type);
            var button = e.Field.ChildrenOfType<RadRadioButton>().FirstOrDefault(x => x.Name == "helpButton");
            button.DataContext = data;
            button.MouseEnter += OnMouseEnter;
        }

        static void OnMouseEnter(object sender, System.Windows.Input.MouseEventArgs e)
        {
            var toggleButton = (sender as RadRadioButton);
            if (!toggleButton.IsChecked.Value)
            {
                toggleButton.LostFocus += OnLostFocus;
                toggleButton.IsChecked = true;
                toggleButton.Focus();
            }
        }

        static void OnLostFocus(object sender, RoutedEventArgs e)
        {
            var button = (sender as RadRadioButton);

            button.IsChecked = false;
        }
    }
}
