using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace ChangeScaleFactor
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly ViewModel viewModel;
        private const double DefaultInitialScaleFactor = 0.5;

        public MainWindow()
        {
            this.viewModel = new ViewModel(DefaultInitialScaleFactor);
            InitializeComponent();
            pdfViewer.DocumentChanged += pdfViewer_DocumentChanged;
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
            pdfViewer.Commands.FixedDocumentViewer.ScaleFactor = this.ViewModel.InitialScaleFactor;    
        }
    }
}
