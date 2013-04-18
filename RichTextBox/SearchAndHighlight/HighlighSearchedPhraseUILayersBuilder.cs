using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Controls;
using Telerik.Windows.Controls;
using Telerik.Windows.Documents.Model;
using Telerik.Windows.Documents.UI.Extensibility;
using Telerik.Windows.Documents.UI.Layers;

namespace SearchAndHighlight
{
    [CustomUILayersBuilder]
    public class HighlighSearchedPhraseUILayersBuilder : UILayersBuilder
    {
        protected override void BuildUILayersOverride(Telerik.Windows.Documents.UI.IUILayerContainer uiLayerContainer)
        {
            HighlightSearchedWordLayer layer = new HighlightSearchedWordLayer();            
            uiLayerContainer.UILayers.AddBefore(DefaultUILayers.SelectionLayer, layer);
            base.BuildUILayersOverride(uiLayerContainer);
        }

    }
}
