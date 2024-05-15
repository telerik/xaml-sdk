using System.Linq;
using System.Windows.Controls;
using Telerik.Windows.Controls;

namespace OrderedUnpinnedPanes
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

        private void Docking_Unpin(object sender, Telerik.Windows.Controls.Docking.StateChangeEventArgs e)
        {
            var orderredPanes = this.PaneGroup.UnpinnedPanes.OrderBy(x => x.Header);

            foreach (RadPane pane in orderredPanes)
            {
                this.PaneGroup.RemovePane(pane);
                this.PaneGroup.Items.Add(pane);
            }

            // we need to manually activate a Pane after modifying the collection
            var activePane = this.PaneGroup.EnumeratePanes().LastOrDefault(p => p.IsPinned);
            if (activePane != null)
            {
                activePane.IsActive = true;
            }
        }
    }
}
