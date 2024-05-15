using System;
using System.Windows.Controls;
using Telerik.Windows.Controls;
using Telerik.Windows.Controls.Docking;

namespace ShellPrism8
{
    /// <summary>
    /// Interaction logic for ToolBox.xaml
    /// </summary>
    public partial class ToolBox : RadPane, IPaneModel, Prism.IActiveAware
    {
        public ToolBox()
        {
            InitializeComponent();
        }

        public DockState Position
        {
            get { return DockState.DockedLeft; }
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