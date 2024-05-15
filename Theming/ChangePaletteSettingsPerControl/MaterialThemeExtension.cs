using System.Windows;
using System.Windows.Media;
using Telerik.Windows.Controls;

namespace ChangePaletteSettingsPerControl
{
    public class MaterialThemeExtensions
    {
        public static Color GetPrimaryNormalColor(DependencyObject obj)
        {
            return (Color)obj.GetValue(PrimaryNormalColorProperty);
        }

        public static void SetPrimaryNormalColor(DependencyObject obj, Color value)
        {
            obj.SetValue(PrimaryNormalColorProperty, value);
        }

        public static readonly DependencyProperty PrimaryNormalColorProperty =
            DependencyProperty.RegisterAttached("PrimaryNormalColor", typeof(Color), typeof(MaterialThemeExtensions), new PropertyMetadata(OnPrimaryNormalColorPropertyChanged));

        public static Color GetPrimaryFocusColor(DependencyObject obj)
        {
            return (Color)obj.GetValue(PrimaryFocusColorProperty);
        }

        public static void SetPrimaryFocusColor(DependencyObject obj, Color value)
        {
            obj.SetValue(PrimaryFocusColorProperty, value);
        }

        public static readonly DependencyProperty PrimaryFocusColorProperty =
            DependencyProperty.RegisterAttached("PrimaryFocusColor", typeof(Color), typeof(MaterialThemeExtensions), new PropertyMetadata(OnPrimaryFocusColorChanged));

        public static Color GetPrimaryHoverColor(DependencyObject obj)
        {
            return (Color)obj.GetValue(PrimaryHoverColorProperty);
        }

        public static void SetPrimaryHoverColor(DependencyObject obj, Color value)
        {
            obj.SetValue(PrimaryHoverColorProperty, value);
        }

        public static readonly DependencyProperty PrimaryHoverColorProperty =
            DependencyProperty.RegisterAttached("PrimaryHoverColor", typeof(Color), typeof(MaterialThemeExtensions), new PropertyMetadata(OnPrimaryHoverColorChanged));

        private static void OnPrimaryNormalColorPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var freezable = FreezeResource(new SolidColorBrush((Color)e.NewValue));
           
            RemoveAndAddResource((d as FrameworkElement), MaterialResourceKey.PrimaryNormalBrush, freezable);
        }

        private static void OnPrimaryFocusColorChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var freezable = FreezeResource(new SolidColorBrush((Color)e.NewValue));
            
            RemoveAndAddResource((d as FrameworkElement), MaterialResourceKey.PrimaryFocusBrush, freezable);
        }

        private static void OnPrimaryHoverColorChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var freezable = FreezeResource(new SolidColorBrush((Color)e.NewValue));
            
            RemoveAndAddResource((d as FrameworkElement), MaterialResourceKey.PrimaryHoverBrush, freezable);
        }

        internal static Freezable FreezeResource(object resource)
        {
            Freezable freezable = resource as Freezable;
            if (freezable != null)
            {
                freezable.Freeze();
            }

            return freezable;
        }

        internal static void RemoveAndAddResource(FrameworkElement element, ResourceKey key, Freezable freezable)
        {
            element.Resources.Remove(key);
            element.Resources.Add(key, freezable);
        }
    }
}
