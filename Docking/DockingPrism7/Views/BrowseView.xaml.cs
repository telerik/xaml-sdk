using Telerik.Windows.Controls;
using Telerik.Windows.Controls.Docking;

namespace DockingPrism7.Views
{
    public partial class BrowseView : RadDocumentPane, IPaneModel
    {
        public BrowseView()
        {
            InitializeComponent();
        }

        public DockState Position
        {
            get
            {
                return DockState.DockedLeft;
            }
        }
    }
}
