using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using Telerik.Windows.Controls;
using Telerik.Windows.Controls.Docking;

namespace VisualStudioDocking
{
    public partial class MainPage : UserControl
    {
        public MainPage()
        {
            InitializeComponent();
            this.Loaded += OnLoaded;
            Application.Current.Exit += Current_Exit;

            this.DataContext = new MainWindowViewModel();
        }

        private void Current_Exit(object sender, EventArgs e)
        {
            var viewModel = this.DataContext as MainWindowViewModel;
            if (viewModel != null)
            {
                viewModel.Save(this.radDocking);
            }
        }

        private void OnLoaded(object sender, RoutedEventArgs e)
        {
            var viewModel = this.DataContext as MainWindowViewModel;
            if (viewModel != null)
            {
                Dispatcher.BeginInvoke(() =>
                    {
                        viewModel.Load(this.radDocking);
                    });
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