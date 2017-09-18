using Microsoft.Practices.Prism.Events;
using Microsoft.Practices.Prism.MefExtensions.Modularity;
using Microsoft.Practices.Prism.Modularity;
using Microsoft.Practices.Prism.Regions;
using Microsoft.Practices.ServiceLocation;
using ShellPrism;
using ShellPrism.Menu;
using System.ComponentModel.Composition;
using System.Linq;
using Telerik.Windows.Controls;

namespace ShellPrism
{
    [ModuleExport(typeof(FileServicesModule))]
    public class FileServicesModule : IModule
    {
        private IRegionManager regionManager;
        private IEventAggregator aggregator;

        [ImportingConstructor]
        public FileServicesModule(IRegionManager regionManager, IEventAggregator aggregator)
        {
            this.regionManager = regionManager;
            this.aggregator = aggregator;
            aggregator.GetEvent<CreateDocumentEvent>().Subscribe(this.OnCreateDocument, ThreadOption.PublisherThread);
            aggregator.GetEvent<ActivateViewEvent>().Subscribe(this.OnActivateView, ThreadOption.PublisherThread);
        }

        public static RadDocumentPane CreateDocument(string documentHeader)
        {
            return new NewDocument { Header = documentHeader };
        }

        public void Initialize()
        {
            // Docking region
            this.regionManager.RegisterViewWithRegion("DocumentsRegion", typeof(ErrorList));
            this.regionManager.RegisterViewWithRegion("DocumentsRegion", typeof(Output));
            this.regionManager.RegisterViewWithRegion("DocumentsRegion", typeof(PropertiesView));
            this.regionManager.RegisterViewWithRegion("DocumentsRegion", typeof(ServerExplorer));
            this.regionManager.RegisterViewWithRegion("DocumentsRegion", typeof(SolutionExplorer));
            this.regionManager.RegisterViewWithRegion("DocumentsRegion", typeof(ToolBox));

            this.aggregator.GetEvent<LoadLayoutEvent>().Publish(null);

            // Menu region
            this.regionManager.RegisterViewWithRegion("FileMenuRegion", typeof(MenuItemNew));
            this.regionManager.RegisterViewWithRegion("FileMenuRegion", typeof(MenuItemSave));
            this.regionManager.RegisterViewWithRegion("FileMenuRegion", typeof(MenuItemLoad));
            this.regionManager.RegisterViewWithRegion("FileMenuRegion", typeof(MenuItemActivatePane));
        }

        public void OnCreateDocument(string documentHeader)
        {
            var document = FileServicesModule.CreateDocument(documentHeader);
            this.AddDocument(document);
        }

        public void OnActivateView(string documentHeader)
        {
            var regionManager = ServiceLocator.Current.GetInstance<IRegionManager>();
            if (regionManager != null)
            {
                var dockRegion = regionManager.Regions["DocumentsRegion"];
                var paneView = dockRegion.Views.OfType<RadPane>.FirstOrDefault(p => p.Header.ToString() == documentHeader);
                if (paneView != null)
                {
                    dockRegion.Activate(paneView);
                    
                    paneView.IsHidden = false;
                }
            }
        }

        private void AddDocument(RadDocumentPane document)
        {
            var regionManager = ServiceLocator.Current.GetInstance<IRegionManager>();
            if (regionManager != null)
            {
                regionManager.AddToRegion("DocumentsRegion", document);
            }
        }
    }
}
