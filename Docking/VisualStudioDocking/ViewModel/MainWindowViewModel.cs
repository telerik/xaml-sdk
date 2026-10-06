using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.IO.IsolatedStorage;
using System.Windows.Input;
using System.Xml.Serialization;
using Telerik.Windows.Controls;
using Telerik.Windows.Controls.Docking;
using VisualStudioDocking.View;

namespace VisualStudioDocking
{
    public class MainWindowViewModel : ViewModelBase
    {
        private const string dockingLayoutFileName = "Layout.xml";
        private const string newDocumentsFileName = "NewDocuments.xml";
        private DelegateCommand newDocumentCommand;
        private DelegateCommand saveCommand;
        private DelegateCommand loadCommand;

        public MainWindowViewModel()
        {
            this.Panes = new ObservableCollection<PaneViewModel>();
        }

        public ObservableCollection<PaneViewModel> Panes
        {
            get;
            set;
        }

        public ICommand NewDocumentCommand
        {
            get
            {
                if (newDocumentCommand == null)
                    newDocumentCommand = new DelegateCommand(this.CreateNewDocument);

                return newDocumentCommand;
            }
        }

        public Action<Stream> SaveAction { get; set; }
        
        public Action<Stream> LoadAction { get; set; }

        public ICommand SaveCommand
        {
            get
            {
                if (saveCommand == null)
                    saveCommand = new DelegateCommand(Save);

                return saveCommand;
            }
        }

        public ICommand LoadCommand
        {
            get
            {
                if (loadCommand == null)
                    loadCommand = new DelegateCommand(Load);

                return loadCommand;
            }
        }

        public void Load(object param)
        {
            using (IsolatedStorageFile storage = IsolatedStorageFile.GetMachineStoreForAssembly())
            {
                if (storage.FileExists(newDocumentsFileName))
                {
                    var xmlSerializer = new XmlSerializer(this.Panes.GetType());
                    using (var isoStream = storage.OpenFile(newDocumentsFileName, FileMode.Open))
                    {
                        isoStream.Seek(0, SeekOrigin.Begin);
                        var documents = (IEnumerable<PaneViewModel>)xmlSerializer.Deserialize(isoStream);
                        this.Panes.Clear();
                        foreach (var document in documents)
                        {
                            this.Panes.Add(document);
                        }
                    }
                }
                else
                {
                    // Initial layout in Docking
                    this.Panes.Add(new PaneViewModel(typeof(ErrorList)) { Header = "Error List", InitialPosition = DockState.DockedBottom });
                    this.Panes.Add(new PaneViewModel(typeof(PropertiesPane)) { Header = "Properties", InitialPosition = DockState.DockedRight });
                    this.Panes.Add(new PaneViewModel(typeof(ServerExplorer)) { Header = "Server Explorer", InitialPosition = DockState.DockedLeft });
                    this.Panes.Add(new PaneViewModel(typeof(Output)) { Header = "Output", InitialPosition = DockState.DockedBottom });
                    this.Panes.Add(new PaneViewModel(typeof(SolutionExplorer)) { Header = "Solution Explorer", InitialPosition = DockState.DockedRight });
                    this.Panes.Add(new PaneViewModel(typeof(ToolBox)) { Header = "ToolBox", InitialPosition = DockState.DockedLeft });
                }

                if (storage.FileExists(dockingLayoutFileName))
                {
                    using (var isoStream = storage.OpenFile(dockingLayoutFileName, FileMode.Open))
                    {
                        isoStream.Seek(0, SeekOrigin.Begin);
                        this.LoadAction.Invoke(isoStream);
                    }
                }
            }
        }

        public void Save(object param)
        {
            using (IsolatedStorageFile storage = IsolatedStorageFile.GetMachineStoreForAssembly())
            {
                using (var isoStream = storage.OpenFile(newDocumentsFileName, FileMode.Create))
                {
                    var xmlSerializer = new XmlSerializer(this.Panes.GetType());
                    xmlSerializer.Serialize(isoStream, this.Panes);
                }

                using (var isoStream = storage.OpenFile(dockingLayoutFileName, FileMode.Create))
                {
                    isoStream.Seek(0, SeekOrigin.Begin);
                    this.SaveAction.Invoke(isoStream);
                }
            }
        }

        private void CreateNewDocument(object param)
        {
            this.Panes.Add(new PaneViewModel(null)
            {
                Header = "New Document " + Guid.NewGuid(),
                IsDocument = true
            });
        }
    }
}