using System;
using System.Linq;
using System.Windows.Controls;

namespace ChangeScaleFactor
{
    public partial class MainPage : UserControl
    {
        private readonly ViewModel viewModel;
        private const double DefaultInitialScaleFactor = 0.5;

        public MainPage()
        {
            this.viewModel = new ViewModel(DefaultInitialScaleFactor);
            InitializeComponent();
            this.pdfViewer.DocumentChanged += pdfViewer_DocumentChanged;
        }

        public ViewModel ViewModel
        {
            get
            {
                return this.viewModel;
            }
        }

        void pdfViewer_DocumentChanged(object sender, EventArgs e)
        {
            this.pdfViewer.CommandDescriptors.FixedDocumentViewer.ScaleFactor = this.ViewModel.InitialScaleFactor;    
        } 
    }
}
