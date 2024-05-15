using System;
using System.Globalization;
using System.Windows;
using Telerik.Windows.Controls;

namespace StringFormatMVVM
{
    public static class DateTimePickerExtensions
    {
        public static string GetShortDateFormat(DependencyObject obj)
        {
            return (string)obj.GetValue(ShortDateFormatProperty);
        }

        public static void SetShortDateFormat(DependencyObject obj, string value)
        {
            obj.SetValue(ShortDateFormatProperty, value);
        }

        public static readonly DependencyProperty ShortDateFormatProperty =
            DependencyProperty.RegisterAttached("ShortDateFormat", typeof(string), typeof(DateTimePickerExtensions), new PropertyMetadata(null, OnShortDateFormatPropertyChanged));

        private static void OnShortDateFormatPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            UpdatePatterns(d);
        }

        public static string GetLongDateFormat(DependencyObject obj)
        {
            return (string)obj.GetValue(LongDateFormatProperty);
        }

        public static void SetLongDateFormat(DependencyObject obj, string value)
        {
            obj.SetValue(LongDateFormatProperty, value);
        }

        public static readonly DependencyProperty LongDateFormatProperty =
            DependencyProperty.RegisterAttached("LongDateFormat", typeof(string), typeof(DateTimePickerExtensions), new PropertyMetadata(null, OnLongDateFormatPropertyChanged));

        private static void OnLongDateFormatPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            UpdatePatterns(d);
        }

        public static string GetShortTimeFormat(DependencyObject obj)
        {
            return (string)obj.GetValue(ShortTimeFormatProperty);
        }

        public static void SetShortTimeFormat(DependencyObject obj, string value)
        {
            obj.SetValue(ShortTimeFormatProperty, value);
        }

        public static readonly DependencyProperty ShortTimeFormatProperty =
            DependencyProperty.RegisterAttached("ShortTimeFormat", typeof(string), typeof(DateTimePickerExtensions), new PropertyMetadata(null, OnShortTimeFormatPropertyChanged));

        private static void OnShortTimeFormatPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            UpdatePatterns(d);
        }

        public static string GetLongTimeFormat(DependencyObject obj)
        {
            return (string)obj.GetValue(LongTimeFormatProperty);
        }

        public static void SetLongTimeFormat(DependencyObject obj, string value)
        {
            obj.SetValue(LongTimeFormatProperty, value);
        }

        public static readonly DependencyProperty LongTimeFormatProperty =
            DependencyProperty.RegisterAttached("LongTimeFormat", typeof(string), typeof(DateTimePickerExtensions), new PropertyMetadata(null, OnLongTimeFormatPropertyChanged));

        private static void OnLongTimeFormatPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            UpdatePatterns(d);
        }

        private static void UpdatePatterns(DependencyObject d)
        {
            var picker = d as RadDateTimePicker;
            if (picker != null)
            {
                var originalCulture = picker.Culture ?? CultureInfo.CurrentCulture;
                var culture = originalCulture.Clone() as CultureInfo;

                var format = culture.DateTimeFormat.Clone() as DateTimeFormatInfo;

                format.ShortDatePattern = GetShortDateFormat(picker) ?? format.ShortDatePattern;
                format.LongDatePattern = GetLongDateFormat(picker) ?? format.LongDatePattern;
                format.ShortTimePattern = GetShortTimeFormat(picker) ?? format.ShortTimePattern;
                format.LongTimePattern = GetLongTimeFormat(picker) ?? format.LongTimePattern;

                culture.DateTimeFormat = format;
                picker.Culture = culture;
            }
        }
    }
}