using Telerik.Windows.Controls;
using Telerik.Windows.Controls.Docking;

namespace DockingPrism7.Views
{
    public partial class OutgoingView : RadPane, IPaneModel
    {
        public OutgoingView()
        {
            InitializeComponent();
        }

        public DockState Position
        {
            get
            {
                return DockState.DockedBottom;
            }
        }
    }
}
