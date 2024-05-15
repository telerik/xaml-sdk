using System;
using System.Windows.Controls;
using Telerik.Windows.Controls;
using Telerik.Windows.Controls.Docking;

namespace ShellPrism8
{
    /// <summary>
    /// Interaction logic for ErrorList.xaml
    /// </summary>
   
    public partial class ErrorList : RadPane, IPaneModel, Prism.IActiveAware
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