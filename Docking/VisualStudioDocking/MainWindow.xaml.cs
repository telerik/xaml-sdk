using System;
using System.IO;
using System.Linq;
using System.Windows;
using Telerik.Windows.Controls;
using Telerik.Windows.Controls.Docking;

namespace VisualStudioDocking
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            this.Loaded += OnLoaded;
            Application.Current.Exit += OnApplicationExit;
        }

        private void OnApplicationExit(object sender, ExitEventArgs e)
        {
            var viewModel = this.DataContext as MainWindowViewModel;
            if (viewModel != null)
            {
                viewModel.Save(null);
            }
        }

        private void OnLoaded(object sender, RoutedEventArgs e)
        {
            var viewModel = this.DataContext as MainWindowViewModel;
            if (viewModel != null)
            {
                viewModel.SaveAction = new Action<Stream>((str) => { this.radDocking.SaveLayout(str); }); 
                viewModel.LoadAction = new Action<Stream>((str) => { this.radDocking.LoadLayout(str); });
                viewModel.Load(null);
            }
        }

        private void OnPreviewShowCompass(object sender, PreviewShowCompassEventArgs e)
        {
            bool isRootCompass = e.Compass is RootCompass;
            var splitContainer = e.DraggedElement as RadSplitContainer;
            if (splitContainer != null)
            {
                bool isDraggingDocument = splitContainer.EnumeratePanes().Any(p => p is RadDocumentPane);
                var isTargetDocument = e.TargetGroup == null ? true : e.TargetGroup.EnumeratePanes().Any(p => p is RadDocumentPane);
                if (isDraggingDocument)
                {
                    e.Canceled = isRootCompass || !isTargetDocument;
                }
                else
                {
                    e.Canceled = !isRootCompass && isTargetDocument;
                }
            }
        }

        private void OnClose(object sender, StateChangeEventArgs e)
        {
            var documents = e.Panes.Select(p => p.DataContext).OfType<PaneViewModel>().Where(vm => vm.IsDocument).ToList();
            foreach (var document in documents)
            {
                ((MainWindowViewModel)this.DataContext).Panes.Remove(document);
            }
        }

        private void FilterActiveViewsSource(object sender, System.Windows.Data.FilterEventArgs e)
        {
            var vm = e.Item as PaneViewModel;
            e.Accepted = vm.IsDocument;
        }

        private void FilterToolboxesSource(object sender, System.Windows.Data.FilterEventArgs e)
        {
            var vm = e.Item as PaneViewModel;
            e.Accepted = !vm.IsDocument;
        }
    }
}