using System;
using System.ComponentModel.Composition;
using System.Windows.Controls;
using Telerik.Windows.Controls;
using Telerik.Windows.Controls.Docking;
using PRISMIActiveAware = Microsoft.Practices.Prism.IActiveAware;

namespace ShellPrism
{
    /// <summary>
    /// Interaction logic for ErrorList.xaml
    /// </summary>
    [Export]
    public partial class ErrorList : RadPane, IPaneModel, PRISMIActiveAware
    {
        public ErrorList()
        {
            InitializeComponent();
        }

        public DockState Position
        {
            get { return DockState.DockedBottom; }
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