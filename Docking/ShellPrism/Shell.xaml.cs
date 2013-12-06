using Microsoft.Practices.Prism.Events;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.IO;
using System.IO.IsolatedStorage;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;

namespace ShellPrism
{
    [Export]
    public partial class Shell : UserControl
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

        public void SaveLayout(object param)
        {
            using (IsolatedStorageFile storage = IsolatedStorageFile.GetUserStoreForApplication())
            {
                using (var isoStream = storage.OpenFile(DockingLayoutFileName, FileMode.Create))
                {
                    radDocking.SaveLayout(isoStream);
                }
            }
        }

        public void LoadLayout(object param)
        {
            using (var storage = IsolatedStorageFile.GetUserStoreForApplication())
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