using System;
using System.Windows.Controls;
using Telerik.Windows.Controls;
using Telerik.Windows.Controls.Docking;

namespace ShellPrism8
{
    /// <summary>
    /// Interaction logic for Output.xaml
    /// </summary>
    public partial class Output : RadPane, IPaneModel, Prism.IActiveAware
    {
        public Output()
        {
            InitializeComponent();
        }

        public DockState Position
        {
            get { return DockState.FloatingDockable; }
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