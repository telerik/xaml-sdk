using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using Telerik.Windows.Controls.Diagrams.Extensions;

namespace GlyphToolBox
{
    public class ToolBoxTemplateSelector : DataTemplateSelector
    {
        public override DataTemplate SelectTemplate(object item, DependencyObject container)
        {
            GalleryItem galleryitem = item as GalleryItem;
            if (galleryitem == null)
            {
 	            return base.SelectTemplate(item, container);
            }

            return galleryitem.ItemType == "Glyph" ? this.TextShapeTemplate : this.GeometryShapeTemplate;
        }

        public DataTemplate GeometryShapeTemplate { get; set; }
        public DataTemplate TextShapeTemplate { get; set; }
    }  
}
