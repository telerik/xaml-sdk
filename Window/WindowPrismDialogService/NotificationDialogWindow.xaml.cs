
using Prism.Events;
using Prism.Services.Dialogs;
using System;
using System.ComponentModel;
using System.IO;
using System.IO.IsolatedStorage;
using System.Windows;
using System.Windows.Threading;
using Telerik.Windows.Controls;

namespace WindowPrismDialogService
{
    public partial class NotificationDialogWindow : RadWindow, IDialogWindow
    {
        private CancelEventHandler closing;
        private EventHandler closed;

        public IDialogResult Result { get; set; }

        Window IDialogWindow.Owner { get; set; }

        public NotificationDialogWindow()
        {
            this.InitializeComponent();
            this.Closed += (s, e) => this.closed(this, e);
        }

        event CancelEventHandler IDialogWindow.Closing
        {
            add { closing += value; }
            remove { closing -= value; }
        }

        event EventHandler IDialogWindow.Closed
        {
            add { closed += value; }
            remove { closed -= value; }
        }

    }
}