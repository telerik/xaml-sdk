using System.Windows.Controls;
using Telerik.Windows.Controls;

namespace CustomDockingPanesFactory
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

        private void OnRadDockingClose(object sender, Telerik.Windows.Controls.Docking.StateChangeEventArgs e)
        {
            // By default when a RadPane is closed via its 'X' close button it is not fully removed from the control rather its 
            // IsHidden property is set to True. This is done in order to provide an easy way of reopening such closed instance
            // without the need of fully creating the a new 'mirrored' instance and adding it to the control.

            // The next code will permanently remove the RadPane instance from the RadDocking control. When PanesSource is set 
            // the closed RadPane instance needs to be manually removed from the bound collection. This will trigger the RemovePane()
            // method of the DockingPanesFactory of the control. If no PanesSource is used again a manually remove needs to be 
            // done by simply calling the RemoveFromParent() method of the RadPane instance.
            var docking = sender as RadDocking;
            foreach (var pane in e.Panes)
            {
                var dataContext = docking.DataContext as ViewModel;
                if (dataContext != null)
                {
                    dataContext.Panes.Remove(pane);
                }
            }
        }
    }
}
