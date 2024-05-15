using System;
using System.Windows;
using System.Windows.Media;
using Telerik.Windows.Controls;

namespace MediaPlayer.Library
{
    public class GlyphButton : RadButton
    {
        static GlyphButton()
        {   
            DefaultStyleKeyProperty.OverrideMetadata(typeof(GlyphButton), new FrameworkPropertyMetadata(typeof(GlyphButton)));
        }

        public static readonly DependencyProperty GlyphValueProperty =
            DependencyProperty.Register(
                "GlyphValue", 
                typeof(string), 
                typeof(GlyphButton), 
                new PropertyMetadata(null));

        public static readonly DependencyProperty GlyphSizeProperty =
           DependencyProperty.Register(
               "GlyphSize", 
               typeof(double), 
               typeof(GlyphButton), 
               new PropertyMetadata(0d));

        public static readonly DependencyProperty GlyphMarginProperty =
            DependencyProperty.Register(
                "GlyphMargin",
                typeof(Thickness),
                typeof(GlyphButton),
                new PropertyMetadata(new Thickness()));
        
        public static readonly DependencyProperty GlyphBrushProperty =
           DependencyProperty.Register(
               "GlyphBrush",
               typeof(Brush),
               typeof(GlyphButton),
               new PropertyMetadata(null));
       
        public string GlyphValue
        {
            get { return (string)GetValue(GlyphValueProperty); }
            set { SetValue(GlyphValueProperty, value); }
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

        protected override void OnInitialized(EventArgs e)
        {
            base.OnInitialized(e);
            StyleManager.SetDefaultStyleKey(this, typeof(GlyphButton));
        }
    }
}
