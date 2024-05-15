using Telerik.Windows.Controls;
using Telerik.Windows.Controls.Docking;

namespace DockingPrism7.Views
{
    public partial class IncomingView : RadPane, IPaneModel
    {
        public IncomingView()
        {
            InitializeComponent();
        }

        public DockState Position
        {
            get
            {
                return DockState.DockedRight;
            }
        }
    }
}
