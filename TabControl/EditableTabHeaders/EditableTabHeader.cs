using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace EditableTabHeaders
{
    [TemplatePart(Name = "PART_EditArea", Type = typeof(TextBox))]
    public class EditableTabHeader_WPF : ContentControl
    {
        static EditableTabHeader_WPF()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(EditableTabHeader_WPF), new FrameworkPropertyMetadata(typeof(EditableTabHeader_WPF)));
        }
        private TextBox textBox;
        public static DependencyProperty IsInEditModeProperty =
         DependencyProperty.Register("IsInEditMode", typeof(Boolean), typeof(EditableTabHeader_WPF));
        public bool IsInEditMode
        {
            get
            {
                return (bool)this.GetValue(IsInEditModeProperty);
            }
            set
            {
                this.SetValue(IsInEditModeProperty, value);
            }
        }
        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            this.textBox = this.Template.FindName("PART_EditArea", this) as TextBox;
            this.textBox.LostFocus += new RoutedEventHandler(textBox_LostFocus);
            this.MouseDoubleClick += new MouseButtonEventHandler(EditableTabHeaderControl_MouseDoubleClick);
        }
        private void textBox_LostFocus(object sender, RoutedEventArgs e)
        {
            this.IsInEditMode = false;
        }
        private void EditableTabHeaderControl_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                e.Handled = true;
                this.IsInEditMode = true;
                this.textBox.Focus();
            }
        }
    }
}
