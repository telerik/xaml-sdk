using Telerik.Windows.Controls;
using Telerik.Windows.Controls.Docking;

namespace DockingPrism7.Views
{
    public partial class AdditionalView : RadDocumentPane, IPaneModel
    {
        public AdditionalView()
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
