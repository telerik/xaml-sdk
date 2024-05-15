using System;
using System.ComponentModel.Composition;
using System.Windows.Controls;
using Telerik.Windows.Controls;
using Telerik.Windows.Controls.Docking;
using PRISMIActiveAware = Microsoft.Practices.Prism.IActiveAware;

namespace ShellPrism
{
    /// <summary>
    /// Interaction logic for ToolBox.xaml
    /// </summary>
    [Export]
    public partial class ToolBox : RadPane, IPaneModel, PRISMIActiveAware
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