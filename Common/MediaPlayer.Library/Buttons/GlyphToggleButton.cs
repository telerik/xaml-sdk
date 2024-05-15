using System.Windows;
using System.Windows.Media;
using Telerik.Windows.Controls;

namespace MediaPlayer.Library
{
    public class GlyphToggleButton : RadToggleButton
    {
        static GlyphToggleButton()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(GlyphToggleButton), new FrameworkPropertyMetadata(typeof(GlyphToggleButton)));
        }

        public static readonly DependencyProperty GlyphValueProperty =
            DependencyProperty.Register(
                "GlyphValue",
                typeof(string),
                typeof(GlyphToggleButton),
                new PropertyMetadata(null));
         
        public static readonly DependencyProperty ToggledGlyphValueProperty =
            DependencyProperty.Register(
                "ToggledGlyphValue", 
                typeof(string),
                typeof(GlyphToggleButton), 
                new PropertyMetadata(string.Empty));

        public static readonly DependencyProperty GlyphSizeProperty =
           DependencyProperty.Register(
               "GlyphSize",
               typeof(double),
               typeof(GlyphToggleButton),
               new PropertyMetadata(0d));

        public static readonly DependencyProperty GlyphMarginProperty =
            DependencyProperty.Register(
                "GlyphMargin",
                typeof(Thickness),
                typeof(GlyphToggleButton),
                new PropertyMetadata(new Thickness()));

        public static readonly DependencyProperty GlyphBrushProperty =
           DependencyProperty.Register(
               "GlyphBrush",
               typeof(Brush),
               typeof(GlyphToggleButton),
               new PropertyMetadata(null));

        public string GlyphValue
        {
            get { return (string)GetValue(GlyphValueProperty); }
            set { SetValue(GlyphValueProperty, value); }
        }

        public string ToggledGlyphValue
        {
            get { return (string)GetValue(ToggledGlyphValueProperty); }
            set { SetValue(ToggledGlyphValueProperty, value); }
        }

        public double GlyphSize
        {
            get { return (double)GetValue(GlyphSizeProperty); }
            set { SetValue(GlyphSizeProperty, value); }
        }

        public Thickness GlyphMargin
        {
            get { return (Thickness)GetValue(GlyphMarginProperty); }
            set { SetValue(GlyphMarginProperty, value); }
        }

        public Brush GlyphBrush
        {
            get { return (Brush)GetValue(GlyphBrushProperty); }
            set { SetValue(GlyphBrushProperty, value); }
        }
    }
}
