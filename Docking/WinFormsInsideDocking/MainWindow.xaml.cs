using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Telerik.Windows.Controls.Animation;

namespace WinFormsInsideDocking_WPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
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
    }
}
