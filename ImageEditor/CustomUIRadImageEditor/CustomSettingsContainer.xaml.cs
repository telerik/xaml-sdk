using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using Telerik.Windows.Imaging.Tools;
using Telerik.Windows.Media.Imaging.Tools;

namespace CustomUIRadImageEditor
{
    public partial class CustomSettingsContainer : UserControl, IToolSettingsContainer
    {
        private Action applyCallback;
        private Action cancelCallback;

        public CustomSettingsContainer()
        {
            InitializeComponent();
        }

        public void Show(ITool tool, Action applyCallback, Action cancelCallback)
        {
            this.applyCallback = applyCallback;
            this.cancelCallback = cancelCallback;
            this.Visibility = Visibility.Visible;
        }
        public void Hide()
        {
            this.Visibility = Visibility.Collapsed;
        }

        private void btnApply_Click(object sender, RoutedEventArgs e)
        {
            if (this.applyCallback != null)
            {
                this.CallApplyCallback();
            }
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            if (this.cancelCallback != null)
            {
                this.CallCancelCallback();
            }
        }


        private void CallApplyCallback()
        {
            this.applyCallback();
        }

        private void CallCancelCallback()
        {
            this.cancelCallback();
        }
    }
}
