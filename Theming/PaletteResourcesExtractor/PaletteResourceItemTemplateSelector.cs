using System.Collections;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using Telerik.Windows.Controls.Theming;

namespace PaletteResourcesExtractor
{
    public class PaletteResourceItemTemplateSelector : DataTemplateSelector
    {
        public DataTemplate BrushTemplate { get; set; }
        public DataTemplate ColorTemplate { get; set; }
        public DataTemplate FontSizeTemplate { get; set; }
        public DataTemplate FontFamilyTemplate { get; set; }
        public DataTemplate OpacityTemplate { get; set; }
        public DataTemplate CornerRadiusTemplate { get; set; }
        public DataTemplate NoVisualTemplate { get; set; }

        public override DataTemplate SelectTemplate(object item, DependencyObject container)
        {
            var resourceEntry = (DictionaryEntry)item;
            if (resourceEntry.Value.GetType().IsSubclassOf(typeof(Brush)))
            {
                return BrushTemplate;
            }
            else if (resourceEntry.Value.GetType() == typeof(Color))
            {
                return ColorTemplate;
            }
            else if (resourceEntry.Value.GetType() == typeof(double))
            {
                if (resourceEntry.Key.ToString().Contains("Opacity"))
                {
                    return OpacityTemplate;
                }
                else if (resourceEntry.Key.ToString().Contains("FontSize"))
                {
                    return FontSizeTemplate;
                }
                else if (resourceEntry.Key.ToString().Contains("PopupOffset") ||
                         resourceEntry.Key.ToString().Contains("PillScale") ||
                         resourceEntry.Key.ToString().Contains("Offset") ||
                         resourceEntry.Key.ToString().Contains("Length") ||
                         resourceEntry.Key.ToString().Contains("Indentation") ||
                         resourceEntry.Key.ToString().Contains("Width") ||
                         resourceEntry.Key.ToString().Contains("Height"))
                {
                    return NoVisualTemplate;
                }
            }
            else if (resourceEntry.Value.GetType() == typeof(CornerRadius))
            {
                return CornerRadiusTemplate;
            }
            else if (resourceEntry.Value.GetType() == typeof(ScrollViewerScrollBarsMode))
            {
                return NoVisualTemplate;
            }
            else if (resourceEntry.Value.GetType() == typeof(FontFamily))
            {
                return FontFamilyTemplate;
            }
            else if (resourceEntry.Value.GetType() == typeof(Thickness))
            {
                if (resourceEntry.Key.ToString().Contains("Margin") ||
                    resourceEntry.Key.ToString().Contains("Padding") ||                    
                    resourceEntry.Key.ToString().Contains("Thickness") || 
                    resourceEntry.Key.ToString().Contains("Offset")) 
                {
                    return NoVisualTemplate;
                }
            }

            return NoVisualTemplate;
        }
    }
}
