using Telerik.Windows.Controls;
using Telerik.Windows.Controls.Docking;

namespace DockingPrism7.Views
{
    public partial class SentView : RadPane, IPaneModel
    {
        public SentView()
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
