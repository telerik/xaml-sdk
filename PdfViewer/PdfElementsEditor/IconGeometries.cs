using System.Windows.Media;

namespace PdfElementsEditor
{
    /// <summary>
    /// Provides the frozen vector geometries and brushes used as icons in the tree view.
    /// Keeping them static and frozen avoids re-parsing the path data for every node.
    /// </summary>
    internal static class IconGeometries
    {
        internal static readonly Geometry Page = Create("M4,1 L11,1 L15,5 L15,19 L4,19 Z M11,1 L11,5 L15,5");
        internal static readonly Geometry Image = Create("M2,3 L18,3 L18,17 L2,17 Z M2,14 L7,9 L11,13 L14,10 L18,14 L18,17 L2,17 Z M13.5,6.5 A1.5,1.5 0 1 1 13.49,6.5 Z");
        internal static readonly Geometry Text = Create("M3,3 L17,3 L17,6 L11.5,6 L11.5,17 L8.5,17 L8.5,6 L3,6 Z");
        internal static readonly Geometry Path = Create("M3,16 C3,8 8,4 17,4 L17,7 C10,7 6,10 6,16 Z M1,15 L5,15 L5,19 L1,19 Z M15,2 L19,2 L19,6 L15,6 Z");
        internal static readonly Geometry Unknown = Create("M10,2 A8,8 0 1 1 9.99,2 Z");

        internal static readonly Brush PageBrush = Create(Colors.SteelBlue);
        internal static readonly Brush ImageBrush = Create(Colors.SeaGreen);
        internal static readonly Brush TextBrush = Create(Colors.DarkOrange);
        internal static readonly Brush PathBrush = Create(Colors.MediumPurple);
        internal static readonly Brush UnknownBrush = Create(Colors.Gray);

        private static Geometry Create(string pathData)
        {
            Geometry geometry = Geometry.Parse(pathData);
            geometry.Freeze();

            return geometry;
        }

        private static Brush Create(Color color)
        {
            SolidColorBrush brush = new SolidColorBrush(color);
            brush.Freeze();

            return brush;
        }
    }
}
