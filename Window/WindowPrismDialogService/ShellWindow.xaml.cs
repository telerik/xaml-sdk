
using Prism.Events;
using System;
using System.IO;
using System.IO.IsolatedStorage;
using System.Windows.Threading;
using Telerik.Windows.Controls;

namespace WindowPrismDialogService
{
    public partial class Shell : RadWindow
    {
        public Shell()
        {
            StyleManager.ApplicationTheme = new MaterialTheme();
            InitializeComponent();
        }
    }
}