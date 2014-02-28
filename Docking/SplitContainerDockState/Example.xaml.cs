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
using Telerik.Windows.Controls;

namespace SplitContainerDockState
{
    /// <summary>
    /// Interaction logic for Example.xaml
    /// </summary>
    public partial class Example : UserControl
    {
        public Example()
        {
            InitializeComponent();
        }

        private void ShowPane1DockStateClick(object sender, RoutedEventArgs e)
        {
            this.GetCorrectDockState(this.pane1);
        }

        private void ShowPane2DockStateClick(object sender, RoutedEventArgs e)
        {
            this.GetCorrectDockState(this.pane2);
        }

        private void GetCorrectDockState(Telerik.Windows.Controls.RadPane radPane)
        {
            var splitContainer = radPane.ParentOfType<RadSplitContainer>();

            if (splitContainer != null)
            {
                var dockState = RadDocking.GetDockState(splitContainer);

                if (radPane.IsInDocumentHost)
                {
                    MessageBox.Show(string.Format("The '{0}' is in the DocumentHost", radPane.Header));
                }
                else
                {
                    MessageBox.Show(string.Format("The DockState of '{0}' is '{1}'", radPane.Header, dockState.ToString()));
                }
            }
        }
    }
}
