using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Diagnostics;
using System.Windows.Threading;

namespace CustomHyperlinkToolTip
{
    public static class HyperlinkNavigationBehavior
    {
        public static bool GetRequestNavigateOnLostFocusProperty(FrameworkElement frameworkElement)
        {
            return (bool)frameworkElement.GetValue(HyperlinkNavigationBehavior.RequestNavigateOnLostFocusProperty);
        }

        public static void SetRequestNavigateOnLostFocusProperty(FrameworkElement frameworkElement, bool value)
        {
            frameworkElement.SetValue(HyperlinkNavigationBehavior.RequestNavigateOnLostFocusProperty, value);
        }

        public static readonly DependencyProperty RequestNavigateOnLostFocusProperty =
            DependencyProperty.RegisterAttached(
            "RequestNavigateOnLostFocus",
            typeof(bool),
            typeof(HyperlinkNavigationBehavior),
            new PropertyMetadata(OnLoaded));

        private static void OnLoaded(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {

            TextBlock tb = d as TextBlock;
            if (tb != null)
            {
                tb.Dispatcher.BeginInvoke(new Action(() =>
                {
                    var hlink = tb.Inlines.FirstOrDefault(inline => inline is Hyperlink) as Hyperlink;
                    hlink.RequestNavigate += OnRequestNavigate;
                    hlink.GotFocus += OnGotFocus;
                }), DispatcherPriority.Render);
            }
        }

        static void OnGotFocus(object sender, RoutedEventArgs e)
        {
            var hl = sender as Hyperlink;
            hl.DoClick();
        }

        static void OnRequestNavigate(object sender, System.Windows.Navigation.RequestNavigateEventArgs e)
        {
            Process.Start(new ProcessStartInfo(e.Uri.AbsoluteUri));
            e.Handled = true;
        }
    }
}
