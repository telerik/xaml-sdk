using System.Collections.Generic;
using System.Windows.Media;
using Telerik.Windows.Controls;

namespace AddColorPaletteViews_WPF
{
    public class ColorPaletteView
    {
        public string Header { get; set; }
        public List<Color> Colors { get; set; }
        public ColorPaletteView()
        {
            Colors = new List<Color>();
        }
    }
}
