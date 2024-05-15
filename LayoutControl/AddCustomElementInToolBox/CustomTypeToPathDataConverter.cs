using System;
using System.Globalization;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Media;
using Telerik.Windows.Controls.LayoutControl;

namespace AddCustomElementInToolBox
{
    public class CustomTypeToPathDataConverter : IValueConverter
    {
        private static TypeToPathDataConverter DefaultTypeToPathDataConverter = new TypeToPathDataConverter();
        private readonly Geometry ButtonPath = WebUIFontToGeometryConverter.ConvertGlyphNameToGeometry("GlyphButton", 14, Brushes.Red, new Point(0, 2));
        private readonly Geometry TextBoxPath = WebUIFontToGeometryConverter.ConvertGlyphNameToGeometry("GlyphTextboxHidden", 14, Brushes.Red, new Point(0, 2));

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            Type type = (Type)value;
            object pathData = null;

            if (type == typeof(Button))
            {
                pathData = ButtonPath;
            }
            else if (type == typeof(TextBox))
            {
                pathData = TextBoxPath;
            }
            else 
            {
                pathData = (string)DefaultTypeToPathDataConverter.Convert(type, null, null, null);
            }            

            return pathData;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }     
    }
}
