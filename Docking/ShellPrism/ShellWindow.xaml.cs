using Microsoft.Practices.Prism.Events;
using System.ComponentModel.Composition;
using System.IO;
using System.IO.IsolatedStorage;
using System.Windows;
using System.Windows.Media;

namespace ShellPrism
{
    /// <summary>
    /// Interaction logic for ShellWindow.xaml
    /// </summary>
    [Export]
    public partial class Shell : Window
    {
        public const string DockingLayoutFileName = "layout.xml";

        [ImportingConstructor]
        public Shell(IEventAggregator aggregator)
        {
            InitializeComponent();
            aggregator.GetEvent<SaveLayoutEvent>().Subscribe(this.SaveLayout, ThreadOption.PublisherThread);
            aggregator.GetEvent<LoadLayoutEvent>().Subscribe(this.LoadLayout, ThreadOption.PublisherThread);
            Application.Current.Exit += (s, e) => this.SaveLayout(null);
        }

        private void SaveLayout(object param)
        {
            using (IsolatedStorageFile storage = IsolatedStorageFile.GetUserStoreForAssembly())
            {
                using (var isoStream = storage.OpenFile(DockingLayoutFileName, FileMode.Create))
                {
                    radDocking.SaveLayout(isoStream);
                }
            }
        }

        private void LoadLayout(object param)
        {
            using (var storage = IsolatedStorageFile.GetUserStoreForAssembly())
            {
                if (storage.FileExists(DockingLayoutFileName))
                {
                    using (var isoStream = storage.OpenFile(DockingLayoutFileName, FileMode.Open))
                    {
                        isoStream.Seek(0, SeekOrigin.Begin);
                        radDocking.LoadLayout(isoStream);
                    }
                }
            }
        }
    }
}