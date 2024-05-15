using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace EditableTabHeaders
{
    [TemplateVisualState(GroupName = "EditStates", Name = "EditMode")]
    [TemplateVisualState(GroupName = "EditStates", Name = "ViewMode")]
    public class EditableTabHeader : ContentControl
    {
        private TextBox textBox;
        private DateTime previosLeftClickTime = DateTime.Now;
        private Point previosLeftClickPoint;
        private TimeSpan doubleClickSpan = TimeSpan.FromSeconds(0.4);
        public static readonly DependencyProperty IsInEditModeProperty = DependencyProperty.Register(
            "IsInEditMode",
            typeof(bool),
            typeof(EditableTabHeader),
            new PropertyMetadata(OnIsInEditModeChanged));

        public EditableTabHeader()
        {
            DefaultStyleKey = typeof(EditableTabHeader);
        }

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            this.textBox = this.GetTemplateChild("TextBox") as TextBox;
            this.textBox.LostFocus += new RoutedEventHandler(textBox_LostFocus);
        }

        private void textBox_LostFocus(object sender, RoutedEventArgs e)
        {
            this.IsInEditMode = false;
        }

        protected override void OnMouseLeftButtonDown(MouseButtonEventArgs e)
        {
            base.OnMouseLeftButtonDown(e);
            var currentTime = DateTime.Now;
            var currentPoint = e.GetPosition(this);
            var durationBetweenClicks = currentTime - previosLeftClickTime;
            if (currentPoint == previosLeftClickPoint && durationBetweenClicks < this.doubleClickSpan)
            {
                e.Handled = true;
                this.IsInEditMode = !this.IsInEditMode;
                this.textBox.Focus();
            }
            this.previosLeftClickTime = DateTime.Now;
            this.previosLeftClickPoint = e.GetPosition(this);
        }

        public bool IsInEditMode
        {
            get { return (bool)GetValue(IsInEditModeProperty); }
            set { SetValue(IsInEditModeProperty, value); }
        }

        private static void OnIsInEditModeChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            var editableContentControl = sender as EditableTabHeader;
            var newValue = (bool)e.NewValue;
            if (!newValue)
            {
                editableContentControl.Content = editableContentControl.textBox.Text;
            }
            editableContentControl.ChangeVisualStates();
        }

        public void ChangeVisualStates()
        {
            if (this.IsInEditMode)
            {
                VisualStateManager.GoToState(this, "EditMode", true);
            }
            else
            {
                VisualStateManager.GoToState(this, "ViewMode", true);
            }
        }
    }
}