using System;
using System.ComponentModel.Composition;
using System.Windows.Controls;
using Telerik.Windows.Controls;
using Telerik.Windows.Controls.Docking;
using PRISMIActiveAware = Microsoft.Practices.Prism.IActiveAware;

namespace ShellPrism
{
    /// <summary>
    /// Interaction logic for SolutionExplorer.xaml
    /// </summary>
    [Export]
    public partial class SolutionExplorer : RadPane, IPaneModel, PRISMIActiveAware
    {
        public SolutionExplorer()
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
