using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Telerik.Windows.Controls;
using Telerik.Windows.Controls.Animation;
using Telerik.Windows.Controls.Docking;
using Telerik.Windows.Controls.Navigation;

namespace WinFormsInsideDocking_WPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        static MainWindow()
        {
            EventManager.RegisterClassHandler(typeof(AutoHideArea), AutoHideArea.LoadedEvent, new RoutedEventHandler(OnAutoHideAreaLoaded));
        }

        private static void OnAutoHideAreaLoaded(object sender, RoutedEventArgs e)
        {
            var autoHideArea = (AutoHideArea)sender;
            var popup = autoHideArea.FindChildByType<Popup>();
            popup.AllowsTransparency = false;
        }

        public MainWindow()
        {
            InitializeComponent();
            AnimationManager.SetAnimationSelector(this.docking, null);
        }
        
        // NOTE: This is a workaroud to set properly RadDocking.ActivePane property when clicking on WinForms.
        private void WinFormsUserControl_MouseDown(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            this.docking.ActivePane = this.winFormsPane;
            (sender as WinFormsUserControl).Focus();
        }

        private void docking_ToolWindowCreated(object sender, Telerik.Windows.Controls.Docking.ElementCreatedEventArgs e)
        {
            var window = (ToolWindow)e.CreatedElement;

            RadWindowInteropHelper.SetAllowTransparency(window, false);
        }
    }
}
