using System;
using System.Windows.Controls;
using Telerik.Windows.Controls;
using Telerik.Windows.Controls.Docking;

namespace ShellPrism8
{
    /// <summary>
    /// Interaction logic for ServerExplorer.xaml
    /// </summary>
    public partial class ServerExplorer : RadPane, IPaneModel, Prism.IActiveAware
    {
        public ServerExplorer()
        {
            InitializeComponent();
        }

        public DockState Position
        {
            get { return DockState.DockedRight; }
        }

        public event EventHandler IsActiveChanged;

        protected override void OnIsActiveChanged()
        {
            base.OnIsActiveChanged();
            this.OnIsActiveChanged(EventArgs.Empty);
        }

        private void OnIsActiveChanged(EventArgs e)
        {
            if (this.IsActiveChanged != null)
            {
                this.IsActiveChanged(this, e);
            }
        }
    }
}