using Telerik.Windows.Documents.Fixed.UI.Layers;

namespace DrawOnDocument
{
    public class CustomUILayersBuilder : UILayersBuilder
    {
        protected override void BuildUILayersOverride(IUILayerContainer uiLayerContainer)
        {
            base.BuildUILayersOverride(uiLayerContainer);

            uiLayerContainer.UILayers.AddAfter(DefaultUILayers.ContentElementsUILayer, new DrawUILayer());
        }
    }
}