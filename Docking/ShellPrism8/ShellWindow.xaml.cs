
using Prism.Events;
using System;
using System.IO;
using System.IO.IsolatedStorage;
using System.Windows.Threading;
using Telerik.Windows.Automation.Peers;
using Telerik.Windows.Controls;

namespace ShellPrism8
{
    public partial class Shell : RadWindow
    {
        public const string DockingLayoutFileName = "layout.xml";

        public Shell(IEventAggregator aggregator)
        {
            AutomationManager.AutomationMode = AutomationMode.Disabled;
            StyleManager.ApplicationTheme = new Windows8Theme();
            InitializeComponent();
            aggregator.GetEvent<SaveLayoutEvent>().Subscribe(this.SaveLayout, ThreadOption.PublisherThread);
            aggregator.GetEvent<LoadLayoutEvent>().Subscribe(this.LoadLayout, ThreadOption.PublisherThread);
            
            this.Unloaded += (s, e) => this.SaveLayout(null);
        }

        private void SaveLayout(object param)
        {
            using (IsolatedStorageFile storage = IsolatedStorageFile.GetUserStoreForAssembly())
            {
                using (var isoStream = storage.OpenFile(DockingLayoutFileName, FileMode.OpenOrCreate))
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