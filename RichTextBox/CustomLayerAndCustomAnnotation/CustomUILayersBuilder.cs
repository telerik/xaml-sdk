using Telerik.Windows.Documents.UI.Layers;

namespace CustomLayerAndCustomAnnotation
{
    public class CustomUILayersBuilder : UILayersBuilder
    {
        protected override void BuildUILayersOverride(Telerik.Windows.Documents.UI.IUILayerContainer uiLayerContainer)
        {
            base.BuildUILayersOverride(uiLayerContainer);

            uiLayerContainer.UILayers.AddLast(new CustomRangeLayer());
        }
    }
}
