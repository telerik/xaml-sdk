using System;
using System.Windows;

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
            this.pdfViewer.DocumentChanged += this.OnDocumentChanged;
        } 

        public ViewModel ViewModel
        {
            get
            {
                return this.viewModel;
            }
        } 

        private void OnDocumentChanged(object sender, EventArgs e)
        {
            this.pdfViewer.ScaleFactor = this.ViewModel.InitialScaleFactor;
        }
    }
}
