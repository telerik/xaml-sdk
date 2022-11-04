using System.Linq;
using ShellPrism8.Menu;
using Telerik.Windows.Controls;
using Prism.Modularity;
using Prism.Regions;
using Prism.Events;
using Prism.Ioc;

namespace ShellPrism8
{
    public class FileServicesModule : IModule
    {
        private IRegionManager regionManager;
        private IEventAggregator aggregator;


        public void RegisterTypes(IContainerRegistry containerRegistry)
        {

        }

        public void OnInitialized(IContainerProvider containerProvider)
        {
            this.regionManager = containerProvider.Resolve<IRegionManager>();
            this.aggregator = containerProvider.Resolve<IEventAggregator>();
            aggregator.GetEvent<CreateDocumentEvent>().Subscribe(this.OnCreateDocument, ThreadOption.PublisherThread, true);
            aggregator.GetEvent<ActivateViewEvent>().Subscribe(this.OnActivateView, ThreadOption.PublisherThread, true);

            
        }

        public static RadDocumentPane CreateDocument(string documentHeader)
        {
            return new NewDocument { Header = documentHeader };
        }

        public void OnCreateDocument(string documentHeader)
        {
            var document = FileServicesModule.CreateDocument(documentHeader);
            this.AddDocument(document);
        }

        public void OnActivateView(string documentHeader)
        {
            if (this.regionManager != null)
            {
                var dockRegion = this.regionManager.Regions["DocumentsRegion"];
                var paneView = dockRegion.Views.OfType<RadPane>().FirstOrDefault(p => p.Header.ToString() == documentHeader);
                if (paneView != null)
                {
                    dockRegion.Activate(paneView);

                    paneView.IsHidden = false;
                }
            }
        }

        private void AddDocument(RadDocumentPane document)
        {
            if (this.regionManager != null)
            {
                this.regionManager.AddToRegion("DocumentsRegion", document);
            }
        }

       
    }
}