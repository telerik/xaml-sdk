using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;

namespace SearchWithHighlight_WPF
{
    public class TextBlockUtilities
    {
        public static IEnumerable<InlineModel> GetInlines(DependencyObject obj)
        {
            return (IEnumerable<InlineModel>)obj.GetValue(InlinesProperty);
        }

        public static void SetInlines(DependencyObject obj, IEnumerable<InlineModel> value)
        {
            obj.SetValue(InlinesProperty, value);
        }

        public static readonly DependencyProperty InlinesProperty =
            DependencyProperty.RegisterAttached("Inlines", typeof(IEnumerable<InlineModel>), typeof(TextBlockUtilities), new PropertyMetadata(null, OnInlinesChanged));

        private static void OnInlinesChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var inlines = e.NewValue as IEnumerable<InlineModel>;
            if (inlines != null)
            {
                var textBlock = (TextBlock)d;
                textBlock.Inlines.Clear();
                foreach (var item in inlines)
                {
                    var run = new Run(item.Text);
                    if (item.Background != null)
                    {
                        run.Background = item.Background;
                    }
                    textBlock.Inlines.Add(run);
                }
            }
        }
    }
}
