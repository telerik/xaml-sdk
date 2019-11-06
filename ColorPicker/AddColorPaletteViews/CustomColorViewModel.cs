using System.Collections.Generic;
using System.Windows.Media;

namespace AddColorPaletteViews_WPF
{
    public class CustomColorViewModel
    {
        public List<ColorPaletteView> ColorPaletteViewCollection { get; set; }
        public CustomColorViewModel()
        {
            ColorPaletteViewCollection = new List<ColorPaletteView>();
            GetColorPaletteViews();
        }
        private void GetColorPaletteViews()
        {
            ColorPaletteView customColorsPalleteView = new ColorPaletteView() { Header = "Custom colors 1" };
            customColorsPalleteView.Colors.Add(Colors.AliceBlue);
            customColorsPalleteView.Colors.Add(Colors.AntiqueWhite);
            customColorsPalleteView.Colors.Add(Colors.Aqua);
            customColorsPalleteView.Colors.Add(Colors.Aquamarine);
            customColorsPalleteView.Colors.Add(Colors.Azure);
            customColorsPalleteView.Colors.Add(Colors.Beige);
            customColorsPalleteView.Colors.Add(Colors.Bisque);
            customColorsPalleteView.Colors.Add(Colors.Black);
            customColorsPalleteView.Colors.Add(Colors.BlanchedAlmond);
            customColorsPalleteView.Colors.Add(Colors.Blue);
            ColorPaletteViewCollection.Add(customColorsPalleteView);

            ColorPaletteView customColorsPalleteView2 = new ColorPaletteView() { Header = "Custom Colors 2" };
            customColorsPalleteView2.Colors.Add(new Color() { R = 255, G = 0, B = 0, A = 100 });
            customColorsPalleteView2.Colors.Add(new Color() { R = 200, G = 25, B = 0, A = 100 });
            customColorsPalleteView2.Colors.Add(new Color() { R = 175, G = 50, B = 0, A = 100 });
            customColorsPalleteView2.Colors.Add(new Color() { R = 150, G = 75, B = 0, A = 100 });
            customColorsPalleteView2.Colors.Add(new Color() { R = 125, G = 100, B = 0, A = 100 });
            customColorsPalleteView2.Colors.Add(new Color() { R = 100, G = 125, B = 0, A = 100 });
            customColorsPalleteView2.Colors.Add(new Color() { R = 75, G = 150, B = 0, A = 100 });
            customColorsPalleteView2.Colors.Add(new Color() { R = 50, G = 175, B = 0, A = 100 });
            customColorsPalleteView2.Colors.Add(new Color() { R = 25, G = 200, B = 0, A = 100 });
            customColorsPalleteView2.Colors.Add(new Color() { R = 0, G = 255, B = 0, A = 100 });

            ColorPaletteViewCollection.Add(customColorsPalleteView2);
        }

    }
}
