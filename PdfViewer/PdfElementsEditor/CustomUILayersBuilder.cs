using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telerik.Windows.Documents.Fixed.UI.Layers;

namespace PdfElementsEditor
{
    public class CustomUILayersBuilder : UILayersBuilder
    {
        public List<HighlightElementLayer> MyLayers { get; private set; }
        static int count = 0;
        public CustomUILayersBuilder()
        { 
            MyLayers = new List<HighlightElementLayer>();
        }

        protected override void BuildUILayersOverride(IUILayerContainer uiLayerContainer)
        {
            base.BuildUILayersOverride(uiLayerContainer);

            var MyLayer = new HighlightElementLayer(count++);
            MyLayers.Add(MyLayer);

            uiLayerContainer.UILayers.AddAfter(DefaultUILayers.AnnotationsUILayer, MyLayer);
        }
    }
}
